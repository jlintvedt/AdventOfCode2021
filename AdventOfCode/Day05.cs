using System;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/05
    /// </summary>
    public class Day05
    {
        public class HydrothermalVents
        {
            private static int Width = 1000;
            private static int Height = 1000;
            private readonly int[,] Vents;
            private bool MapVerticalVents;
            public int NumDangeroudAreas { get; private set; }

            public HydrothermalVents(string input, bool mapVertical = false)
            {
                MapVerticalVents = mapVertical;

                Vents = new int[Width, Height];

                foreach (var line in input.Split(Environment.NewLine))
                {
                    MapRawLine(line);
                }
            }

            private void MapRawLine(string input)
            {
                var parts = input.Split(' ');

                var v1 = parts[0].Split(',');
                var x1 = int.Parse(v1[0]);
                var y1 = int.Parse(v1[1]);

                var v2 = parts[2].Split(',');
                var x2 = int.Parse(v2[0]);
                var y2 = int.Parse(v2[1]);

                MapVent((x1, y1), (x2, y2));
            }

            private void MapVent((int x, int y) start, (int x, int y) stop)
            {
                // Skip non-orthogonal lines
                if (start.x == stop.x) // Vertical
                {
                    var min = start.y < stop.y ? start.y : stop.y;
                    var max = start.y > stop.y ? start.y : stop.y;

                    for (int y = min; y <= max; y++)
                    {
                        if (++Vents[start.x, y] == 2)
                            NumDangeroudAreas++;
                    }
                }
                else if (start.y == stop.y) // Horizontal
                {
                    var min = start.x < stop.x ? start.x : stop.x;
                    var max = start.x > stop.x ? start.x : stop.x;

                    for (int x = min; x <= max; x++)
                    {
                        if (++Vents[x, start.y] == 2)
                            NumDangeroudAreas++;
                    }
                }
                else if (MapVerticalVents) // Diagonal
                {
                    if (((start.x < stop.x) && (start.y < stop.y)) || ((start.x > stop.x) && (start.y > stop.y))) // direction:\
                    {
                        var min = (start.x < stop.x) ? start : stop;
                        var max = (start.x > stop.x) ? start : stop;

                        var y = min.y;
                        for (var x = min.x; x <=max.x; x++)
                        {
                            if (++Vents[x, y++] == 2)
                                NumDangeroudAreas++;
                        }
                    }
                    else // direction:/
                    {
                        var min = (start.x < stop.x) ? start : stop;
                        var max = (start.x > stop.x) ? start : stop;

                        var y = min.y;
                        for (var x = min.x; x <= max.x; x++)
                        {
                            if (++Vents[x, y--] == 2)
                                NumDangeroudAreas++;
                        }
                    }
                }
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var hv = new HydrothermalVents(input);

            return hv.NumDangeroudAreas.ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var aa = new HydrothermalVents(input, mapVertical: true);

            return aa.NumDangeroudAreas.ToString();
        }
    }
}
