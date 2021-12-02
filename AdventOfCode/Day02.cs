using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/2
    /// </summary>
    public class Day02
    {
        public class SubmarineDive
        {
            private int horizontal = 0;
            private int depth = 0;
            private int aim = 0;
            private List<(Direction direction, int value)> Commands;

            public SubmarineDive(string input)
            {
                Commands = Common.Common.ParseStringToListOfEnumIntTuples<Direction>(input, Environment.NewLine);
            }

            public int ExecuteMoves(bool manualMode = false)
            {
                foreach (var command in Commands)
                {
                    if (!manualMode)
                        ApplyMoveCommand(command);
                    else
                        ApplyMoveCommandManually(command);
                }

                return horizontal * depth;
            }

            private void ApplyMoveCommand((Direction dir, int val) command)
            {
                switch (command.dir)
                {
                    case Direction.forward:
                        horizontal += command.val;
                        break;
                    case Direction.down:
                        depth += command.val;
                        break;
                    case Direction.up:
                        depth -= command.val;
                        break;
                    default:
                        break;
                }
            }

            private void ApplyMoveCommandManually((Direction dir, int val) command)
            {
                switch (command.dir)
                {
                    case Direction.forward:
                        horizontal += command.val;
                        depth += command.val * aim;
                        break;
                    case Direction.down:
                        aim += command.val;
                        break;
                    case Direction.up:
                        aim -= command.val;
                        break;
                    default:
                        break;
                }
            }

            private enum Direction
            {
                forward,
                down,
                up
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

            return sd.ExecuteMoves(manualMode: true).ToString();
        }
    }
}
