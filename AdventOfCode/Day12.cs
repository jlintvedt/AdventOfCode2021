using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/12
    /// </summary>
    public class Day12
    {
        public class PassagePathing
        {
            public readonly Dictionary<string, Cave> Caves;
            private readonly Stack<string> visited = new Stack<string>();
            private bool allowDoubleVisitToOneSmallCave;
            private int numPaths = 0;

            public PassagePathing(string input)
            {
                Caves = new Dictionary<string, Cave>();

                foreach (var line in input.Split(Environment.NewLine))
                {
                    var parts = line.Split('-');
                    var from = GetOrCreateCave(parts[0]);
                    var to = GetOrCreateCave(parts[1]);

                    from.AddConnection(to);
                    to.AddConnection(from);
                }
            }

            public int FindNumPaths(bool allowDoubleVisit = false)
            {
                allowDoubleVisitToOneSmallCave = allowDoubleVisit;
                FindPathsRec(Caves["start"], false);
                return numPaths;
            }

            private void FindPathsRec(Cave current, bool doubleVisitUsed)
            {
                visited.Push(current.Name);

                if (current.Name == "end")
                {
                    numPaths++;
                }
                else
                {
                    foreach (var cave in current.Connections)
                    {
                        if (!cave.LargeCave && visited.Contains(cave.Name))
                        {
                            if (!allowDoubleVisitToOneSmallCave || doubleVisitUsed)
                                continue;

                            // Allow visiting small cave twice, once
                            FindPathsRec(cave, true);
                        }
                        else
                        {
                            FindPathsRec(cave, doubleVisitUsed);
                        }
                    }
                }

                visited.Pop();
            }

            private Cave GetOrCreateCave(string name)
            {
                if (Caves.TryGetValue(name, out Cave cave))
                    return cave;

                cave = new Cave(name);
                Caves.Add(name, cave);
                return cave;
            }

            public class Cave
            {
                public bool LargeCave;
                public string Name;
                public List<Cave> Connections;

                public Cave(string name)
                {
                    Name = name;
                    Connections = new List<Cave>();

                    if (char.IsUpper(name[0]))
                        LargeCave = true;
                }

                public void AddConnection(Cave con)
                {
                    // We can skip 'start' as it's never re-visited
                    if (con.Name != "start")
                        Connections.Add(con);
                }

                public override string ToString()
                {
                    return Name;
                }
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var pp = new PassagePathing(input);

            return pp.FindNumPaths().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var pp = new PassagePathing(input);

            return pp.FindNumPaths(allowDoubleVisit: true).ToString();
        }
    }
}
