namespace WebApplication2.Controllers
{
    public class JaroWinkler
    {
        public static double GetSimilarity(string s1, string s2)
        {
            double jaroDistance = GetJaroDistance(s1, s2);
            int prefixLength = 0;
            int maxPrefixLength = 4;

            for (int i = 0; i < Math.Min(maxPrefixLength, Math.Min(s1.Length, s2.Length)); i++)
            {
                if (s1[i] == s2[i]) prefixLength++;
                else break;
            }

            double jaroWinklerDistance = jaroDistance + (0.1 * prefixLength * (1 - jaroDistance));
            return jaroWinklerDistance;
        }

        private static double GetJaroDistance(string s1, string s2)
        {
            if (s1 == s2) return 1.0;

            int len1 = s1.Length;
            int len2 = s2.Length;
            int matchDistance = Math.Max(len1, len2) / 2 - 1;

            bool[] s1Matches = new bool[len1];
            bool[] s2Matches = new bool[len2];
            int matches = 0;

            for (int i = 0; i < len1; i++)
            {
                for (int j = Math.Max(0, i - matchDistance); j < Math.Min(len2, i + matchDistance + 1); j++)
                {
                    if (!s2Matches[j] && s1[i] == s2[j])
                    {
                        s1Matches[i] = true;
                        s2Matches[j] = true;
                        matches++;
                        break;
                    }
                }
            }

            if (matches == 0) return 0.0;

            int transpositions = 0;
            int point = 0;
            for (int i = 0; i < len1; i++)
            {
                if (s1Matches[i])
                {
                    while (!s2Matches[point]) point++;
                    if (s1[i] != s2[point]) transpositions++;
                    point++;
                }
            }

            return ((matches / (double)len1) + (matches / (double)len2) + ((matches - transpositions / 2) / (double)matches)) / 3.0;
        }
    }

}
