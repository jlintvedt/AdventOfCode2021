using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/8
    /// </summary>
    public class Day08
    {
        public class AA
        {
            private List<(List<Digit> patterns, List<Digit> output)> entries;

            public AA(string input)
            {
                entries = new List<(List<Digit> patterns,List<Digit> output)> ();
                var lines = input.Split(Environment.NewLine);

                foreach (var line in lines)
                {
                    var parts = line.Split(" | ");

                    var patterns = new List<Digit>();
                    foreach (var raw in parts[0].Split(' '))
                    {
                        patterns.Add(new Digit(raw));
                    }

                    var output = new List<Digit>();
                    foreach (var raw in parts[1].Split(' '))
                    {
                        output.Add(new Digit(raw));
                    }

                    entries.Add((patterns, output));
                }
            }

            public int CountUniqueNumOutputDigits()
            {
                var numDigits = 0;

                foreach (var entry in entries)
                {
                    foreach (var digit in entry.output)
                    {
                        if (digit.NumSegments == 2 || digit.NumSegments == 4 || digit.NumSegments == 3 || digit.NumSegments == 7)
                        {
                            numDigits++;
                        }
                    }
                }

                return numDigits;
            }

            public class Digit
            {
                public int NumSegments;
                public bool[] Segments = new bool[7];

                public Digit(string input)
                {
                    foreach (var seg in input)
                    {
                        switch (seg)
                        {
                            case 'a': Segments[(int)Segment.a] = true; break;
                            case 'b': Segments[(int)Segment.b] = true; break;
                            case 'c': Segments[(int)Segment.c] = true; break;
                            case 'd': Segments[(int)Segment.d] = true; break;
                            case 'e': Segments[(int)Segment.e] = true; break;
                            case 'f': Segments[(int)Segment.f] = true; break;
                            case 'g': Segments[(int)Segment.g] = true; break;
                            default: throw new ArgumentException($"Unknown segment {seg} in {input}");
                        }

                        NumSegments++;
                    }
                }
            }

            public enum Segment {
                a,
                b,
                c,
                d,
                e,
                f,
                g,
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var aa = new AA(input);

            return aa.CountUniqueNumOutputDigits().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return "Puzzle2";
        }
    }
}
