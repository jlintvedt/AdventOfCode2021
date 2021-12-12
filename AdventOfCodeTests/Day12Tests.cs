using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day12Tests
    {
        private string input_puzzle;
        private string input_example1;
        private string input_example2;
        private string input_example3;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "12";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format("start-A{0}start-b{0}A-c{0}A-b{0}b-d{0}A-end{0}b-end", Environment.NewLine);
            input_example2 = string.Format("dc-end{0}HN-start{0}start-kj{0}dc-start{0}dc-HN{0}LN-dc{0}HN-end{0}kj-sa{0}kj-HN{0}kj-dc", Environment.NewLine);
            input_example3 = string.Format("fs-end{0}he-DX{0}fs-he{0}start-DX{0}pj-DX{0}end-zg{0}zg-sl{0}zg-pj{0}pj-he{0}RW-he{0}fs-DX{0}pj-RW{0}zg-RW{0}start-pj{0}he-WI{0}zg-he{0}pj-fs{0}start-RW", Environment.NewLine);
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
            var result = AdventOfCode.Day12.Puzzle1(input_example1);
            Assert.AreEqual($"10", result);

            result = AdventOfCode.Day12.Puzzle1(input_example2);
            Assert.AreEqual($"19", result);

            result = AdventOfCode.Day12.Puzzle1(input_example3);
            Assert.AreEqual($"226", result);
        }

        [TestMethod]
        public void Puzzle1()
        {
            // Act
            var result = AdventOfCode.Day12.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual($"4885", result);
        }

        [TestMethod]
        public void Example_Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day12.Puzzle2(input_example2);

            // Assert
            Assert.AreEqual($"Puzzle2", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day12.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"Puzzle2", result);
        }
    }
}
