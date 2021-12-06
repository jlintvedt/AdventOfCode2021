using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/06
    /// </summary>
    public class Day06
    {
        public class LanterfishSchool
        {
            private List<Lanternfhish> lanternfhish;

            public LanterfishSchool(string input)
            {
                lanternfhish = new List<Lanternfhish>();

                foreach (string days in input.Split(','))
                {
                    lanternfhish.Add(new Lanternfhish(int.Parse(days)));
                }
            }

            public int CalculatePopulation(int days = 80)
            {
                for (int day = 0; day < days; day++)
                {
                    var numNew = 0;

                    foreach (var fish in lanternfhish)
                    {
                        fish.AdvanceDay(out bool newSpawn);

                        if (newSpawn)
                            numNew++;
                    }

                    for (int i = 0; i < numNew; i++)
                    {
                        lanternfhish.Add(new Lanternfhish());
                    }
                }

                return lanternfhish.Count;
            }

            public class Lanternfhish
            {
                public int DaysToSpawning;

                public Lanternfhish(int days = 8)
                {
                    this.DaysToSpawning = days;
                }

                public void AdvanceDay(out bool newSpawn)
                {
                    if (DaysToSpawning-- <= 0)
                    {
                        DaysToSpawning = 6;
                        newSpawn = true;
                    }
                    else
                    {
                        newSpawn = false;
                    }
                }
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var ls = new LanterfishSchool(input);

            return ls.CalculatePopulation().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var ls = new LanterfishSchool(input);

            return "Puzzle2";
        }
    }
}
