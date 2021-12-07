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
            private int MaxPos;
            private int[] CostToMove;
            private bool UseScalingFuelCost;

            public CrabAlligner(string input, bool scalingFuelCost = false)
            {
                CrabPositions = Common.Common.ParseStringToIntArray(input, ",");
                MaxPos = CrabPositions.Max();

                UseScalingFuelCost = scalingFuelCost;
                if (UseScalingFuelCost)
                {
                    CostToMove = new int[MaxPos+1];
                    var cost = 0;
                    var increase = 0;
                    for (int i = 0; i < MaxPos+1; i++)
                    {
                        cost += increase++;
                        CostToMove[i] = cost;
                    }
                }
            }

            public long CalculateAllignmentCost()
            {
                var bestPos = MaxPos/2;
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

            private long CalculateCostForPos(int pos)
            {
                long cost = 0;

                foreach (var crab in CrabPositions)
                {
                    if (!UseScalingFuelCost)
                    {
                        cost += Math.Abs(pos - crab);
                    }
                    else
                    {
                        cost += CostToMove[Math.Abs(pos - crab)];
                    }
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
            var ca = new CrabAlligner(input, scalingFuelCost: true);

            return ca.CalculateAllignmentCost().ToString();
        }
    }
}
