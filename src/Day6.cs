using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

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
            int distinctSum = 0, intersectSum = 0;
            foreach(Group g in groupList)
            {
                distinctSum += g.Distinct;
                intersectSum += g.Intersection;
            }
            if(Utils.NUnitEnabled)
            {
                Assert.IsTrue(distinctSum == 6748);
                Assert.IsTrue(intersectSum == 3445);
            }
            Console.WriteLine($"The sum of distinct answers per group (Part 1) is {distinctSum}");
            Console.WriteLine($"The sum of union answers per group (Part 2) is {intersectSum}");
        }

        public class Group
        {
            private string distinctAnswers;
            private string intersectionAnswers;
            public Group(string _distinctAnswers, string _intersectionAnswers)
            {
                distinctAnswers = _distinctAnswers;
                intersectionAnswers = _intersectionAnswers;
            }

            public int Distinct => distinctAnswers.Length;
            public int Intersection => intersectionAnswers.Length;

            public static Group Parse(string input)
            {
                Group group = null;
                try
                {
                    string[] groups = input.Split(",");
                    string intersection = groups[0];
                    for(int i = 1; i < groups.Length; i++)
                    {
                        intersection = new string(intersection.Intersect(groups[i]).ToArray());
                    }
                    string distinct = new string(input.Replace("," , "").ToCharArray().Distinct().ToArray());
                    group = new Group(distinct, intersection);
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
