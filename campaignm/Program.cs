using System;
using System.Linq;

namespace campaignmonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Campaign M Command Center:");
            DisplayCommandFormat();

            var input = string.Empty;
            do
            {
                Console.WriteLine("Enter Input: (type QUIT to quit)");
                input = Console.ReadLine();
                
                if (string.IsNullOrEmpty(input) || input == "QUIT")
                {
                    break;
                }

                var inputAsArray = input.Split(' ');

                var processed = false;
                if (inputAsArray.Length == 2)
                {
                    var param = inputAsArray[1]; 
                    try
                    {
                        switch (inputAsArray[0])
                        {
                            case "IsNullOrEmpty":
                                var isNullOrEmpty = Utilities.IsNullOrEmpty(param);
                                Console.WriteLine($"> Output: {isNullOrEmpty}");
                                processed = true;
                                break;
                            case "GetPositiveDivisor":
                                var positiveDivisors = Utilities.GetPositiveDivisor(Int16.Parse(param));
                                Console.WriteLine($"> Output: {string.Join(',', positiveDivisors)}");
                                processed = true;
                                break;
                            case "GetTriangleArea":
                                var lengths = param.Split(',');
                                if (lengths.Length == 3)
                                {
                                    var area = Utilities.GetTriangleArea(Int16.Parse(lengths[0]), Int16.Parse(lengths[1]), Int16.Parse(lengths[2]));
                                    Console.WriteLine($"> Output: {area.ToString()}");
                                }
                                processed = true;
                                break;
                            case "GetMostCommon":
                                var numbersAsText = param.Split(',');
                                if (numbersAsText.Length > 0)
                                {
                                    var numbers = numbersAsText.Select(n => Int32.Parse(n));
                                    var mostCommons = Utilities.GetMostCommon(numbers);
                                    Console.WriteLine($"> Output: {string.Join(',', mostCommons)}");
                                }
                                processed = true;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.ToString()}");
                    }
                }

                if (!processed)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid format.");
                    DisplayCommandFormat();
                }
                
                Console.WriteLine("----------------------------------------");
            }
            while (true);
        }

        static void DisplayCommandFormat()
        {
            Console.WriteLine("The available commands:");
            Console.WriteLine("> IsNullOrEmpty string");
            Console.WriteLine("> GetPositiveDivisor 10");
            Console.WriteLine("> GetTriangleArea 1,2,3");
            Console.WriteLine("> GetMostCommon 1,2,3");
        }
    }
}
