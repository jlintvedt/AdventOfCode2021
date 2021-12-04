using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day04Tests
    {
        private string input_puzzle;
        private string input_example1;
        private string input_example2;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "04";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format("7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1{0}{0}22 13 17 11  0{0} 8  2 23  4 24{0}21  9 14 16  7{0} 6 10  3 18  5{0} 1 12 20 15 19{0}{0} 3 15  0  2 22{0} 9 18 13 17  5{0}19  8  7 25 23{0}20 11 10 24  4{0}14 21 16 12  6{0}{0}14 21 17 24  4{0}10 16 15  9 19{0}18  8 23 26 20{0}22 11 13  6  5{0} 2  0 12  3  7", Environment.NewLine);
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
            var result = AdventOfCode.Day04.Puzzle1(input_example1);

            // Assert
            Assert.AreEqual($"4512", result);
        }

        [TestMethod]
        public void Puzzle1()
        {
            // Act
            var result = AdventOfCode.Day04.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual($"2745", result);
        }

        [TestMethod]
        public void Example_Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day04.Puzzle2(input_example1);

            // Assert
            Assert.AreEqual($"1924", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day04.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"6594", result);
        }
    }
}
