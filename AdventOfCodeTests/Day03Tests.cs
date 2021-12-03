using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day03Tests
    {
        private string input_puzzle;
        private string input_example1;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "03";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format("00100{0}11110{0}10110{0}10111{0}10101{0}01111{0}00111{0}11100{0}10000{0}11001{0}00010{0}01010", Environment.NewLine);
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
            var result = AdventOfCode.Day03.Puzzle1(input_example1);

            // Assert
            Assert.AreEqual($"198", result);
        }

        [TestMethod]
        public void Puzzle1()
        {
            // Act
            var result = AdventOfCode.Day03.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual($"1071734", result);
        }

        [TestMethod]
        public void Example_Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day03.Puzzle2(input_example1);

            // Assert
            Assert.AreEqual($"230", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day03.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"6124992", result);
        }
    }
}
