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
            public readonly Cave Start;
            public readonly Cave End;
            private List<string> paths = new List<string>();

            public PassagePathing(string input)
            {
                Caves = new Dictionary<string, Cave>();

                foreach (var line in input.Split(Environment.NewLine))
                {
                    var parts = line.Split('-');
                    var from = GetOrCreateCave(parts[0]);
                    var to = GetOrCreateCave(parts[1]);

                    from.Connections.Add(to);
                    to.Connections.Add(from);
                }

                Start = Caves["start"];
                End= Caves["end"];
            }

            public int FindNumPaths()
            {
                var visited = new Stack<string>();
                FindPathsRec(Start, visited);
                return paths.Count;
            }

            private void FindPathsRec(Cave current, Stack<string> visited)
            {
                visited.Push(current.Name);

                if (current.Name == "end")
                {
                    paths.Add(string.Join(',', visited));
                }
                else
                {
                    foreach (var cave in current.Connections)
                    {
                        if (!cave.LargeCave && visited.Contains(cave.Name))
                            continue;

                        FindPathsRec(cave, visited);
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
            return "Puzzle2";
        }
    }
}
