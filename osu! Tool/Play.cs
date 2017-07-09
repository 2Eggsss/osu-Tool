using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using System.Runtime.InteropServices;

namespace osu__Tool
{
    public sealed class Play
    {
        private float xMultiplier, yMultiplier;
        private int xWindowPos, yWindowPos;

        public void Run(ref Osu osu, ref OsuBeatmap beatmap)
        {
            // Check if correct beatmap is selected and is being played.
            if (beatmap == null || beatmap.Mode != BeatmapMode.Standard || !osu.IsMapPlaying())
                return;

            int time = osu.GetAudioTime();

            int hitObjectIndex = 0;
            BeatmapHitObject hitObject = beatmap.HitObjects[hitObjectIndex];
            float hitObjectRadiusScaled = beatmap.HitObjectRadius * xMultiplier;

            float hitWindow300 = OsuGameRules.GetHitWindow300(beatmap.Difficulty.OverallDifficulty);
            float hitWindow100 = OsuGameRules.GetHitWindow100(beatmap.Difficulty.OverallDifficulty);
            float hitWindow50 = OsuGameRules.GetHitWindow50(beatmap.Difficulty.OverallDifficulty);

            // Can't even change the keys from UI.
            // 2017 programming LUL
            VirtualKeyCode primaryKey = VirtualKeyCode.VK_Z;
            VirtualKeyCode secondaryKey = VirtualKeyCode.VK_X;
            VirtualKeyCode allow100Key = VirtualKeyCode.SPACE;

            VirtualKeyCode activeKey = primaryKey;
            bool keyDown = false;

            // Default click window will simply be the timing offset.
            int clickWindow = Settings.Default.TimingOffset;

            float clickWindow300Min = -Math.Abs(hitWindow300) / 2.0f;
            float clickWindow300Max = Math.Abs(hitWindow300) / 2.0f;
            float clickWindow100Min = -Math.Abs(hitWindow100) / 1.5f;
            float clickWindow100Max = Math.Abs(hitWindow100) / 1.5f;

            // This needs tweaking for sure. Take into account if DT is enabled, otherwise it can hold active key for too long.
            float extraHoldTime = hitWindow300;
            float extraHoldTimeMin = extraHoldTime;
            float extraHoldTimeMax = extraHoldTime * 2.0f;

            float speedMultiplier = 1;

            if (Settings.Default.DoubleTime)
                speedMultiplier = 1.5f;
            else if (Settings.Default.HalfTime)
                speedMultiplier = 0.75f;

            float userSetBPM = Settings.Default.SingleTapMaxBPM / speedMultiplier / 2.0f;

            Random random = new Random();

            while (hitObjectIndex <= beatmap.HitObjects.Capacity)
            {
                // Check if map is still playing.
                if (!osu.IsMapPlaying())
                    break;

                bool waiting = false;

                if (Settings.Default.HitScan && !hitObject.IsSpinner && !IsCursorOnCircle(GetScaledX(hitObject.X), GetScaledY(hitObject.Y), hitObjectRadiusScaled, Settings.Default.HitScanPixelOffset))
                    waiting = true;

                time = osu.GetAudioTime();

                if (Settings.Default.RandomizeKeyTimings)
                {
                    extraHoldTime = GetRandomNumber(random, extraHoldTimeMin, extraHoldTimeMax);

                    if (InputSimulator.IsKeyDown(allow100Key))
                        clickWindow = random.Next((int)clickWindow100Min, (int)clickWindow100Max) + Settings.Default.TimingOffset;
                    else
                        clickWindow = random.Next((int)clickWindow300Min, (int)clickWindow300Max) + Settings.Default.TimingOffset;
                }

                float startTime = hitObject.StartTime + clickWindow;
                float endTime = hitObject.EndTime;

                // Only add extra hold time if the object is a circle OR it is a slider with hold time less than extra hold time.
                if (hitObject.IsCircle)
                    endTime += extraHoldTime;
                else if (hitObject.IsSlider && (endTime - startTime) < extraHoldTime)
                    endTime = startTime + extraHoldTime;

                if (time >= startTime && !keyDown && !waiting)
                {
                    if (Settings.Default.TappingStyle == 1 && !hitObject.IsSpinner && (hitObjectIndex + 1) <= beatmap.HitObjects.Count) // Single, not a spinner and next hit object exists.
                    {
                        BeatmapHitObject nextHitObject = beatmap.HitObjects[hitObjectIndex + 1];
                        int nextObjectBPM = beatmap.MsPerBeatToBPM(nextHitObject.StartTime - hitObject.EndTime);
                        int userSetBPMFinal = (int)Math.Round(userSetBPM * beatmap.GetTimingPointFromOffset(time).Meter);

                        if (nextObjectBPM > userSetBPMFinal)
                            activeKey = (activeKey == primaryKey) ? secondaryKey : primaryKey;
                        else
                            activeKey = primaryKey;
                    }

                    // Hold active key.
                    InputSimulator.SimulateKeyDown(activeKey);
                    keyDown = true;
                }
                else if (time > endTime && keyDown)
                {
                    // Release active key.
                    InputSimulator.SimulateKeyUp(activeKey);
                    keyDown = false;

                    if (Settings.Default.TappingStyle == 0) // Alternate
                        activeKey = (activeKey == primaryKey) ? secondaryKey : primaryKey;

                    // Advance to the next hit object.
                    hitObjectIndex++;
                    hitObject = beatmap.HitObjects[hitObjectIndex];
                }
                // Can't get a 50 anymore, move on to the next object.
                else if (waiting && time > (hitObject.EndTime + hitWindow50))
                {
                    hitObjectIndex++;
                    hitObject = beatmap.HitObjects[hitObjectIndex];
                }

                // Release keys if game is paused to prevent accidentaly hitting a menu button.
                if (!osu.IsAudioPlaying() && keyDown)
                    InputSimulator.SimulateKeyUp(activeKey);

                Task.Delay(1).Wait();
            }

            // Make sure keys are released when done.
            if (keyDown)
                InputSimulator.SimulateKeyUp(activeKey);
        }

