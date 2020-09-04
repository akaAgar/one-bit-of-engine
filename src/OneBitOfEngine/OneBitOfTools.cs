using System;
using System.Collections.Generic;

namespace OneBitOfEngine
{
    /// <summary>
    /// A "toolbox" static classes providing various helper methods.
    /// </summary>
    public static class OneBitOfTools
    {
        /// <summary>
        /// (Private) Random number generators. Index #0 is non-seeded, Index #1 is seeded.
        /// </summary>
        private static readonly Random[] RNG = new Random[] { new Random(), new Random(1) };

        /// <summary>
        /// Sets the seed used by the random number generator when OneBitOfTools.RandomXXX() methods are used with the "seeded = true" parameter.
        /// Changing this will reinitialize the RNG.
        /// </summary>
        /// <param name="seed">The seed</param>
        public static void SetRandomSeed(int seed)
        {
            RNG[1] = new Random(Math.Max(0, seed));
        }

        /// <summary>
        /// Clamps the value between minValue and maxValue.
        /// </summary>
        /// <param name="value">A value</param>
        /// <param name="minValue">Minimum acceptable value</param>
        /// <param name="maxValue">Maximum acceptable value</param>
        /// <returns>The clamped value</returns>
        public static int Clamp(int value, int minValue, int maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, value));
        }

        /// <summary>
        /// Clamps the value between minValue and maxValue.
        /// </summary>
        /// <param name="value">A value</param>
        /// <param name="minValue">Minimum acceptable value</param>
        /// <param name="maxValue">Maximum acceptable value</param>
        /// <returns>The clamped value</returns>
        public static float Clamp(float value, float minValue, float maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, value));
        }

        /// <summary>
        /// Clamps the value between minValue and maxValue.
        /// </summary>
        /// <param name="value">A value</param>
        /// <param name="minValue">Minimum acceptable value</param>
        /// <param name="maxValue">Maximum acceptable value</param>
        /// <returns>The clamped value</returns>
        public static double Clamp(double value, double minValue, double maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, value));
        }

        /// <summary>
        /// Returns a random boolean.
        /// </summary>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A boolean</returns>
        public static bool RandomBool(bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].NextDouble() < .5f;
        }

        /// <summary>
        /// Returns a random float between 0 and 1 (both inclusive).
        /// </summary>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A float</returns>
        public static float RandomFloat(bool useSeed = false)
        {
            return (float)RandomDouble(useSeed);
        }

        /// <summary>
        /// Returns a random float between 0 and maxValue (both inclusive).
        /// </summary>
        /// <param name="maxValue">The maximum possible value, INCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A float</returns>
        public static float RandomFloat(float maxValue, bool useSeed = false)
        {
            return (float)RandomDouble(0.0, maxValue, useSeed);
        }

        /// <summary>
        /// Returns a random float between minValue and maxValue (both inclusive).
        /// </summary>
        /// <param name="minValue">The minimum possible value, INCLUSIVE</param>
        /// <param name="maxValue">The maximum possible value, INCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A float</returns>
        public static float RandomFloat(float minValue, float maxValue, bool useSeed = false)
        {
            return (float)RandomDouble(minValue, maxValue, useSeed);
        }

        /// <summary>
        /// Returns a random double between 0 and 1 (both inclusive).
        /// </summary>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A double</returns>
        public static double RandomDouble(bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].NextDouble();
        }

        /// <summary>
        /// Returns a random double between 0 and maxValue (both inclusive).
        /// </summary>
        /// <param name="maxValue">The maximum possible value, INCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A double</returns>
        public static double RandomDouble(double maxValue, bool useSeed = false)
        {
            return RandomDouble(0.0, maxValue, useSeed);
        }

        /// <summary>
        /// Returns a random double between minValue and maxValue (both inclusive).
        /// </summary>
        /// <param name="minValue">The minimum possible value, INCLUSIVE</param>
        /// <param name="maxValue">The maximum possible value, INCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>A double</returns>
        public static double RandomDouble(double minValue, double maxValue, bool useSeed = false)
        {
            double value = RandomDouble(useSeed);

            return value * (Math.Max(minValue, maxValue) - Math.Min(minValue, maxValue)) + Math.Min(minValue, maxValue);
        }

        /// <summary>
        /// Returns a random integer between 0 (inclusive) and <see cref="int.MaxValue"/> (exclusive).
        /// </summary>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>An integer</returns>
        public static int RandomInt(bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].Next();
        }

        /// <summary>
        /// Returns a random integer between 0 (inclusive) and maxValue (exclusive).
        /// </summary>
        /// <param name="maxValue">The maximum possible value, EXCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>An integer</returns>
        public static int RandomInt(int maxValue, bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].Next(maxValue);
        }

        /// <summary>
        /// Returns a random integer between minValue (inclusive) and maxValue (exclusive).
        /// </summary>
        /// <param name="minValue">The maximum possible value, INCLUSIVE</param>
        /// <param name="maxValue">The maximum possible value, EXCLUSIVE</param>
        /// <param name="useSeed">Should the RNG use the seed provided by <see cref="SetRandomSeed(int)"/>?</param>
        /// <returns>An integer</returns>
        public static int RandomInt(int minValue, int maxValue, bool useSeed = false)
        {
            return RNG[useSeed ? 1 : 0].Next(minValue, maxValue);
        }

        /// <summary>
        /// Returns the number of values in an enumeration of type T.
        /// </summary>
        /// <typeparam name="T">A type of enumeration</typeparam>
        /// <returns>The number of values</returns>
        public static int EnumCount<T>() where T: Enum
        {
            return Enum.GetValues(typeof(T)).Length;
        }

        /// <summary>
        /// Splits a string into multiples lines of maximum length lineLength.
        /// </summary>
        /// <param name="text">The text to split in lines</param>
        /// <param name="lineLength">Max length of each line</param>
        /// <returns>An array of lines</returns>
        public static string[] WordWrap(string text, int lineLength)
        {
            if (string.IsNullOrEmpty(text)) return new string[0];
            lineLength = Math.Max(1, lineLength);

            string[] words = text.Replace("\\n", "\n").Split(' ', '\t', '\n');
            List<string> lines = new List<string>();
            string currentLine = "";

            foreach (string w in words)
            {
                if (currentLine.Length + w.Length >= lineLength)
                {
                    lines.Add(currentLine);
                    currentLine = "";
                }

                if (currentLine.Length > 0) currentLine += " ";
                currentLine += w;
            }

            // Add the last line
            if (currentLine.Length > 0) lines.Add(currentLine);

            return lines.ToArray();
        }
    }
}
