using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/04
    /// </summary>
    public class Day04
    {
        public class BingoSubsystem
        {
            private Queue<int> NumberDraws = new Queue<int>();
            private List<BingoBoard> BingoBoards = new List<BingoBoard>();

            public BingoSubsystem(string input)
            {
                var sections = input.Split(new string[] { $"{Environment.NewLine}{Environment.NewLine}" }, StringSplitOptions.None);

                // Parse numbers
                foreach (var num in sections[0].Split(','))
                {
                    NumberDraws.Enqueue(int.Parse(num));
                }

                // Parse boards
                for (int i = 1; i < sections.Length; i++)
                {
                    BingoBoards.Add(new BingoBoard(sections[i]));
                }
            }

            public int FindWinningNumber()
            {
                while (NumberDraws.Count > 0)
                {
                    var number = NumberDraws.Dequeue();

                    foreach (var board in BingoBoards)
                    {
                        if (board.MarkNumber(number))
                        {
                            return number * board.FindSumOfUnmarkedNumbers();
                        }
                    }
                }

                throw new Exception("Couldn't find any winning boards");
            }

            public class BingoBoard
            {
                private Dictionary<int, (int y, int x)> Numbers;
                private bool[,] Marked;

                public BingoBoard(string input)
                {
                    Numbers = new Dictionary<int, (int, int)>();
                    Marked = new bool[5,5];

                    var lines = input.Split(Environment.NewLine);
                    for (int i = 0; i < 5; i++)
                    {
                        var numbers = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < 5; j++)
                        {
                            Numbers.Add(int.Parse(numbers[j]), (i, j));
                        }
                    }
                }

                public bool MarkNumber(int number)
                {
                    if (!Numbers.TryGetValue(number, out (int y, int x) pos))
                        return false;

                    Marked[pos.y, pos.x] = true;

                    // Check for complete column/row
                    var isWinner = true;
                    for (int x = 0; x < 5; x++)
                    {
                        if (!Marked[pos.y, x])
                        {
                            isWinner = false;
                            break;
                        }
                    }

                    if (isWinner)
                        return true;

                    for (int y = 0; y < 5; y++)
                    {
                        if (!Marked[y, pos.x])
                            return false;
                    }

                    return true;
                }

                public int FindSumOfUnmarkedNumbers()
                {
                    int sum = 0;

                    foreach (var (num, (y, x)) in Numbers)
                    {
                        if (!Marked[y, x])
                        {
                            sum += num;
                        }
                    }

                    return sum;
                }
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var bs = new BingoSubsystem(input);

            return bs.FindWinningNumber().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return "Puzzle2";
        }
    }
}
