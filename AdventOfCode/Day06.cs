namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/06
    /// </summary>
    public class Day06
    {
        public class LanterfishSchool
        {
            private long[] DaysToSpawn;

            public LanterfishSchool(string input)
            {
                DaysToSpawn = new long[9];

                foreach (string days in input.Split(','))
                {
                    DaysToSpawn[int.Parse(days)]++;
                }
            }

            public long CalculatePopulation(int days = 80)
            {
                var nextDay = new long[9];
                
                for (int day = 0; day <= days; day++)
                {
                    // Advance existing fish
                    for (int i = 0; i < 8; i++)
                    {
                        nextDay[i] = DaysToSpawn[i + 1];
                    }

                    // Ready fish spawn
                    nextDay[8] = DaysToSpawn[0];
                    nextDay[6] += DaysToSpawn[0];

                    // Swap arrays
                    var tmp = DaysToSpawn;
                    DaysToSpawn = nextDay;
                    nextDay = tmp;
                }

                // Calculate total
                long total = 0;
                for (int i = 0; i < 8; i++)
                {
                    total += DaysToSpawn[i];
                }

                return total;
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

            return ls.CalculatePopulation(256).ToString();
        }
    }
}
