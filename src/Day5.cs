using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Console.WriteLine($"The highest seatID is {maxSeatId}");
        }

        private class BoardingPass
        {
            private int row, column;
            private const int maxColumn = 7, maxRow = 128;
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
                        upper -= ((upper - lower) / 2);
                    else if (input[i] == upperRange)
                        lower += ((upper - lower) / 2);
                }
                if (input[input.Length -1] == upperRange)
                    return upper;
                else
                    return lower;
             
            }
        }
    }
}
