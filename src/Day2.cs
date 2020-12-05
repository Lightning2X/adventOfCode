using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lightning2x.AdventOfCode2020
{
    public class Day2 : IDay
    {
        public void Run(string path)
        {
            List<Password> passwords = new List<Password>();
            passwords = Utils.TypeParser(path, Password.Parse);
            int validOldPasswords = 0, validNewPasswords = 0;
            for (int i = 0; i < passwords.Count; i++)
            {
                if (passwords[i].IsValidOldPolicy)
                    validOldPasswords++;
                if (passwords[i].IsValidNewPolicy)
                    validNewPasswords++;
            }
            Console.WriteLine($"Amount of valid passwords according to the Old Policy: {validOldPasswords}");
            Console.WriteLine($"Amount of valid passwords according to the NEW Policy: {validNewPasswords}");
        }
        private class Password
        {
            private int min;
            private int max;
            private char constraint;
            private string password;
            public Password(int _min, int _max, char _constraint, string _password)
            {
                min = _min;
                max = _max;
                constraint = _constraint;
                password = _password;
            }

            public bool IsValidOldPolicy
            {
                get
                {
                    int constraintCount = Regex.Matches(password, constraint.ToString()).Count;
                    return constraintCount >= min && constraintCount <= max;
                }
            }

            public bool IsValidNewPolicy => password[min - 1] == constraint ^ password[max - 1] == constraint;
            
            public static Password Parse(string input)
            {
                if (String.IsNullOrWhiteSpace(input)) throw new ArgumentException(input);
                Password password = null;
                try
                {
                    string[] minmax = input.Split(" ")[0].Split("-");
                     password = new Password(
                        int.Parse(minmax[0]),
                        int.Parse(minmax[1]),
                        input.Split(" ")[1].Replace(@":", string.Empty)[0],
                        input.Split(" ")[2]);
                }
                catch
                {
                    throw new ArgumentException("invalid password specs");
                }

                return password;
            }
        }
    }
}
