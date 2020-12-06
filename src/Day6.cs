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
                    parseList.Add(string.Join(",", tempList));
                    tempList = new List<string>();
                }
                else
                    tempList.Add(fileLines[i]);
            }
            List<Group> groupList = Utils.TypeParser(parseList, Group.Parse);
            int distinctSum = 0, unionSum = 0;
            foreach(Group g in groupList)
            {
                distinctSum += g.Distinct;
                unionSum += g.Union;
            }
            Console.WriteLine($"The sum of distinct answers per group (Part 1) is {distinctSum}");
            Console.WriteLine($"The sum of union answers per group (Part 2) is {unionSum}");
        }

        public class Group
        {
            private string distinctAnswers;
            private string unionAnswers;
            public Group(string _distinctAnswers, string _unionAnswers)
            {
                distinctAnswers = _distinctAnswers;
                unionAnswers = _unionAnswers;
            }

            public int Distinct => distinctAnswers.Length;
            public int Union => unionAnswers.Length;

            public static Group Parse(string input)
            {
                Group group = null;
                try
                {
                    string distinct = new string(input.Replace("," , "").ToCharArray().Distinct().ToArray());
                    group = new Group(distinct, "");
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
