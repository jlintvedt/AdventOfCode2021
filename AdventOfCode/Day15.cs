using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/15
    /// </summary>
    public class Day15
    {
        public class ChitonNavigation
        {
            private readonly int[,] riskLevel;
            private readonly int[,] totalRisk;
            private readonly bool[,] visited;
            private readonly int height, width;
            private static readonly List<(int y, int x)> offsets = new List<(int y, int x)>() { (-1, 0), (1, 0), (0, -1), (0, 1) };
            private readonly PriorityQueue<(int y, int x), int> unvisited = new PriorityQueue<(int y, int x), int>();

            public ChitonNavigation(string input, bool largeMap = false)
            {
                var lines = input.Split(Environment.NewLine);
                height = largeMap ? lines.Length * 5 : lines.Length;
                width = largeMap ? lines[0].Length * 5 : lines[0].Length;

                riskLevel = new int[height, width];
                totalRisk = new int[height, width];
                visited = new bool[height, width];

                for (int y = 0; y < lines.Length; y++)
                {
                    for (int x = 0; x < lines[0].Length; x++)
                    {
                        riskLevel[y, x] = (int)Char.GetNumericValue(lines[y][x]);
                    }
                }

                if (largeMap)
                    CreateLargeMap();
            }

            private void CreateLargeMap()
            {
                var h = height / 5;
                var w = width / 5;

                // Iterate over 'extra' maps
                for (int offY = 0; offY < 5; offY++)
                {
                    for (int offX = 0; offX < 5; offX++)
                    {
                        if (offY == 0 && offX == 0)
                            continue; // Skip re-calculating original map

                        var origin = (y: offY * h, x: offX * w);
                        var offset = offY + offX;

                        // Iterate over positions in 'extra' map
                        for (int y = 0; y < h; y++)
                        {
                            for (int x = 0; x < w; x++)
                            {
                                var risk = (riskLevel[y, x] + offset);
                                risk = risk >= 10 ? risk % 10 + 1 : risk;
                                riskLevel[origin.y + y, origin.x + x] = risk;
                            }
                        }
                    }
                }
            }

            public int FindLeastRiskyPath()
            {
                CalculateTotalRiskLevelForEntireCave();

                return totalRisk[height - 1, width - 1];
            }

            private void CalculateTotalRiskLevelForEntireCave()
            {
                unvisited.Enqueue((0, 0), 0);

                while (unvisited.Count > 0)
                {
                    UpdatePositionAndMoveToNext(unvisited.Dequeue());
                }
            }

            private void UpdatePositionAndMoveToNext((int y, int x) pos)
            {
                // Need to make sure this node has not been computed previously
                if (visited[pos.y, pos.x])
                    return;

                visited[pos.y, pos.x] = true;
                var posRisk = totalRisk[pos.y, pos.x];

                foreach (var offset in offsets)
                {
                    var n = (y: pos.y + offset.y, x: pos.x + offset.x);

                    // Check if valid unvisited neighbour
                    if (n.y < 0 || n.y >= width || n.x < 0 || n.x >= height)
                        continue;
                    if (visited[n.y, n.x])
                        continue;

                    // Check if new lowest risk path is found
                    var nRisk = riskLevel[n.y, n.x];
                    var nTotalRisk = totalRisk[n.y, n.x];
                    if (nTotalRisk == 0 || posRisk + nRisk < nTotalRisk) // 0=unset/infinity
                    {
                        nTotalRisk = posRisk + nRisk;
                        totalRisk[n.y, n.x] = nTotalRisk;
                        unvisited.Enqueue(n, nTotalRisk);
                    }
                }
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var cn = new ChitonNavigation(input);

            return cn.FindLeastRiskyPath().ToString(); ;
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var cn = new ChitonNavigation(input, largeMap: true);

            return cn.FindLeastRiskyPath().ToString(); ;
        }
    }
}
