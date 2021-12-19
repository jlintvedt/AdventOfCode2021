using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day18Tests
    {
        private string input_puzzle;
        private string input_example1;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "18";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format("[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]{0}[[[5,[2,8]],4],[5,[[9,9],0]]]{0}[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]{0}[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]{0}[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]{0}[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]{0}[[[[5,4],[7,7]],8],[[8,3],8]]{0}[[9,3],[[9,9],[6,[4,9]]]]{0}[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]{0}[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]", Environment.NewLine);
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
            var result = AdventOfCode.Day18.Puzzle1(input_example1);

            // Assert
            Assert.AreEqual($"4140", result);
        }

        [TestMethod]
        public void Puzzle1()
        {
            // Act
            var result = AdventOfCode.Day18.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual($"4057", result);
        }

        [TestMethod]
        public void Example_Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day18.Puzzle2(input_example1);

            // Assertt
            Assert.AreEqual($"3993", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day18.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"4683", result);
        }

        // Number
        [TestMethod]
        public void Number_IsParsedCorrectly()
        {
            // Arrange, Act & Assert
            var input = "[1,2]";
            Assert.AreEqual(input, (new AdventOfCode.Day18.SnailfishMath.Number(input)).ToString());

            input = "[[1,2],3]";
            Assert.AreEqual(input, (new AdventOfCode.Day18.SnailfishMath.Number(input)).ToString());

            input = "[9,[8,7]]";
            Assert.AreEqual(input, (new AdventOfCode.Day18.SnailfishMath.Number(input)).ToString());

            input = "[[1,9],[8,5]]";
            Assert.AreEqual(input, (new AdventOfCode.Day18.SnailfishMath.Number(input)).ToString());

            input = "[[[[1,2],[3,4]],[[5,6],[7,8]]],9]";
            Assert.AreEqual(input, (new AdventOfCode.Day18.SnailfishMath.Number(input)).ToString());

            input = "[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]";
            Assert.AreEqual(input, (new AdventOfCode.Day18.SnailfishMath.Number(input)).ToString());

            input = "[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]";
            Assert.AreEqual(input, (new AdventOfCode.Day18.SnailfishMath.Number(input)).ToString());
        }

        [TestMethod]
        public void Number_PerformExplosion()
        {
            // Arrange, Act & Assert
            var number = new AdventOfCode.Day18.SnailfishMath.Number("[[[[[9,8],1],2],3],4]");
            number.PerformExplosion(4);
            Assert.AreEqual("[[[[0,9],2],3],4]", number.ToString());

            number = new AdventOfCode.Day18.SnailfishMath.Number("[7,[6,[5,[4,[3,2]]]]]");
            number.PerformExplosion(12);
            Assert.AreEqual("[7,[6,[5,[7,0]]]]", number.ToString());

            number = new AdventOfCode.Day18.SnailfishMath.Number("[[6,[5,[4,[3,2]]]],1]");
            number.PerformExplosion(10);
            Assert.AreEqual("[[6,[5,[7,0]]],3]", number.ToString());

            number = new AdventOfCode.Day18.SnailfishMath.Number("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]");
            number.PerformExplosion(10);
            Assert.AreEqual("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", number.ToString());

            number = new AdventOfCode.Day18.SnailfishMath.Number("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
            number.PerformExplosion(24);
            Assert.AreEqual("[[3,[2,[8,0]]],[9,[5,[7,0]]]]", number.ToString());
        }

        [TestMethod]
        public void Number_ReduceStepByStep()
        {
            // Arrange
            var number = new AdventOfCode.Day18.SnailfishMath.Number("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]");

            // Act & Assert
            number.PerformExplosion(4);
            Assert.AreEqual("[[[[0,7],4],[7,[[8,4],9]]],[1,1]]", number.ToString());

            number.PerformExplosion(16);
            Assert.AreEqual("[[[[0,7],4],[15,[0,13]]],[1,1]]", number.ToString());

            number.PerformSplit(13);
            Assert.AreEqual("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", number.ToString());

            number.PerformSplit(22);
            Assert.AreEqual("[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]", number.ToString());

            number.PerformExplosion(22);
            Assert.AreEqual("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", number.ToString());
        }

        [TestMethod]
        public void Number_AddNumber()
        {
            // Arrange, Act & Assert
            var number = new AdventOfCode.Day18.SnailfishMath.Number("[1,1]");
            var other= new AdventOfCode.Day18.SnailfishMath.Number("[2,2]");
            number.AddNumber(other);
            Assert.AreEqual("[[1,1],[2,2]]", number.ToString());

            number = new AdventOfCode.Day18.SnailfishMath.Number("[[[[4,3],4],4],[7,[[8,4],9]]]");
            other = new AdventOfCode.Day18.SnailfishMath.Number("[1,1]");
            number.AddNumber(other);
            Assert.AreEqual("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", number.ToString());
        }

        [TestMethod]
        public void Pilotfish_Sum()
        {
            // Arrange, Act & Assert
            var input = string.Format("[1,1]{0}[2,2]{0}[3,3]{0}[4,4]", Environment.NewLine);
            var sm = new AdventOfCode.Day18.SnailfishMath(input);
            Assert.AreEqual("[[[[1,1],[2,2]],[3,3]],[4,4]]", sm.Sum.ToString());

            input = string.Format("[1,1]{0}[2,2]{0}[3,3]{0}[4,4]{0}[5,5]", Environment.NewLine);
            sm = new AdventOfCode.Day18.SnailfishMath(input);
            Assert.AreEqual("[[[[3,0],[5,3]],[4,4]],[5,5]]", sm.Sum.ToString());

            input = string.Format("[1,1]{0}[2,2]{0}[3,3]{0}[4,4]{0}[5,5]{0}[6,6]", Environment.NewLine);
            sm = new AdventOfCode.Day18.SnailfishMath(input);
            Assert.AreEqual("[[[[5,0],[7,4]],[5,5]],[6,6]]", sm.Sum.ToString());

            input = string.Format("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]{0}[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]{0}[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]{0}[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]{0}[7,[5,[[3,8],[1,4]]]]{0}[[2,[2,2]],[8,[8,1]]]{0}[2,9]{0}[1,[[[9,3],9],[[9,0],[0,7]]]]{0}[[[5,[7,4]],7],1]{0}[[[[4,2],2],6],[8,7]]", Environment.NewLine);
            sm = new AdventOfCode.Day18.SnailfishMath(input);
            Assert.AreEqual("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", sm.Sum.ToString());
        }

        [TestMethod]
        public void Pilotfish_FindMagnitudeOfFinalSum()
        {
            // Arrange, Act & Assert
            var sm = new AdventOfCode.Day18.SnailfishMath("[9,1]");
            Assert.AreEqual(29, sm.FindMagnitudeOfFinalSum());

            sm = new AdventOfCode.Day18.SnailfishMath("[1,9]");
            Assert.AreEqual(21, sm.FindMagnitudeOfFinalSum());

            sm = new AdventOfCode.Day18.SnailfishMath("[[9,1],[1,9]]");
            Assert.AreEqual(129, sm.FindMagnitudeOfFinalSum());

            sm = new AdventOfCode.Day18.SnailfishMath("[[1,2],[[3,4],5]]");
            Assert.AreEqual(143, sm.FindMagnitudeOfFinalSum());

            sm = new AdventOfCode.Day18.SnailfishMath("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]");
            Assert.AreEqual(1384, sm.FindMagnitudeOfFinalSum());

            sm = new AdventOfCode.Day18.SnailfishMath("[[[[1,1],[2,2]],[3,3]],[4,4]]");
            Assert.AreEqual(445, sm.FindMagnitudeOfFinalSum());

            sm = new AdventOfCode.Day18.SnailfishMath("[[[[3,0],[5,3]],[4,4]],[5,5]]");
            Assert.AreEqual(791, sm.FindMagnitudeOfFinalSum());

            sm = new AdventOfCode.Day18.SnailfishMath("[[[[5,0],[7,4]],[5,5]],[6,6]]");
            Assert.AreEqual(1137, sm.FindMagnitudeOfFinalSum());

            sm = new AdventOfCode.Day18.SnailfishMath("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]");
            Assert.AreEqual(3488, sm.FindMagnitudeOfFinalSum());
        }
    }
}
