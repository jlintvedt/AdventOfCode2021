using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/1
    /// </summary>
    public class Day01
    {
        public class SubmarineSonar
        {
            private List<int> Depths;

            public SubmarineSonar(string[] depths)
            {
                Depths = new List<int>();

                foreach (var depth in depths)
                {
                    Depths.Add(int.Parse(depth));
                }
            }

            public int CountIncrements()
            {
                var numInc = 0;
                var prev = int.MaxValue;

                foreach (var depth in Depths)
                {
                    if (depth > prev)
                    {
                        numInc++;
                    }

                    prev = depth;
                }

                return numInc;
            }

            public int CountSlidingWindowIncrements()
            {
                var numInc = 0;
                var prevSum = int.MaxValue;

                for (int i = 0; i < Depths.Count-2; i++)
                {
                    var nextSum = Depths[i] + Depths[i + 1] + Depths[i + 2];
                    if (nextSum > prevSum)
                    {
                        numInc++;
                    }

                    prevSum = nextSum;
                }

                return numInc;
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var depths = input.Split(Environment.NewLine);

            var ss = new SubmarineSonar(depths);

            return ss.CountIncrements().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var depths = input.Split(Environment.NewLine);

            var ss = new SubmarineSonar(depths);

            return ss.CountSlidingWindowIncrements().ToString();
        }
    }
}
