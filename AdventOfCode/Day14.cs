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
            private readonly List<char> elements;
            private Dictionary<string, char> instructions = new Dictionary<string, char> ();
            private readonly Dictionary<char, int> elementCount = new Dictionary<char, int>();
            private Queue<(int pos, char elem)> newElements;

            public ExtendedPolymerization(string input)
            {
                var parts = input.Split(new string[] { $"{Environment.NewLine}{Environment.NewLine}" }, StringSplitOptions.None);
                elements = parts[0].ToList();

                foreach (var e in elements)
                {
                    IncreaseElementCount(e, 1);
                }

                foreach (var inst in parts[1].Split(Environment.NewLine))
                {
                    instructions.Add($"{inst[0]}{inst[1]}", inst[6]);
                }
            }

            public int FindElmentDiff(int steps = 10)
            {
                // Execute steps
                for (int s = 0; s < steps; s++)
                {
                    ExecuteInstructions();
                }

                // Calculate diff
                return elementCount.Values.Max() - elementCount.Values.Min();
            }

            private void ExecuteInstructions()
            {
                newElements = new Queue<(int pos, char elem)>();

                for (int i = 0; i < elements.Count-1; i++)
                {
                    var pair = $"{elements[i]}{elements[i + 1]}";

                    if (instructions.TryGetValue(pair, out char insert))
                    {
                        newElements.Enqueue((i+1+newElements.Count, insert));
                    }
                }

                while (newElements.Count > 0)
                {
                    var (pos, elem) = newElements.Dequeue();
                    IncreaseElementCount(elem, 1);

                    elements.Insert(pos, elem);
                }
            }

            private void IncreaseElementCount(char element, int value)
            {
                if (elementCount.ContainsKey(element))
                {
                    elementCount[element] += value;
                }
                else
                {
                    elementCount[element] = value;
                }
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var ep = new ExtendedPolymerization(input);

            return ep.FindElmentDiff().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return "Puzzle2";
        }
    }
}
