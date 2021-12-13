using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/13
    /// </summary>
    public class Day13
    {
        public class TransparentOrigami
        {
            private HashSet<(int x, int y)> dots;
            private readonly List<(string axis, int pos)> instructions;

            public TransparentOrigami (string input)
            {
                var parts = input.Split(new string[] { $"{Environment.NewLine}{Environment.NewLine}" }, StringSplitOptions.None);
                dots = new HashSet<(int x, int y)>();
                instructions = new List<(string axit, int pos)>();

                foreach (var line in parts[0].Split(Environment.NewLine))
                {
                    var coord = line.Split(',');
                    var dot = (int.Parse(coord[0]), int.Parse(coord[1]));
                    dots.Add(dot);
                }

                foreach (var line in parts[1].Split(Environment.NewLine))
                {
                    var inst = line.Split(' ')[2].Split('=');
                    var instruction = (inst[0], int.Parse(inst[1]));
                    instructions.Add(instruction);
                }
            }

            public int VisibleDotaAfterFirstFold()
            {
                FoldSheet(instructions[0].axis, instructions[0].pos);
                return dots.Count;
            }

            private void FoldSheet(string axis, int pos)
            {
                var tmp = new HashSet<(int x, int y)>();
                // Horizontal fold
                if (axis == "y")
                {
                    foreach (var dot in dots)
                    {
                        if (dot.y < pos)
                        {
                            tmp.Add(dot);
                        }
                        else
                        {
                            tmp.Add((dot.x, (pos * 2) - dot.y));
                        }
                        if (dot.y > pos*2)
                        {
                            throw new Exception("don't");
                        }
                    }
                }
                // Vertical fold
                else
                {
                    foreach (var dot in dots)
                    {
                        if (dot.x < pos)
                        {
                            tmp.Add(dot);
                        }
                        else
                        {
                            tmp.Add(((pos * 2) - dot.x, dot.y));
                        }
                        if (dot.x > pos * 2)
                        {
                            throw new Exception("don't");
                        }
                    }
                }

                dots = tmp;
            }
        }


        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var to = new TransparentOrigami (input);

            return to.VisibleDotaAfterFirstFold().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return "Puzzle2";
        }
    }
}
