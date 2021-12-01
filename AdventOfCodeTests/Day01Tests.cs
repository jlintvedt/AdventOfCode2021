using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day01Tests
    {
        private string input_puzzle;
        private string input_example1;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "01";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format("199{0}200{0}208{0}210{0}200{0}207{0}240{0}269{0}260{0}263", Environment.NewLine);
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
            var result = AdventOfCode.Day01.Puzzle1(input_example1);

            // Assert
            Assert.AreEqual($"7", result);
        }

        [TestMethod]
        public void Puzzle1()
        {
            // Act
            var result = AdventOfCode.Day01.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual($"1121", result);
        }

        [TestMethod]
        public void Example_Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day01.Puzzle2(input_example1);

            // Assert
            Assert.AreEqual($"5", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day01.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"1065", result);
        }
    }
}
