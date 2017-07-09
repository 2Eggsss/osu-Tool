using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace osu__Tool
{
    public static class BeatmapPlayfield
    {
        public static int Width { get => 512; }
        public static int Height { get => 384; }
    }

    public enum BeatmapMode
    {
        Standard,
        Taiko,
        CatchTheBeat,
        Mania
    }

    public struct BeatmapMetadata
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Creator { get; set; }
        public string Version { get; set; }
    }

    public struct BeatmapDifficulty
    {
        public float HPDrainRate { get; set; }
        public float CircleSize { get; set; }
        public float OverallDifficulty { get; set; }
        public float ApproachRate { get; set; }
        public float SliderMultiplier { get; set; }
        public float SliderTickRate { get; set; }
    }

    public struct BeatmapTimingPoint
    {
        public float Offset { get; set; }
        public float MsPerBeat { get; set; }
        public int Meter { get; set; }
        public float Velocity { get; set; }
    }

    public enum HitObjectType
    {
        Circle = 1,
        Slider = 2,
        Spinner = 8
    }

    public enum HitObjectSliderType
    {
        Linear = 'L',
        Perfect = 'P',
        Bezier = 'B',
        Catmull = 'C'
    }

    public struct BeatmapHitObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public HitObjectType Type { get; set; }

        public HitObjectSliderType SliderType { get; set; }
        public List<int> SliderCurveX { get; set; }
        public List<int> SliderCurveY { get; set; }
        public int SliderRepeat { get; set; }
        public float SliderPixelLength { get; set; }

        public bool IsCircle { get => (Type & HitObjectType.Circle) == HitObjectType.Circle; }
        public bool IsSlider { get => (Type & HitObjectType.Slider) == HitObjectType.Slider; }
        public bool IsSpinner { get => (Type & HitObjectType.Spinner) == HitObjectType.Spinner; }
    }

    public sealed class OsuBeatmap
    {
        private BeatmapMode mode;
        private BeatmapMetadata metadata;
        private BeatmapDifficulty difficulty;
        private List<BeatmapTimingPoint> timingPoints = new List<BeatmapTimingPoint>();
        private List<BeatmapHitObject> hitObjects = new List<BeatmapHitObject>();

        public BeatmapMode Mode { get => mode; }
        public BeatmapMetadata Metadata { get => metadata; }
        public BeatmapDifficulty Difficulty { get => difficulty; }
        public List<BeatmapTimingPoint> TimingPoints { get => timingPoints; }
        public List<BeatmapHitObject> HitObjects { get => hitObjects; }
        public float HitObjectRadius { get => (BeatmapPlayfield.Width / 16.0f) * (1.0f - 0.7f * (difficulty.CircleSize - 5.0f) / 5.0f); }

        public OsuBeatmap(string path, bool ez, bool hr)
        {
            string currentSection = String.Empty;
            bool foundAR = false;
            float lastMsPerBeat = 0;

            foreach (string line in File.ReadLines(path))
            {
                if (String.IsNullOrEmpty(line))
                    continue;

                // Check if current line is a section.
                if (line.First() == '[' && line.Last() == ']')
                {
                    currentSection = line;
                    continue;
                }

                switch (currentSection)
                {
                    case "[General]":
                        ParseGeneral(line);
                        break;
                    case "[Editor]":
                        break;
                    case "[Metadata]":
                        ParseMetadata(line);
                        break;
                    case "[Difficulty]":
                        ParseDifficulty(line, ref foundAR, ez, hr);
                        break;
                    case "[Events]":
                        break;
                    case "[TimingPoints]":
                        ParseTimingPoints(line, ref lastMsPerBeat);
                        break;
                    case "[Colours]":
                        break;
                    case "[HitObjects]":
                        ParseHitObjects(line, hr);
                        break;
                }
            }

            if (!foundAR)
                difficulty.ApproachRate = difficulty.OverallDifficulty;
        }

        public BeatmapTimingPoint GetTimingPointFromOffset(int offset)
        {
            for (int i = timingPoints.Count; i-- > 0;)
            {
                if (timingPoints[i].Offset <= offset)
                    return timingPoints[i];
            }

            return timingPoints.First();
        }

        public int MsPerBeatToBPM(float msPerBeat)
        {
            // Convert from MsPerBeat to BPM.
            float msPerMinute = 1 * 60 * 1000;

            return (int)Math.Round(msPerMinute / msPerBeat);
        }

        private void ParseGeneral(string line)
        {
            string[] lineSplit = line.Split(new string[] { ": " }, StringSplitOptions.None);

            switch (lineSplit.First())
            {
                case "Mode":
                    mode = (BeatmapMode)Convert.ToInt32(lineSplit.Last());
                    break;
            }
        }

        private void ParseMetadata(string line)
        {
            // Will break if line contains more than one ':' character.
            // Use Regex.Match

            string[] lineSplit = line.Split(':');

            switch (lineSplit.First())
            {
                case "Title":
                    metadata.Title = lineSplit.Last();
                    break;
                case "Artist":
                    metadata.Artist = lineSplit.Last();
                    break;
                case "Creator":
                    metadata.Creator = lineSplit.Last();
                    break;
                case "Version":
                    metadata.Version = lineSplit.Last();
                    break;
            }
        }

        private void ParseDifficulty(string line, ref bool foundAR, bool ez, bool hr)
        {
            string[] lineSplit = line.Split(':');

            switch (lineSplit.First())
            {
                case "HPDrainRate":
                    difficulty.HPDrainRate = Convert.ToSingle(lineSplit.Last());

                    if (ez)
                        difficulty.HPDrainRate /= 2;
                    else if (hr)
                    {
                        difficulty.HPDrainRate *= 1.4f;

                        if (difficulty.HPDrainRate > 10)
                            difficulty.HPDrainRate = 10;
                    }

                    break;
                case "CircleSize":
                    difficulty.CircleSize = Convert.ToSingle(lineSplit.Last());

                    if (ez)
                        difficulty.CircleSize /= 2;
                    else if (hr)
                    {
                        difficulty.CircleSize *= 1.3f;

                        if (difficulty.CircleSize > 10)
                            difficulty.CircleSize = 10;
                    }

                    break;
                case "OverallDifficulty":
                    difficulty.OverallDifficulty = Convert.ToSingle(lineSplit.Last());

                    if (ez)
                        difficulty.OverallDifficulty /= 2;
                    else if (hr)
                    {
                        difficulty.OverallDifficulty *= 1.4f;

                        if (difficulty.OverallDifficulty > 10)
                            difficulty.OverallDifficulty = 10;
                    }

                    break;
                case "ApproachRate":
                    difficulty.ApproachRate = Convert.ToSingle(lineSplit.Last());
                    foundAR = true;

                    if (ez)
                        difficulty.ApproachRate /= 2;
                    else if (hr)
                    {
                        difficulty.ApproachRate *= 1.4f;

                        if (difficulty.ApproachRate > 10)
                            difficulty.ApproachRate = 10;
                    }

                    break;
                case "SliderMultiplier":
                    difficulty.SliderMultiplier = Convert.ToSingle(lineSplit.Last());
                    break;
                case "SliderTickRate":
                    difficulty.SliderTickRate = Convert.ToSingle(lineSplit.Last());
                    break;
            }
        }

        private void ParseTimingPoints(string line, ref float lastMsPerBeat)
        {
            string[] lineSplit = line.Split(',');

            BeatmapTimingPoint timingPoint = new BeatmapTimingPoint()
            {
                Offset = Convert.ToSingle(lineSplit[0]),
                MsPerBeat = Convert.ToSingle(lineSplit[1]),
                Meter = Convert.ToInt32(lineSplit[2]),
                Velocity = 1
            };

            if (timingPoint.MsPerBeat < 0)
            {
                timingPoint.Velocity = Math.Abs(100.0f / timingPoint.MsPerBeat);

                if (lastMsPerBeat != 0)
                    timingPoint.MsPerBeat = lastMsPerBeat;
            }
            else
                lastMsPerBeat = timingPoint.MsPerBeat;

            timingPoints.Add(timingPoint);
        }

        private void ParseHitObjects(string line, bool hr)
        {
            string[] lineSplit = line.Split(',');

            BeatmapHitObject hitObject = new BeatmapHitObject()
            {
                X = Convert.ToInt32(lineSplit[0]),
                Y = Convert.ToInt32(lineSplit[1]),
                StartTime = Convert.ToInt32(lineSplit[2]),
                Type = (HitObjectType)Convert.ToInt32(lineSplit[3])
            };

            if (hr)
                hitObject.Y = BeatmapPlayfield.Height - hitObject.Y;

            if (hitObject.IsCircle)
                hitObject.EndTime = hitObject.StartTime;
            else if (hitObject.IsSlider)
            {
                string[] sliderSplit = lineSplit[5].Split('|');

                hitObject.SliderType = (HitObjectSliderType)sliderSplit[0].First();

                hitObject.SliderCurveX = new List<int>();
                hitObject.SliderCurveY = new List<int>();

                // Loop through slider tokens, but skip the first one because it is the slider type.
                for (int i = 1; i < sliderSplit.Length; i++)
                {
                    string[] sliderCurveSplit = sliderSplit[i].Split(':');

                    hitObject.SliderCurveX.Add(Convert.ToInt32(sliderCurveSplit.First()));
                    hitObject.SliderCurveY.Add(Convert.ToInt32(sliderCurveSplit.Last()));
                }

                hitObject.SliderRepeat = Convert.ToInt32(lineSplit[6]);
                hitObject.SliderPixelLength = Convert.ToSingle(lineSplit[7]);

                BeatmapTimingPoint timingPoint = GetTimingPointFromOffset(hitObject.StartTime);
                float pxPerBeat = difficulty.SliderMultiplier * 100 * timingPoint.Velocity;
                float beatsNum = (hitObject.SliderPixelLength * hitObject.SliderRepeat) / pxPerBeat;
                hitObject.EndTime = hitObject.StartTime + (int)Math.Ceiling(beatsNum * timingPoint.MsPerBeat);
            }
            else if (hitObject.IsSpinner)
                hitObject.EndTime = Convert.ToInt32(lineSplit[5]);

            hitObjects.Add(hitObject);
        }
    }
}
