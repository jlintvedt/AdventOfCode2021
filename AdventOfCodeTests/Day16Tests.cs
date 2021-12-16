using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day16Tests
    {
        private string input_puzzle;
        private string input_example1;
        private string input_example2;
        private string input_example3;
        private string input_example4;
        private string input_example5;
        private string input_example6;
        private string input_example7;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "16";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format("D2FE28", Environment.NewLine);
            input_example2 = string.Format("38006F45291200", Environment.NewLine);
            input_example3 = string.Format("EE00D40C823060", Environment.NewLine);
            input_example4 = string.Format("8A004A801A8002F478", Environment.NewLine);
            input_example5 = string.Format("620080001611562C8802118E34", Environment.NewLine);
            input_example6 = string.Format("C0015000016115A2E0802F182340", Environment.NewLine);
            input_example7 = string.Format("A0016C880162017C3686B18A3D4780", Environment.NewLine);
        }

        [TestMethod]
        public void Begin_WarmUp()
        {
            // Force performing LoadInput() warm-up as part of this test
        }

        [TestMethod]
        public void Example_Puzzle1()
        {
            // Act & Assert
            var result = AdventOfCode.Day16.Puzzle1(input_example1);
            Assert.AreEqual($"6", result);

            result = AdventOfCode.Day16.Puzzle1(input_example2);
            Assert.AreEqual($"9", result);

            result = AdventOfCode.Day16.Puzzle1(input_example3);
            Assert.AreEqual($"14", result);

            result = AdventOfCode.Day16.Puzzle1(input_example4);
            Assert.AreEqual($"16", result);

            result = AdventOfCode.Day16.Puzzle1(input_example5);
            Assert.AreEqual($"12", result);

            result = AdventOfCode.Day16.Puzzle1(input_example6);
            Assert.AreEqual($"23", result);

            result = AdventOfCode.Day16.Puzzle1(input_example7);
            Assert.AreEqual($"31", result);
        }

        [TestMethod]
        public void Puzzle1()
        {
            // Act
            var result = AdventOfCode.Day16.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual($"866", result);
        }

        [TestMethod]
        public void Example_Puzzle2()
        {
            // Act & Assert
            var result = AdventOfCode.Day16.Puzzle2("C200B40A82");
            Assert.AreEqual($"3", result);

            result = AdventOfCode.Day16.Puzzle2("04005AC33890");
            Assert.AreEqual($"54", result);

            result = AdventOfCode.Day16.Puzzle2("880086C3E88112");
            Assert.AreEqual($"7", result);

            result = AdventOfCode.Day16.Puzzle2("CE00C43D881120");
            Assert.AreEqual($"9", result);

            result = AdventOfCode.Day16.Puzzle2("D8005AC2A8F0");
            Assert.AreEqual($"1", result);

            result = AdventOfCode.Day16.Puzzle2("F600BC2D8F");
            Assert.AreEqual($"0", result);

            result = AdventOfCode.Day16.Puzzle2("9C005AC2F8F0");
            Assert.AreEqual($"0", result);

            result = AdventOfCode.Day16.Puzzle2("9C0141080250320F1802104A08");
            Assert.AreEqual($"1", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day16.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"1392637195518", result);
        }
    }
}
