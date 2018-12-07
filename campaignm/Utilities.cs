using System;
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
        public static int[] GetPositiveDivisor(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("number should be positive");
            }

            var result = new List<int>();
            for (var i=1; i <= number; i++)
            {
                if (number % i == 0)
                {
                    result.Add(i);
                }
            }

            return result.ToArray();
        }

        // Question 3
        public static double GetTriangleArea(int length1, int length2, int length3)
        {
            if (length1 < 1 || length2 < 1 || length3 < 1)
            {
                throw new InvalidTriangleException("Length should be greater than 0");
            }

            var isValid = length1+length2 > length3
                && length1+length3 > length2
                && length2+length3 > length1;
            if(!isValid)
            {
                throw new InvalidTriangleException("This is not a valid triangle");
            }

            var p = (length1+length2+length3) / 2;

            return Math.Sqrt(p * (p-length1) * (p-length2) * (p-length3));
        }

        // Question 4
        public static IEnumerable<int> GetMostCommon(IEnumerable<int> numbers)
        {
            var result = new Dictionary<int, int>();

            foreach (var number in numbers)
            {
                if (result.ContainsKey(number))
                {
                    result[number]++;
                }
                else
                {
                    result[number] = 0;
                }
            }

            var maxOccurrence = result.Values.Max();
            return result
                .Where(kvp => kvp.Value == maxOccurrence)
                .Select(kvp => kvp.Key);
        }
    }
}