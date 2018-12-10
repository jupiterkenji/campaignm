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
            return text == null || text.Length == 0;
        }

        // Question 2

        public static IEnumerable<int> GetPositiveDivisor(int number)
        {
            return GetPositiveDivisorCore(number).OrderBy(n => n);
        }

        // This function is more complicated than simply checking '(number % i) == 0'
        //  because of performance reason i.e. cut the run time to less than O(n)
        //  the idea is when 'number % i == 0', we found 2 divisors 
        //  so we don't have to iterate all number
        //  For number=60, instead of having 60 iterations, we just have 8 iterations 
        static IEnumerable<int> GetPositiveDivisorCore(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException($"number '{number}' should be positive.");
            }
            else if(number == 0)
            {
                yield break;
            }
            else if(number == 1)
            {
                yield return 1;
            }
            else
            {
                yield return 1;

                var maxIteration = number;
                for (var i=2; i < maxIteration; i++)
                {
                    if (number % i == 0)
                    {
                        yield return i;

                        var otherDivisor = number / i;
                        if (otherDivisor != i)
                        {
                            yield return otherDivisor;
                        }

                        maxIteration = otherDivisor;
                    }
                }

                yield return number;
            }
        }

        // Question 3
        public static double GetTriangleArea(int length1, int length2, int length3)
        {
            ValidateTriangle(length1, length2, length3);

            var halfPerimeter = (length1+length2+length3) / 2;
            return Math.Sqrt(halfPerimeter * (halfPerimeter-length1) * (halfPerimeter-length2) * (halfPerimeter-length3));
        }

        static void ValidateTriangle(int length1, int length2, int length3)
        {
            var lengths = new[] {length1, length2, length3};

            var invalidLengths = lengths.Where(length => length < 1);
            if (invalidLengths.Any())
            {
                throw new InvalidTriangleException(string.Format(NegativeTriangleLengthExceptionMessage, string.Join(", ", invalidLengths)));
            }

            if (length1 < 1)
            {
                throw new InvalidTriangleException(string.Format(NegativeTriangleLengthExceptionMessage, length1));
            }
            else if (length2 < 1)
            {
                throw new InvalidTriangleException(string.Format(NegativeTriangleLengthExceptionMessage, length2));
            }
            else if (length3 < 1)
            {
                throw new InvalidTriangleException(string.Format(NegativeTriangleLengthExceptionMessage, length2));
            }
            else if (length1+length2 <= length3)
            {
                throw new InvalidTriangleException(string.Format(InvalidTriangleExceptionMessage, length1, length2, length3));
            }
            else if (length1+length3 <= length2)
            {
                throw new InvalidTriangleException(string.Format(InvalidTriangleExceptionMessage, length1, length3, length2));
            }
            else if (length2+length3 <= length1)
            {
                throw new InvalidTriangleException(string.Format(InvalidTriangleExceptionMessage, length2, length3, length1));
            }
        }

        const string NegativeTriangleLengthExceptionMessage = "Length '{0}' should be greater than 0.";
        const string InvalidTriangleExceptionMessage = "Invalid triangle because its lengths '{0} + {1} <= {2}'.";

        // Question 4
        public static IEnumerable<int> GetMostCommon(IEnumerable<int> numbers)
        {
            var result = new Dictionary<int, int>();
            var maxOccurrence = 0;

            foreach (var number in numbers)
            {
                if (result.ContainsKey(number))
                {
                    result[number]++;
                    if (result[number] > maxOccurrence)
                    {
                        maxOccurrence = result[number];
                    }
                }
                else
                {
                    result[number] = 0;
                }
            }

            return result
                .Where(kvp => kvp.Value == maxOccurrence)
                .Select(kvp => kvp.Key);
        }
    }
}