        public void UpdateWindow(IntPtr osuWindowHandle)
        {
            GetClientRect(osuWindowHandle, out Rect rect);

            int x = Math.Min(rect.Right, GetSystemMetrics(SM_CXSCREEN));
            int y = Math.Min(rect.Bottom, GetSystemMetrics(SM_CYSCREEN));

            int swidth = x;
            int sheight = y;

            if (swidth * 3 > sheight * 4)
                swidth = sheight * 4 / 3;
            else
                sheight = swidth * 3 / 4;

            xMultiplier = swidth / 640.0f;
            yMultiplier = sheight / 480.0f;

            int xOffset = (int)(x - BeatmapPlayfield.Width * xMultiplier) / 2;
            int yOffset = (int)(y - BeatmapPlayfield.Height * yMultiplier) / 2;

            Point p = new Point()
            {
                X = 1,
                Y = (int)(8.0f * yMultiplier)
            };

            ClientToScreen(osuWindowHandle, ref p);

            xWindowPos = p.X + xOffset;
            yWindowPos = p.Y + yOffset;
        }

        private int GetStartingObjectIndex(OsuBeatmap beatmap, int time)
        {
            // Find out which hit object to start from.
            for (int i = 0; i < beatmap.HitObjects.Capacity; i++)
            {
                if (beatmap.HitObjects[i].StartTime > time)
                {
                    return i;
                }
            }

            throw new Exception("Could not get starting object index");
        }

        private bool IsCursorOnCircle(int circleX, int circleY, float circleRadius, float offset)
        {
            // Use cursor coordinates from game memory instead of GetCursorPos to make this method work with raw input.

            GetCursorPos(out Point cursor);

            int distanceX = circleX - cursor.X;
            int distanceY = circleY - cursor.Y;
            circleRadius -= offset;

            if (distanceX * distanceX + distanceY * distanceY <= circleRadius * circleRadius)
                return true;
            else
                return false;
        }

        private int GetScaledX(int x)
        {
            return (int)(x * xMultiplier + xWindowPos);
        }

        private int GetScaledY(int y)
        {
            return (int)(y * yMultiplier + yWindowPos);
        }

        private float GetRandomNumber(Random random, float minimum, float maximum)
        {
            return (float)random.NextDouble() * (maximum - minimum) + minimum;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetCursorPos(int X, int Y);

        private struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
    }
}
