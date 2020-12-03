using System;
using System.Collections.Generic;
using System.Text;

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
            int treeAmount = 0;
            int xCoord = 0;
            for (int y = 0; y < grid.Length; y++)
            {
                if (grid[y][xCoord % wrap] == '#')
                    treeAmount++;
                xCoord += 3;
            }
            Console.WriteLine($"Total Amount of trees for Part 1 is: {treeAmount}");
        }
    }
}
