using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day13Tests
    {
        private string input_puzzle;
        private string input_example1;
        private string input_example2;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "13";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format("6,10{0}0,14{0}9,10{0}0,3{0}10,4{0}4,11{0}6,0{0}6,12{0}4,1{0}0,13{0}10,12{0}3,4{0}3,0{0}8,4{0}1,10{0}2,14{0}8,10{0}9,0{0}{0}fold along y=7{0}fold along x=5", Environment.NewLine);
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
            var result = AdventOfCode.Day13.Puzzle1(input_example1);

            // Assert
            Assert.AreEqual($"17", result);
        }

        [TestMethod]
        public void Puzzle1()
        {
            // Act
            var result = AdventOfCode.Day13.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual($"737", result);
        }

        [TestMethod]
        public void Example_Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day13.Puzzle2(input_example1);

            // Assert
            Assert.AreEqual($"16", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day13.Puzzle2(input_puzzle);

            // Assert -> Manually read output for puzzle answer: ZUJUAFHP
            Assert.AreEqual($"96", result);
        }
    }
}
