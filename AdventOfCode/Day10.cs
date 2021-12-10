using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/10
    /// </summary>
    public class Day10
    {
        public class SyntaxScoring
        {
            private List<string> lines;
            private static readonly Dictionary<char, char> chunkPairs = new Dictionary<char, char>() {
                { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' }
            };
            private static readonly Dictionary<char, int> illegalValue = new Dictionary<char, int>() {
                { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 }
            };
            private static readonly Dictionary<char, int> autocompleteValue = new Dictionary<char, int>() {
                { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 }
            };

            public SyntaxScoring(string input)
            {
                lines = input.Split(Environment.NewLine).ToList();
            }

            public long CalculateSyntaxErrorScore()
            {
                long score = 0;

                foreach (var line in lines)
                {
                    var ls = CheckLineScore(line, out bool isIllegal);
                    if (isIllegal)
                        score += ls;
                }

                return score;
            }

            public long CalculateAutocompleteScore()
            {
                var scores = new List<long>();

                foreach (var line in lines)
                {
                    var ls = CheckLineScore(line, out bool isIllegal);
                    if (!isIllegal)
                        scores.Add(ls);
                }

                scores.Sort();
                return scores[(scores.Count / 2)];
            }

            private long CheckLineScore(string line, out bool isCorrupt)
            {
                var open = new Stack<char>();
                isCorrupt = false;

                // Check if line is corrupt
                foreach (var symbol in line)
                {
                    // Open
                    if (chunkPairs.ContainsKey(symbol))
                    {
                        open.Push(symbol);
                        continue;
                    }
                    // Close
                    var lastOpen = open.Pop();
                    if (!(chunkPairs[lastOpen] == symbol))
                    {
                        isCorrupt = true;
                        return illegalValue[symbol];
                    }
                }

                // Line is not corrupt, calculate auto-complete score
                long score = 0;
                foreach (var symbol in open)
                {
                    score *= 5;
                    score += autocompleteValue[symbol];
                }

                return score;
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var ss = new SyntaxScoring(input);

            return ss.CalculateSyntaxErrorScore().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var ss = new SyntaxScoring(input);

            return ss.CalculateAutocompleteScore().ToString();
        }
    }
}
