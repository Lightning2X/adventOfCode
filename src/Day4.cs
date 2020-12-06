using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Lightning2x.AdventOfCode2020
{
    public class Day4 : IDay
    {
        public void Run(string path)
        {
            List<string> fileLines = Utils.ReadFile(path);
            List<string> passportStrings = FormatPasswords(fileLines);
            List<Passport> passports = Utils.TypeParser(passportStrings, Passport.Parse);

            int passportsValidKeys = 0;
            int passportsValid = 0;
            foreach (Passport p in passports)
            {
                if (p.HasValidKeys)
                    passportsValidKeys++;
                if (p.IsValidPassport)
                    passportsValid++;
            }

            Assert.IsTrue(passportsValidKeys == 190);
            Assert.IsTrue(passportsValid == 121);
            Console.WriteLine($"The amount of valid passports for part 1 is {passportsValidKeys}");
            Console.WriteLine($"The amount of valid passports for part 2 is {passportsValid}");
        }

        private List<string> FormatPasswords(List<string> fileLines)
        {
            List<string> passportStrings = new List<string>();
            int startIndex = 0;
            for (int i = 0; i < fileLines.Count; i++)
            {
                if (String.IsNullOrEmpty(fileLines[i]))
                {
                    string passportString = fileLines[startIndex];
                    for (int j = startIndex + 1; j < i; j++)
                    {
                        passportString += " " + fileLines[j];
                    }
                    passportStrings.Add(passportString);
                    if (i < fileLines.Count - 1)
                        startIndex = i + 1;
                }
            }
            return passportStrings;
        }
        private class Passport
        {
            private Dictionary<string, string> passportFields;
            private HashSet<string> expectedFields = new HashSet<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};
            public Passport()
            {
                passportFields = new Dictionary<string, string>();
            }

            public bool HasValidKeys => expectedFields.All(passportFields.ContainsKey);

            public bool IsValidPassport
            {
                get
                {
                    if (!HasValidKeys)
                        return false;
                    else return BirthYearValid && IssuerYearValid && ExpirationYearValid && HeightValid && HairColorValid && EyeColorValid && PassportIdValid;
                }
            }

            private bool KeyInRange(string key, int min, int max)
            {
                int val;
                if (int.TryParse(passportFields[key], out val))
                    return val >= min && val <= max;
                else
                    return false;
            }
            private bool BirthYearValid => KeyInRange("byr", 1920, 2002);
            private bool IssuerYearValid => KeyInRange("iyr", 2010, 2020);
            private bool ExpirationYearValid => KeyInRange("eyr", 2020, 2030);
            private bool HeightValid
            {
                get
                {
                    string heightIndicator = passportFields["hgt"].Substring(passportFields["hgt"].Length - 2);
                    string heightString = passportFields["hgt"].Substring(0, passportFields["hgt"].Length - 2);
                    int height;
                    if (!int.TryParse(heightString, out height))
                        return false;
                    if (heightIndicator == "in")
                        return height >= 59 && height <= 76;
                    else if (heightIndicator == "cm")
                        return height >= 150 && height <= 193;
                    else
                        return false;
                }
            }
            private bool HairColorValid => passportFields["hcl"][0] == '#' && passportFields["hcl"].Length == 7 && int.TryParse(passportFields["hcl"].Substring(1), System.Globalization.NumberStyles.HexNumber, null, out _);
            private bool EyeColorValid
            {
                get
                {
                    string[] validEyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                    return validEyeColors.Any(c => c == passportFields["ecl"]);
                }
            }

            private bool PassportIdValid => passportFields["pid"].Length == 9 && int.TryParse(passportFields["pid"], out _);
            public static Passport Parse(string input)
            {
                Passport passport = new Passport();
                try
                {
                    string[] splitInput = input.Split(" ");
                    foreach (string s in splitInput)
                    passport.passportFields.Add(s.Split(":")[0], s.Split(":")[1]);
                }
                catch
                {
                    throw new ArgumentException("invalid passport specs");
                }
                return passport;
            }
        }
    }
}
