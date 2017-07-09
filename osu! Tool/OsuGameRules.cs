using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu__Tool
{
    public sealed class OsuGameRules
    {
        private static int HitWindow300Min { get => 80; }
        private static int HitWindow300Mid { get => 50; }
        private static int HitWindow300Max { get => 20; }
        private static int HitWindow100Min { get => 140; }
        private static int HitWindow100Mid { get => 100; }
        private static int HitWindow100Max { get => 60; }
        private static int HitWindow50Min { get => 200; }
        private static int HitWindow50Mid { get => 150; }
        private static int HitWindow50Max { get => 100; }

        private static float MapDifficultyRange(float scaledDiff, float min, float mid, float max)
        {
            if (scaledDiff > 5.0f)
                return mid + (max - mid) * (scaledDiff - 5.0f) / 5.0f;

            if (scaledDiff < 5.0f)
                return mid - (mid - min) * (5.0f - scaledDiff) / 5.0f;

            return mid;
        }

        private static float MapDifficultyRangeInv(float val, float min, float mid, float max)
        {
            if (val < mid) // > 5.0f (inverted)
                return ((val * 5.0f - mid * 5.0f) / (max - mid)) + 5.0f;

            if (val > mid) // < 5.0f (inverted)
                return 5.0f - ((mid * 5.0f - val * 5.0f) / (mid - min));

            return 5.0f;
        }

        public static float GetHitWindow300(float overallDifficulty)
        {
            return MapDifficultyRange(overallDifficulty, HitWindow300Min, HitWindow300Mid, HitWindow300Max);
        }

        public static float GetHitWindow100(float overallDifficulty)
        {
            return MapDifficultyRange(overallDifficulty, HitWindow100Min, HitWindow100Mid, HitWindow100Max);
        }

        public static float GetHitWindow50(float overallDifficulty)
        {
            return MapDifficultyRange(overallDifficulty, HitWindow50Min, HitWindow50Mid, HitWindow50Max);
        }

        public static float GetHitWindowMiss()
        {
            return 400;
        }

        public static float GetOverallDifficultyForSpeedMultiplier(float overallDifficulty, float speedMultiplier)
        {
            return MapDifficultyRangeInv(GetHitWindow300(overallDifficulty) * (1.0f / speedMultiplier), HitWindow300Min, HitWindow300Mid, HitWindow300Max);
        }

        public static OsuScore.Hit GetHitResult(float delta, float overallDifficulty)
        {
            delta = Math.Abs(delta);

            OsuScore.Hit result = OsuScore.Hit.Null;

            if (delta <= GetHitWindow300(overallDifficulty))
                result = OsuScore.Hit.Hit300;
            else if (delta <= GetHitWindow100(overallDifficulty))
                result = OsuScore.Hit.Hit100;
            else if (delta <= GetHitWindow50(overallDifficulty))
                result = OsuScore.Hit.Hit50;
            else if (delta <= GetHitWindowMiss())
                result = OsuScore.Hit.Miss;

            return result;
        }
    }
}
