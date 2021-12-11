using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day11Tests
    {
        private string input_puzzle;
        private string input_example1;
        private string input_example2;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "11";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format("5483143223{0}2745854711{0}5264556173{0}6141336146{0}6357385478{0}4167524645{0}2176841721{0}6882881134{0}4846848554{0}5283751526", Environment.NewLine);
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
            var result = AdventOfCode.Day11.Puzzle1(input_example1);

            // Assert
            Assert.AreEqual($"1656", result);
        }

        [TestMethod]
        public void Puzzle1()
        {
            // Act
            var result = AdventOfCode.Day11.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual($"1757", result);
        }

        [TestMethod]
        public void Example_Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day11.Puzzle2(input_example1);

            // Assert
            Assert.AreEqual($"195", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day11.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"422", result);
        }
    }
}
