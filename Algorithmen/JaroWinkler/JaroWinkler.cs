using System;

namespace AlgoDat
{
    public static class JaroWinkler
    {
        private const double JARO_WINKLER_SCALE_FACTOR = 0.1;

        public static double JaroSimilarity(string s1, string s2)
        {
            if (s1.Equals(s2))
            {
                return 1.0;
            }

            if (s1.Length == 0 || s2.Length == 0)
            {
                return 0.0;
            }

            int maxDist = (int)Math.Floor(Math.Max(s1.Length, s2.Length) / 2.0) - 1;

            int m = 0;
            bool[] matchedCharsS1 = new bool[s1.Length];
            bool[] matchedCharsS2 = new bool[s2.Length];

            for (int i = 0; i < s1.Length; i++)
            {
                // Check if there is any matches
                for (int j = Math.Max(0, i - maxDist); j < Math.Min(s2.Length, i + maxDist + 1); j++)
                {
                    // If there is a match
                    if (s1[i] == s2[j] && !matchedCharsS2[j])
                    {
                        matchedCharsS1[i] = true;
                        matchedCharsS2[j] = true;
                        m++;

                        break;
                    }
                }
            }

            if (m == 0)
            {
                return 0.0;
            }

            double t = 0;
            int s2Pos = 0;

            for (int i = 0; i < s1.Length; i++)
            {
                if (matchedCharsS1[i])
                {
                    while (!matchedCharsS2[s2Pos])
                    {
                        s2Pos++;
                    }

                    if (s1[i] != s2[s2Pos++])
                    {
                        t++;
                    }
                }
            }

            t /= 2;

            return (((double)m) / ((double)s1.Length)
                + ((double)m) / ((double)s2.Length)
                + ((double)m - t) / ((double)m))
                / 3.0;
        }

        public static double JaroWinklerSimilarity(string s1, string s2)
        {
            double jaroSim = JaroSimilarity(s1, s2);

            // Find the length of common prefix
            int commonPrefixLength = 0;
            for (int i = 0; i < Math.Min(4, Math.Min(s1.Length, s2.Length)); i++)
            {
                // If the characters match
                if (s1[i] == s2[i])
                {
                    commonPrefixLength++;
                }
                else
                {
                    break;
                }
            }

            // Calculate jaro winkler Similarity
            jaroSim += JARO_WINKLER_SCALE_FACTOR * commonPrefixLength * (1 - jaroSim);
            
            return jaroSim;
        }
    }
}