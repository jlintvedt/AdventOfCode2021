using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/01
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
                var prev = Depths.First();

                foreach (var depth in Depths.Skip(1))
                {
                    if (depth > prev)
                    {
                        numInc++;
                    }
                    prev = depth;
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
            return "Puzzle2";
        }
    }
}
