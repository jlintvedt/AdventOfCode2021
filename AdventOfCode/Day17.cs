using System;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/17
    /// </summary>
    public class Day17
    {
        public class TrickShot
        {
            private (int min, int max) limitX, limitY;
            private (int x, int y) position, velocity;

            public TrickShot(string input)
            {
                var parts = input.Split(' ');

                var comp = parts[2].Split("..");
                limitX = (int.Parse(comp[0][2..^0]), int.Parse(comp[1][0..^1]));

                comp = parts[3].Split("..");
                limitY = (int.Parse(comp[0][2..^0]), int.Parse(comp[1]));
            }

            public int FindHighestShot()
            {
                int minX = 0;
                for (int tmp = 0; tmp < limitX.min ; )
                    tmp += ++minX;

                // Open fire
                var maxVelY = 0;
                for (int x = minX; x < minX + 2; x++)
                {
                    var velY = 0;
                    var consecutiveMiss = 0;
                    for (int y = 0; ; y++)
                    {
                        if (CheckForHit((x, y)))
                        {
                            velY = y;
                            consecutiveMiss = 0;
                        }
                        else if ((++consecutiveMiss > 10) || ((velY > 0) && (consecutiveMiss > 10)))
                            break;
                    }
                    maxVelY = velY > maxVelY ? velY : maxVelY;
                }

                var maxY = 0;
                while (maxVelY > 0)
                    maxY += maxVelY--;

                return maxY;
            }

            private bool CheckForHit((int x, int y) velocity)
            {
                var pos = (x: 0, y: 0);

                if (velocity.x == 6 && velocity.y == 9)
                    pos.x = 0;

                while (pos.x < limitX.max && pos.y > limitY.min)
                {
                    pos.x += velocity.x;
                    pos.y += velocity.y;
                    velocity.x = velocity.x > 0 ? velocity.x-1 : 0;
                    velocity.y--;

                    if (pos.x >= limitX.min && pos.y <= limitY.max && pos.x <= limitX.max && pos.y >= limitY.min)
                        return true;
                }

                return false;
            }
        }
        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var ts = new TrickShot(input);

            return ts.FindHighestShot().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return "Puzzle2";
        }
    }
}
