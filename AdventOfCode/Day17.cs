using System;
using System.Collections.Generic;

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
            private List<int> initialX, initialY;

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

            public int FindNumInitialVelocitiy()
            {
                // Idea: Can be further optimized by making dicts with numSteps for valid start positions and cross-referencing
                FindValidInitialVelocities();
                
                // Open fire
                var validStartVelocity = new HashSet<(int x, int y)>();
                foreach (var x in initialX)
                {
                    var consecutiveMiss = 0;
                    foreach (var y in initialY)
                    {
                        if (CheckForHit((x, y)))
                            validStartVelocity.Add((x, y));
                        else if ((++consecutiveMiss > 120))
                            break;
                    }
                }

                return validStartVelocity.Count;
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

            private void FindValidInitialVelocities()
            {
                initialX = new List<int>();
                int minX = 0;
                for (int tmp = 0; tmp < limitX.min;)
                    tmp += ++minX;

                for (int initX = minX; initX <= limitX.max; initX++)
                {
                    var velX = initX;
                    for (int x = 0; x <= limitX.max && velX > 0; velX--)
                    {
                        x += velX;
                        if (x >= limitX.min && x <= limitX.max)
                        {
                            initialX.Add(initX);
                            break;
                        }
                    }
                }

                initialY = new List<int>();
                for (int initY = limitY.min; initY < -limitY.min; initY++)
                {
                    var velY = initY;
                    for (int y = 0; y >= limitY.min; velY--)
                    {
                        y += velY;
                        if (y >= limitY.min && y <= limitY.max)
                        {
                            initialY.Add(initY);
                            break;
                        }
                    }
                }
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
            var ts = new TrickShot(input);

            return ts.FindNumInitialVelocitiy().ToString();
        }
    }
}
