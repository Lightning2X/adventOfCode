using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Password> passwords = new List<Password>();
            List<string> fileLines = new List<string>();

            // Read the file.
            string path = "in.txt";
            StreamReader fileReader = new StreamReader(path);
            string line = fileReader.ReadLine();
            while (line != null)
            {
                passwords.Add(Password.Parse(line));
                line = fileReader.ReadLine();
            }
            Console.ReadLine();
        }

        public class Password
        {
            public int min;
            public int max; 
            public char constraint;
            public string password;
            public Password(int _min, int _max, char _constraint, string _password)
            {
                min = _min;
                max = _max;
                constraint = _constraint;
                password = _password;
            }

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
                // Parse the string and populate the MyClass instance

                return password;
            }
        }
    }
}
