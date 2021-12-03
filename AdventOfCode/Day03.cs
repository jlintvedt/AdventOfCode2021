using System;
using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/03
    /// </summary>
    public class Day03
    {
        public class BinaryDiagnostics
        {
            private int[,] DiagnosticReport;
            private int NumLines;
            private int NumBits;

            public BinaryDiagnostics(string input)
            {
                var lines = input.Split(Environment.NewLine);
                NumLines = lines.Length;
                NumBits = lines[0].Length;
                DiagnosticReport = new int[NumLines,NumBits];

                for (int i = 0; i < NumLines; i++)
                {
                    for (int j = 0; j < NumBits; j++)
                    {
                        if (lines[i][j] == '1')
                        {
                            DiagnosticReport[i, j] = 1;
                        }
                    }
                }
            }

            public int CalculatePowerConsumption()
            {
                var numSet = new int[NumBits];

                // Count bits set
                for (int i = 0; i < NumLines; i++)
                {
                    for (int j = 0; j < NumBits; j++)
                    {
                        if (DiagnosticReport[i,j] == 1)
                            numSet[j]++;
                    }
                }

                UInt16 gamma = 0;
                UInt16 epsilon = 0;
                for (int i = 0; i < NumBits; i++)
                {
                    if (numSet[NumBits - 1 - i] > NumLines / 2)
                        gamma |= (UInt16)(1 << i);
                    else
                        epsilon |= (UInt16)(1 << i);
                }

                return gamma * epsilon;
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var bd = new BinaryDiagnostics(input);

            return bd.CalculatePowerConsumption().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var bd = new BinaryDiagnostics(input);

            return "Puzzle2";
        }
    }
}
