using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightning2x.AdventOfCode2020
{
    public class Day4 : IDay
    {
        public void Run(string path)
        {
            List<string> fileLines = Utils.ReadFile(path);
            List<string> passportStrings = new List<string>();
            int startIndex = 0;
            for(int i = 0; i < fileLines.Count; i++)
            {
                if(String.IsNullOrEmpty(fileLines[i])) 
                {
                    string passportString = fileLines[startIndex];
                    for(int j = startIndex + 1; j < i; j++)
                    {
                        passportString += " " + fileLines[j];
                    }
                    passportStrings.Add(passportString);
                    if(i < fileLines.Count - 1)
                        startIndex = i + 1;
                }
            }
            List<Passport> passports = Utils.TypeParser(passportStrings, Passport.Parse);
            int validPassports = 0;
            foreach (Passport p in passports)
                if (p.IsValid)
                    validPassports++;
            Console.WriteLine($"The amount of valid passports for part 1 is {validPassports}");
        }

        private class Passport
        {
            private Dictionary<string, string> passportFields;
            private HashSet<string> expectedFields = new HashSet<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};
            public Passport()
            {
                passportFields = new Dictionary<string, string>();
            }

            public bool IsValid => expectedFields.All(passportFields.ContainsKey);
            
            public static Passport Parse(string input)
            {
                //if (String.IsNullOrWhiteSpace(input)) throw new ArgumentException(input);
                Passport passport = new Passport();
                try
                {
                    string[] splitInput = input.Split(" ");
                    foreach (string s in splitInput)
                    passport.passportFields.Add(s.Split(":")[0], s.Split(":")[1]);
                }
                catch
                {
                    throw new ArgumentException("invalid password specs");
                }
                // Parse the string and populate the MyClass instance

                return passport;
            }
        }
    }
}
