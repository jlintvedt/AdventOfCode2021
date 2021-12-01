using System;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/1
    /// </summary>
    public class Day01
    {
        public class SonarSweep
        {
            private readonly int[] Depths;

            public SonarSweep(string rawDepths)
            {
                Depths = Common.Common.ParseStringToIntArray(rawDepths, delim:Environment.NewLine);
            }

            public int CountIncrements()
            {
                var numInc = 0;
                var prev = int.MaxValue;

                foreach (var depth in Depths)
                {
                    if (depth > prev)
                        numInc++;

                    prev = depth;
                }

                return numInc;
            }

            public int CountSlidingWindowIncrements()
            {
                var numInc = 0;
                var prevSum = int.MaxValue;

                for (int i = 0; i < Depths.Length-2; i++)
                {
                    var nextSum = Depths[i] + Depths[i + 1] + Depths[i + 2];
                    
                    if (nextSum > prevSum)
                        numInc++;

                    prevSum = nextSum;
                }

                return numInc;
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var ss = new SonarSweep(input);

            return ss.CountIncrements().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var ss = new SonarSweep(input);

            return ss.CountSlidingWindowIncrements().ToString();
        }
    }
}
