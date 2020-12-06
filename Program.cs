using System;
using System.Linq;
using System.Reflection;
using System.IO;

namespace Lightning2x.AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------- AOC 2020 Lightning2x -------");
            Console.WriteLine("this program expects one of the following inputs:");
            Console.WriteLine("a > runs all days");
            Console.WriteLine("digit > runs the day specified using default input, and checks Nunit");
            Console.WriteLine("digit string > runs the day specified using the string as path \n");
            string input = Console.ReadLine();
            string[] split = input.Split(" ");
            // Find all types that implement the interface IDay in the current Assembly.
            Type[] iDayTypes = (from t in Assembly.Load("Lightning2x.AdventOfCode2020").GetExportedTypes()
                                where !t.IsInterface && !t.IsAbstract
                                where typeof(IDay).IsAssignableFrom(t)
                                select t).ToArray();
            if (input == "a")
            {
                RunAll(iDayTypes);
            }
            // Check if it is a number
            else if (int.TryParse(split[0], out _))
            {
                if (split.Length == 1)
                    RunDay(int.Parse(split[0]), iDayTypes);
                else
                    RunDay(int.Parse(split[0]), iDayTypes, split[1]);
            }
            else
            {
                Console.WriteLine("Invalid input specified.");
            }

        }

        private static void RunAll(Type[] dayTypes)
        {
            Utils.NUnitEnabled = true;
            int day = 1;
            // For each of these types found, we instantiate the type and Run it (in order of days)
            foreach (Type t in dayTypes)
            {
                string path = $"inputs/{day}.txt";
                var dayInstance = Activator.CreateInstance(t);
                Console.WriteLine($"--- Running Day {day} ---");
                ((IDay)dayInstance).Run(path);
                Console.WriteLine("\n");
                day++;
            }
        }

        // Run a specific day. Luckily for us we can assume that the order of the type array is 1-31 due to the way the types are ordered.
        private static void RunDay(int day, Type[] dayTypes)
        {
            Utils.NUnitEnabled = true;
            string path = $"inputs/{day}.txt";
            RunDayHelper(day, dayTypes, path);
        }

        private static void RunDay(int day, Type[] dayTypes, string path)
        {
            Utils.NUnitEnabled = false;
            if (!File.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File at path specified does not exist! Exiting!");
                Console.ResetColor();
            }
            RunDayHelper(day, dayTypes, path);
        }

        private static void RunDayHelper(int day, Type[] dayTypes, string path)
        {
            if (!(day > 0 || day <= 31))
                throw new ArgumentException("Invalid day specified! " + day);
            var dayInstance = Activator.CreateInstance(dayTypes[day - 1]);
            ((IDay)dayInstance).Run(path);
        }
    }
}
