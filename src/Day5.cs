using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Lightning2x.AdventOfCode2020
{
    public class Day5 : IDay
    {
        public void Run(string path)
        {
            List<BoardingPass> boardingPasses = Utils.TypeParser(path, BoardingPass.Parse);
            int maxSeatId = 0;
            foreach (BoardingPass b in boardingPasses)
                if (b.SeatID > maxSeatId)
                    maxSeatId = b.SeatID;
            if(Utils.NUnitEnabled)
            {
                Assert.IsTrue(maxSeatId == 871);
                Assert.IsTrue(FindSeatId(boardingPasses) == 640);
            }
            Console.WriteLine($"The highest seatID is {maxSeatId}");
            Console.WriteLine("My seatID is " + FindSeatId(boardingPasses));

        }

        private int FindSeatId(List<BoardingPass> boardingPasses)
        {
            List<int> boardingPassesIDs = boardingPasses.Select(x => x.SeatID).ToList();
            boardingPassesIDs.Sort();
            List<int> allSeatIDs = Enumerable.Range(boardingPassesIDs.First(), boardingPassesIDs.Last() - boardingPassesIDs.First()).ToList();
            return allSeatIDs.Except(boardingPassesIDs).First();
        }
        private class BoardingPass : IComparable
        {
            private int row, column;
            public const int maxColumn = 8, maxRow = 128;
            public BoardingPass(int _row, int _column)
            {
                row = _row;
                column = _column;
            }

            public int SeatID => row * 8 + column;
            

            public static BoardingPass Parse(string input)
            {
                if (String.IsNullOrWhiteSpace(input)) throw new ArgumentException(input);
                if (input.Length != 10)
                    throw new ArgumentException("Wrong length for a boarding pass");
                BoardingPass boardingPass = null;
                string rowString = input.Substring(0, 7);
                string columnString = input.Substring(7, 3);
                
                boardingPass = new BoardingPass(
                    RangeHelper(rowString, 'F', 'B', maxRow),
                    RangeHelper(columnString, 'L', 'R', maxColumn));

                return boardingPass;
            }

            private static int RangeHelper(string input, char lowerRange, char upperRange, int maxRange)
            {

                int upper = maxRange, lower = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == lowerRange)
                        upper -= (upper - lower) / 2;
                    else if (input[i] == upperRange)
                        lower += (upper - lower) / 2;
                }
                return lower;
             
            }

            public int CompareTo(object obj)
            {
                return this.SeatID - ((BoardingPass)obj).SeatID;
            }
        }
    }
}
