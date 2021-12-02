using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/02
    /// </summary>
    public class Day02
    {
        public class SubmarineDive
        {
            private int distance = 0;
            private int depth = 0;
            private List<string> moves;

            public SubmarineDive(string input)
            {
                moves = input.Split(Environment.NewLine).ToList();
            }

            public int ExecuteMoves()
            {
                foreach (var move in moves)
                {
                    ApplyMoveCommand(move);
                }

                return distance * depth;
            }

            private void ApplyMoveCommand(string command)
            {
                var parts = command.Split(' ');

                switch (parts[0])
                {
                    case "forward":
                        distance += int.Parse(parts[1]);
                        break;
                    case "down":
                        depth += int.Parse(parts[1]);
                        break;
                    case "up":
                        depth -= int.Parse(parts[1]);
                        break;
                    default:
                        throw new ArgumentException($"Unknown move {command}");
                }
            }
        } 

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var sd = new SubmarineDive(input);

            return sd.ExecuteMoves().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var sd = new SubmarineDive(input);

            return "Puzzle2";
        }
    }
}
