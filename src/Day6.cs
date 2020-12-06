using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightning2x.AdventOfCode2020
{
    public class Day6 : IDay
    {
        public void Run(string path)
        {
            List<string> fileLines = Utils.ReadFile(path);
            // Add terminator at the end of the list.
            fileLines.Add("");
            List<string> tempList = new List<string>();
            List<string> parseList = new List<string>();
            for(int i = 0; i < fileLines.Count; i++)
            {
                if (string.IsNullOrEmpty(fileLines[i]))
                {
                    Console.WriteLine($"i is {i}");
                    parseList.Add(string.Join("", tempList));
                    tempList = new List<string>();
                }
                else
                    tempList.Add(fileLines[i]);
            }
            List<Group> groupList = Utils.TypeParser(parseList, Group.Parse);
            int sumCounts = 0;
            groupList.ForEach(x => sumCounts += x.AnswerCount);
            Console.WriteLine($"The sum of the counts is {sumCounts}");
        }

        public class Group
        {
            private string answers;

            public Group(string _answers)
            {
                answers = _answers;
            }

            public int AnswerCount => answers.Length;

            public string Answers => answers;

            public static Group Parse(string input)
            {
                Group group = null;
                try
                {
                    group = new Group(new string(input.ToCharArray().Distinct().ToArray()));
                }
                catch
                {
                    throw new ArgumentException("invalid group specs");
                }
                return group;
            }
        }
    }
}
