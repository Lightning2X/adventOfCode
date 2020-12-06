using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Lightning2x.AdventOfCode2020
{
    public class Day3 : IDay
    {
        public void Run(string path)
        {
            // I convert to array since the list shouldn't "Expand" anymore after being read. since its a static grid.
            string[] grid = Utils.ReadFile(path).ToArray();
            // Amount of characters before we wrap around the list.
            int wrap = grid[0].Length;
            List<(int, int)> slopes = new List<(int, int)>() {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
                };
            int part2Result = 1;
            foreach ((int, int) s in slopes)
            {
                part2Result *= TreeSlopes(grid, wrap, s.Item1, s.Item2);
            }
            if(Utils.NUnitEnabled)
            {
                Assert.IsTrue(TreeSlopes(grid, wrap, 3, 1) == 225);
                Assert.IsTrue(part2Result == 1115775000);
            }
            Console.WriteLine("Total Amount of trees for Part 1 is: " + TreeSlopes(grid, wrap, 3, 1));
            Console.WriteLine($"Total Amount of trees for Part 2 is: {part2Result}");
        }

        public int TreeSlopes(string[] grid, int wrap, int dx, int dy)
        {
            int treeAmount = 0;
            int xCoord = 0;
            for (int y = 0; y < grid.Length; y += dy)
            {
                if (grid[y][xCoord % wrap] == '#')
                    treeAmount++;
                xCoord += dx;
            }

            return treeAmount;
        }
    }
}
