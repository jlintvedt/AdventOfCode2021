using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day09Tests
    {
        private string input_puzzle;
        private string input_example1;
        private string input_example2;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "09";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format("2199943210{0}3987894921{0}9856789892{0}8767896789{0}9899965678", Environment.NewLine);
            input_example2 = string.Format("example{0}2", Environment.NewLine);
        }

        [TestMethod]
        public void Begin_WarmUp()
        {
            // Force performing LoadInput() warm-up as part of this test
        }

        [TestMethod]
        public void Example_Puzzle1()
        {
            // Act
            var result = AdventOfCode.Day09.Puzzle1(input_example1);

            // Assert
            Assert.AreEqual($"15", result);
        }

        [TestMethod]
        public void Puzzle1()
        {
            // Act
            var result = AdventOfCode.Day09.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual($"500", result);
        }

        [TestMethod]
        public void Example_Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day09.Puzzle2(input_example2);

            // Assert
            Assert.AreEqual($"Puzzle2", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day09.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"Puzzle2", result);
        }
    }
}
