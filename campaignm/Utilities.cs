using System.Collections.Generic;
using System.Linq;

namespace campaignmonitor
{
    public class Utilities
    {
        // Question 1
        public static bool IsNullOrEmpty(string text)
        {
            return text == null || text == string.Empty;
        }

        // Question 2
        public static IEnumerable<int> GetPositiveDivisor(int number)
        {
            return Enumerable.Empty<int>();
        }

        // Question 3
        public static int GetTriangleArea(int lenght1, int length2, int length3)
        {
            return -1;
        }

        // Question 4
        public static IEnumerable<int> GetMostCommon(IEnumerable<int> numbers)
        {
            return Enumerable.Empty<int>();
        }
    }
}