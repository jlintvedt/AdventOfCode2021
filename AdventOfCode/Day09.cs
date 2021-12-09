using System;
using System.Collections.Generic;

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
                        if (CheckIfLowPoint(y, x))
                            totalRiskLevel += heightmap[y, x] + 1;
                    }
                }

                return totalRiskLevel;
            }

            public int FindBasinSum()
            {
                var lowPoints = new List<(int y, int x)>();
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (CheckIfLowPoint(y, x))
                            lowPoints.Add((y, x));
                    }
                }

                int l1 = 0, l2 = 0, l3 = 0;
                foreach (var pos in lowPoints)
                {
                    var visited = new HashSet<(int, int)>();
                    FindBasinSizeRec(pos, visited);

                    var size = visited.Count;

                    // Upadate largest 3
                    if (size > l1)
                    {
                        var tmp = l1;
                        l1 = size;
                        size = tmp;
                    }

                    if (size > l2)
                    {
                        var tmp = l2;
                        l2 = size;
                        size = tmp;
                    }

                    if (size > l3)
                    {
                        var tmp = l3;
                        l3 = size;
                        size = tmp;
                    }
                }

                return l1 * l2 * l3;
            }

            private void FindBasinSizeRec((int y, int x) pos, HashSet<(int y, int x)> visited)
            {
                var current = heightmap[pos.y, pos.x];
                // 9's are never part of basins
                if (current == 9)
                    return;

                // Don't re-check visited positions
                if (visited.Contains(pos))
                    return;
                visited.Add(pos);

                // 8's don't need to check their neighbours
                if (current == 8)
                    return;

                // Up
                if (pos.y != 0)
                {
                    FindBasinSizeRec((pos.y - 1, pos.x), visited);
                }
                // Down
                if (pos.y +1 != height)
                {
                    FindBasinSizeRec((pos.y + 1, pos.x), visited);
                }
                // Left
                if (pos.x != 0)
                {
                    FindBasinSizeRec((pos.y, pos.x - 1), visited);
                }
                // Right
                if (pos.x + 1 != width)
                {
                    FindBasinSizeRec((pos.y, pos.x + 1), visited);
                }
            }

            private bool CheckIfLowPoint(int y, int x)
            {
                var current = heightmap[y, x];
                // Up
                if ((y != 0) && (heightmap[y - 1, x] <= current))
                    return false;
                // Down
                if ((y + 1 != height) && (heightmap[y + 1, x] <= current))
                    return false;
                // Left
                if ((x != 0) && (heightmap[y, x - 1] <= current))
                    return false;
                // Right
                if ((x + 1 != width) && (heightmap[y, x + 1] <= current))
                    return false;

                // Low Point found
                return true;
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
            var sb = new SmokeBasin(input);

            return sb.FindBasinSum().ToString();
        }
    }
}
