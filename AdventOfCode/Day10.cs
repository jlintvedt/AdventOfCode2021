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
            private Dictionary<char, char> chunkPairs = new Dictionary<char, char>() {
                { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' }
            };
            private Dictionary<char, int> symbolValue = new Dictionary<char, int>() {
                { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 }
            };

            public SyntaxScoring(string input)
            {
                lines = input.Split(Environment.NewLine).ToList();
            }

            public int CalculateSyntaxErrorScore()
            {
                var score = 0;

                foreach (var line in lines)
                {
                    if (!CheckIfLineIsValid(line, out int errorScore))
                    {
                        score += errorScore;
                    }
                }

                return score;
            }

            private bool CheckIfLineIsValid(string line, out int errorScore)
            {
                var open = new Stack<char>();

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
                        errorScore = symbolValue[symbol];
                        return false;
                    }
                }

                errorScore = 0;
                return true;
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
            return "Puzzle2";
        }
    }
}
