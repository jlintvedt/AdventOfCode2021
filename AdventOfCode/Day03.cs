using System;
using System.Collections.Generic;
using System.Linq;

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
                var mostCommon = CalculateMostCommonValues();

                UInt16 gamma = 0;
                UInt16 epsilon = 0;
                for (int i = 0; i < NumBits; i++)
                {
                    if (mostCommon[NumBits - 1 - i] == 1)
                        gamma |= (UInt16)(1 << i);
                    else
                        epsilon |= (UInt16)(1 << i);
                }

                return gamma * epsilon;
            }

            public int CalculateLifeSupportRating()
            {
                var mostCommon = CalculateMostCommonValues();

                var canOxygen = new HashSet<int>(NumLines);
                for (int i = 0; i < NumLines; i++)
                {
                    canOxygen.Add(i);
                }
                var canCo2 = new HashSet<int>(canOxygen);

                // Calculate ratings
                var oxygenRating = 0;
                var co2Rating = 0;

                for (int j = 0; j < NumBits; j++)
                {
                    var oxygenCriteria = CalculateMostCommonValueForCandidatesAtPos(canOxygen, j);
                    var co2Criteria = 1 - CalculateMostCommonValueForCandidatesAtPos(canCo2, j);

                    for (int i = 0; i < NumLines; i++)
                    {
                        if (DiagnosticReport[i,j] != oxygenCriteria)
                            canOxygen.Remove(i);

                        if (DiagnosticReport[i,j] != co2Criteria)
                            canCo2.Remove(i);
                    }

                    if (canOxygen.Count == 1)
                        oxygenRating = Common.Common.IntArrayOfUnsignedBinaryToInt(GetRow(canOxygen.First()));

                    if (canCo2.Count == 1)
                        co2Rating= Common.Common.IntArrayOfUnsignedBinaryToInt(GetRow(canCo2.First()));

                    if (oxygenRating > 0 && co2Rating > 0)
                    {
                        break;
                    }
                }

                return oxygenRating * co2Rating;
            }

            private int[] CalculateMostCommonValues()
            {
                var numSet = new int[NumBits];

                // Count bits set
                for (int i = 0; i < NumLines; i++)
                {
                    for (int j = 0; j < NumBits; j++)
                    {
                        if (DiagnosticReport[i, j] == 1)
                            numSet[j]++;
                    }
                }

                var mostCommon = new int[NumBits];
                for (int i = 0; i < NumBits; i++)
                {
                    if (numSet[i] >= NumLines / 2)
                        mostCommon[i] = 1;
                }

                return mostCommon;
            }

            private int CalculateMostCommonValueForCandidatesAtPos(HashSet<int> candidates, int pos)
            {
                var numSet = 0;

                foreach (var can in candidates)
                {
                    if (DiagnosticReport[can,pos] == 1)
                        numSet++;
                }

                return numSet >= (candidates.Count - numSet) ? 1 : 0;
            }

            private int[] GetRow(int rowNum)
            {
                var row = new int[NumBits];
                for (int i = 0; i < NumBits; i++)
                {
                    row[i] = DiagnosticReport[rowNum, i];
                }

                return row;
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

            return bd.CalculateLifeSupportRating().ToString();
        }
    }
}
