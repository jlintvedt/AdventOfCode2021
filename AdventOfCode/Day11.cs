using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/11
    /// </summary>
    public class Day11
    {
        public class DumboOctopus
        {
            private readonly int[,] energyLevel;
            private static readonly int height = 10;
            private static readonly int width = 10;
            private readonly HashSet<(int y, int x)> isFlashing;
            private readonly HashSet<(int y, int x)> hasFlashed;
            private static readonly List<(int y, int x)> neighbourOffset = new List<(int y, int x)>(){
                (-1, -1), (-1, 0), (-1, 1), // Above
                (0, -1), (0, 1),            // Sides
                (1, -1), (1, 0), (1, 1)     // Below
            };

            public DumboOctopus (string input)
            {
                energyLevel = new int[10, 10];
                isFlashing = new HashSet<(int y, int x)>();
                hasFlashed = new HashSet<(int y, int x)>();

                var lines = input.Split(Environment.NewLine);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        energyLevel[y,x] = (int)char.GetNumericValue(lines[y][x]);
                    }
                }
            }

            public int FindNumFlashes(int steps = 100)
            {
                var numFlashes = 0;

                for (int step = 0; step < steps; step++)
                {
                    IncreaseEnergyOfAll();
                    numFlashes += FlashAllWithMaximumEnergy();
                    ResetAllFlashed();
                }

                return numFlashes;
            }

            public int FindFirstSynchronizedFlash()
            {
                for (int step = 1; step < 1000; step++)
                {
                    IncreaseEnergyOfAll();
                    FlashAllWithMaximumEnergy();
                    if (hasFlashed.Count == width * height)
                        return step;
                    ResetAllFlashed();
                }

                throw new Exception("Couldn't find any synchronized flash within the first 1000 steps");
            }

            private void IncreaseEnergyOfAll()
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (++energyLevel[y, x] > 9)
                            isFlashing.Add((y, x));
                    }
                }
            }

            private int FlashAllWithMaximumEnergy()
            {
                var numFlashes = 0;
                while (isFlashing.Count > 0)
                {
                    var octo = isFlashing.First();
                    FlashOctopus(octo);
                    isFlashing.Remove(octo);
                    hasFlashed.Add(octo);
                    numFlashes++;
                }

                return numFlashes;
            }

            private void ResetAllFlashed()
            {
                foreach (var (y, x) in hasFlashed)
                    energyLevel[y, x] = 0;

                hasFlashed.Clear();
            }

            private void FlashOctopus((int y, int x) octo)
            {
                foreach (var offset in neighbourOffset)
                {
                    var neighbour = (y: octo.y + offset.y, x: octo.x + offset.x);

                    // Check if widhin grid
                    if (0 > neighbour.y || neighbour.y >= height || 0 > neighbour.x || neighbour.x >= width)
                        continue;

                    if (++energyLevel[neighbour.y, neighbour.x] == 10)
                        isFlashing.Add(neighbour);
                }
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var doc = new DumboOctopus(input);

            return doc.FindNumFlashes().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var doc = new DumboOctopus(input);

            return doc.FindFirstSynchronizedFlash().ToString();
        }
    }
}
