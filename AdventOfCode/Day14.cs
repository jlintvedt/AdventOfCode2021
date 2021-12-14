using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/14
    /// </summary>
    public class Day14
    {
        public class ExtendedPolymerization
        {
            private Dictionary<(char first, char second), long> elementPairs = new Dictionary<(char first, char second), long>();
            private readonly Dictionary<(char, char), char> instructions = new Dictionary<(char, char), char> ();
            private readonly Dictionary<char, long> elementCount = new Dictionary<char, long>();

            public ExtendedPolymerization(string input)
            {
                var parts = input.Split(new string[] { $"{Environment.NewLine}{Environment.NewLine}" }, StringSplitOptions.None);

                var start = parts[0];
                IncreaseElementCount(start[0], 1);
                for (int i = 0; i < start.Length-1; i++)
                {
                    var pair = (start[i], start[i + 1]);
                    IncreaseCount(elementPairs, pair, 1);
                    IncreaseElementCount(start[i+1], 1);
                }

                foreach (var inst in parts[1].Split(Environment.NewLine))
                {
                    instructions.Add((inst[0], inst[1]), inst[6]);
                }
            }

            public long FindElmentDiff(int steps = 10)
            {
                for (int s = 0; s < steps; s++)
                {
                    ExecuteInstructions();
                }

                return elementCount.Values.Max() - elementCount.Values.Min();
            }

            private void ExecuteInstructions()
            {
                var newPairs = new Dictionary<(char, char), long> ();

                foreach (var (pair, count) in elementPairs)
                {
                    if (instructions.TryGetValue(pair, out char element))
                    {
                        IncreaseCount(newPairs, (pair.first, element), count);
                        IncreaseCount(newPairs, (element, pair.second), count);
                        IncreaseElementCount(element, count);
                    }
                    else
                    {
                        newPairs.Add(pair, count);
                    }
                }

                elementPairs = newPairs;
            }

            private void IncreaseElementCount(char element, long value)
            {
                if (elementCount.ContainsKey(element))
                    elementCount[element] += value;
                else
                    elementCount[element] = value;
            }

            private void IncreaseCount(Dictionary<(char, char), long> dict, (char, char) pair, long value)
            {
                if (dict.ContainsKey(pair))
                    dict[pair] += value;
                else
                    dict[pair] = value;
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var ep = new ExtendedPolymerization(input);

            return ep.FindElmentDiff(10).ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var ep = new ExtendedPolymerization(input);

            return ep.FindElmentDiff(40).ToString();
        }
    }
}
