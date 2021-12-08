using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/8
    /// </summary>
    public class Day08
    {
        public class SevenSegmentDisplay
        {
            private readonly List<Display> displays;

            public SevenSegmentDisplay(string input)
            {
                displays = new List<Display>();
                var lines = input.Split(Environment.NewLine);

                foreach (var line in lines)
                {
                    displays.Add(new Display(line));
                }
            }

            public int CountUniqueNumOutputDigits()
            {
                var numDigits = 0;

                foreach (var display in displays)
                {
                    foreach (var digit in display.output)
                    {
                        if (digit.NumWires == 2 || digit.NumWires == 4 || digit.NumWires == 3 || digit.NumWires == 7)
                        {
                            numDigits++;
                        }
                    }
                }

                return numDigits;
            }

            public long SumOfAllOutputValues()
            {
                long total = 0;

                foreach (var display in displays)
                {
                    total += display.DecodeValue();
                }

                return total;
            }

            public class Display
            {
                public List<Digit> patterns;
                public List<Digit> output;

                private static readonly HashSet<Wire> allWires = new HashSet<Wire>(7) { Wire.a, Wire.b, Wire.c, Wire.d, Wire.e, Wire.f, Wire.g };
                private HashSet<Wire>[] possibleWires;
                private Wire[] confirmedWires; // The wire corresponding to each position [0,6]
                private Dictionary<int, HashSet<Wire>> ConfirmedDigits;

                public Display(string input)
                {
                    var parts = input.Split(" | ");

                    patterns = new List<Digit>();
                    foreach (var raw in parts[0].Split(' '))
                    {
                        patterns.Add(new Digit(raw));
                    }

                    output = new List<Digit>();
                    foreach (var raw in parts[1].Split(' '))
                    {
                        output.Add(new Digit(raw));
                    }
                }

                public int DecodeValue()
                {
                    // Init
                    confirmedWires = new Wire[7];
                    ConfirmedDigits = new Dictionary<int, HashSet<Wire>>();

                    possibleWires = new HashSet<Wire>[7];
                    for (int i = 0; i < 7; i++)
                    {
                        possibleWires[i] = new HashSet<Wire>(7) {Wire.a, Wire.b, Wire.c, Wire.d, Wire.e, Wire.f, Wire.g};
                    }

                    //patterns.AddRange(output); // Also add output digits as they are valid patterns

                    // Decode
                    ProcessDigitsWithSetValue();

                    // Process by inference
                    FindAndProcessDigits(0, 1, byExclusion: false);



                    // Clean-up
                    possibleWires = null;
                    confirmedWires = null;
                    ConfirmedDigits = null;

                    return 0;
                }

                private void FindAndProcessDigits(int target, int mask, bool byExclusion)
                {
                    var maskWires = ConfirmedDigits[mask];
                    var matchFound = false;

                    foreach (var digit in patterns.Where(d => d.NumWires == SevenSegmentDisplay.ValueSegmentPositions[target].Count))
                    {
                        if (byExclusion)
                        {
                            if (digit.CheckValueByExclusion(maskWires, target))
                                matchFound = true;
                        }
                        else
                        {
                            if (digit.CheckValueByInclusion(maskWires, target))
                                matchFound = true;
                        }
                    }

                    if (matchFound)
                        ProcessDigitsWithSetValue();
                    else
                        throw new Exception("Pattern not found");
                }

                private void ProcessDigitsWithSetValue()
                {
                    var remove = new List<Digit>();

                    foreach (var digit in patterns)
                    {
                        if (digit.Value < 0)
                            continue;

                        ProcessKnownDigit(digit, skipRemove: true);
                        remove.Add(digit);
                    }

                    foreach (var digit in remove)
                    {
                        patterns.Remove(digit);
                    }
                }

                private void ProcessKnownDigit(Digit digit, bool skipRemove = false)
                {
                    if (digit.Value < 0)
                        throw new Exception("Trying to process a 'KnownDigit' without set value");

                    if (ConfirmedDigits.ContainsKey(digit.Value))
                        return;

                    foreach (var pos in SevenSegmentDisplay.ValueSegmentPositions[digit.Value])
                    {
                        possibleWires[pos].IntersectWith(digit.Wires);
                    }

                    foreach (var pos in SevenSegmentDisplay.ValueSegmentPositionsEmpty[digit.Value])
                    {
                        possibleWires[pos].ExceptWith(digit.Wires);
                    }

                    ConfirmedDigits.Add(digit.Value, digit.Wires);

                    if (!skipRemove)
                    {
                        patterns.Remove(digit);
                    }
                }
            }

            public class Digit
            {
                public int NumWires;
                public HashSet<Wire> Wires;
                public int Value = -1;

                public Digit(string input)
                {
                    Wires = new HashSet<Wire>();
                    foreach (var wire in input)
                    {
                        switch (wire)
                        {
                            case 'a': Wires.Add(Wire.a); break;
                            case 'b': Wires.Add(Wire.b); break;
                            case 'c': Wires.Add(Wire.c); break;
                            case 'd': Wires.Add(Wire.d); break;
                            case 'e': Wires.Add(Wire.e); break;
                            case 'f': Wires.Add(Wire.f); break;
                            case 'g': Wires.Add(Wire.g); break;
                            default: throw new ArgumentException($"Unknown wire '{wire}' in \"{input}\"");
                        }

                        NumWires++;
                    }

                    switch (NumWires)
                    {
                        case 2: Value = 1; break;
                        case 4: Value = 4; break;
                        case 3: Value = 7; break;
                        case 7: Value = 8; break;
                        default: break;
                    }
                }

                public bool CheckValueByExclusion(HashSet<Wire> mustNotInclude, int valueToSet)
                {
                    if (Wires.Union(mustNotInclude).Any())
                        return false;

                    Value = valueToSet;
                    return true;
                }

                public bool CheckValueByInclusion(HashSet<Wire> mustInclude, int valueToSet)
                {
                    if (!Wires.IsSupersetOf(mustInclude))
                        return false;

                    Value = valueToSet;
                    return true;
                }
            }

            public enum Wire {
                x,
                a,
                b,
                c,
                d,
                e,
                f,
                g,
            }

            /// Segment positions
            ///  000
            /// 1   2
            ///  333
            /// 4   5
            ///  666
            public static List<List<int>> ValueSegmentPositions = new List<List<int>>() {
                new List<int>() { 0, 1, 2, 4, 5, 6 },    // 0
                new List<int>() { 2, 5 },                // 1
                new List<int>() { 0, 2, 3, 4, 6 },       // 2
                new List<int>() { 0, 2, 3, 5, 6 },       // 3
                new List<int>() { 1, 2, 3, 5 },          // 4
                new List<int>() { 0, 1, 3, 5, 6 },       // 5
                new List<int>() { 0, 1, 3, 4, 5, 6 },    // 6
                new List<int>() { 0, 2, 5 },             // 7
                new List<int>() { 0, 1, 2, 3, 4, 5, 6 }, // 8
                new List<int>() { 0, 1, 2, 3, 5, 6 },    // 9
            };

            public static List<List<int>> ValueSegmentPositionsEmpty = new List<List<int>>() {
                new List<int>() { 3 },             // 0
                new List<int>() { 0, 1, 3, 4, 6 }, // 1
                new List<int>() { 1, 5 },          // 2
                new List<int>() { 1, 4 },          // 3
                new List<int>() { 0, 4, 6 },       // 4
                new List<int>() { 2, 4 },          // 5
                new List<int>() { 2 },             // 6
                new List<int>() { 1, 3, 4, 6 },    // 7
                new List<int>() { },               // 8
                new List<int>() { 4 },             // 9
            };
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var ssd = new SevenSegmentDisplay(input);

            return ssd.CountUniqueNumOutputDigits().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var ssd = new SevenSegmentDisplay(input);

            return ssd.SumOfAllOutputValues().ToString();
        }
    }
}
