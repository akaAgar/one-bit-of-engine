using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asterion
{
    public static class AsterionTools
    {
        private static readonly Random[] RNG = new Random[] { new Random(), new Random() };

        public static int Clamp(int value, int minValue, int maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, value));
        }

        public static float Clamp(float value, float minValue, float maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, value));
        }

        public static double Clamp(double value, double minValue, double maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, value));
        }

        public static void SetRandomSeed(int seed)
        {
            RNG[1] = new Random(seed);
        }

        public static bool RandomBool(bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].NextDouble() < .5f;
        }

        public static float RandomFloat(bool useSeed = false)
        {
            return (float)RandomDouble(useSeed);
        }

        public static float RandomFloat(float maxValue, bool useSeed = false)
        {
            return (float)RandomDouble(0.0, maxValue, useSeed);
        }

        public static float RandomFloat(float minValue, float maxValue, bool useSeed = false)
        {
            return (float)RandomDouble(minValue, maxValue, useSeed);
        }

        public static double RandomDouble(bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].NextDouble();
        }

        public static double RandomDouble(double maxValue, bool useSeed = false)
        {
            return RandomDouble(0.0, maxValue, useSeed);
        }

        public static double RandomDouble(double minValue, double maxValue, bool useSeed = false)
        {
            double value = RandomDouble(useSeed);

            return value * (Math.Max(minValue, maxValue) - Math.Min(minValue, maxValue)) + Math.Min(minValue, maxValue);
        }

        public static int RandomInt(bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].Next();
        }

        public static int RandomInt(int maxValue, bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].Next(maxValue);
        }

        public static int RandomInt(int minValue, int maxValue, bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].Next(minValue, maxValue);
        }

        public static int EnumCount<T>() where T: Enum
        {
            return Enum.GetValues(typeof(T)).Length;
        }
    }
}
