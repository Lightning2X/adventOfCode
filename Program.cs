using System;
using System.Linq;
using System.Reflection;
using System.Runtime;
namespace Lightning2x.AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please select day. Type a for all days.");
            string input = Console.ReadLine();
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
            else if (int.TryParse(input, out _))
            {
               RunDay(int.Parse(input), iDayTypes);
            }
            else
            {
                Console.WriteLine("Invalid input specified.");
            }
           
        }

        private static void RunAll(Type[] dayTypes)
        {
           
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
            string path = $"inputs/{day}.txt";
            if (!(day > 0 || day <= 31))
                throw new ArgumentException("Invalid day specified! " + day);
            var dayInstance = Activator.CreateInstance(dayTypes[day - 1]);
            ((IDay)dayInstance).Run(path);
        }
    }
}
