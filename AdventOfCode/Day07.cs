using System;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/7
    /// </summary>
    public class Day07
    {
        public class CrabAlligner
        {
            private int[] CrabPositions;

            public CrabAlligner(string input)
            {
                CrabPositions = Common.Common.ParseStringToIntArray(input, ",");
            }

            public long CalculateAllignmentCost()
            {
                var bestPos = CrabPositions.Max()/2;
                var bestCost = CalculateCostForPos(bestPos);
                var range = bestPos / 2;

                do
                {
                    var lower = CalculateCostForPos(bestPos - range);
                    if (lower < bestCost)
                    {
                        bestPos -= range;
                        bestCost = lower;
                        continue;
                    }

                    var higher = CalculateCostForPos(bestPos + range);
                    if (higher < bestCost)
                    {
                        bestPos += range;
                        bestCost = higher;
                        continue;
                    }

                    range /= 2;

                    if (range == 0)
                    {
                        break;
                    }
                } while (true);

                return bestCost;
            }

            public long CalculateCostForPos(int pos)
            {
                long cost = 0;

                foreach (var crab in CrabPositions)
                {
                    cost += Math.Abs(pos - crab);
                }

                return cost;
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var ca = new CrabAlligner(input);

            return ca.CalculateAllignmentCost().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var ca = new CrabAlligner(input);

            return "Puzzle2";
        }
    }
}
