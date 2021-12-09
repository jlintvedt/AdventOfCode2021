using System;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/9
    /// </summary>
    public class Day09
    {
        public class SmokeBasin
        {
            private int[,] heightmap;
            private int height, width;

            public SmokeBasin (string input)
            {
                var lines = input.Split (Environment.NewLine);
                height = lines.Length;
                width = lines[0].Length;
                heightmap = new int[height, width];

                for (int y = 0; y < height; y++)
                {
                    var numbers = Common.Common.ParseStringToIntArray (lines[y]);
                    for (int x = 0; x < width; x++)
                    {
                        heightmap[y, x] = numbers[x];
                    }
                }
            }

            public int CalculateSumOfRiskLevels()
            {
                var totalRiskLevel = 0;

                for (int y = 0;y < height;y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var current = heightmap[y, x];
                        // Up
                        if ((y != 0) && (heightmap[y - 1, x] <= current))
                            continue;
                        // Down
                        if ((y + 1 != height) && (heightmap[y + 1, x] <= current))
                            continue;
                        // Left
                        if ((x != 0) && (heightmap[y, x - 1] <= current))
                            continue;
                        // Right
                        if ((x + 1 != width) && (heightmap[y, x + 1] <= current))
                            continue;

                        // Low Point found
                        totalRiskLevel += current + 1;
                    }
                }

                return totalRiskLevel;
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var sb = new SmokeBasin (input);

            return sb.CalculateSumOfRiskLevels().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return "Puzzle2";
        }
    }
}
