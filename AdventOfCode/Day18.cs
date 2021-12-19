using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/18
    /// </summary>
    public class Day18
    {
        public class SnailfishMath
        {
            private readonly Queue<Number> numbers = new();
            private Number sum;

            public SnailfishMath(string input)
            {
                foreach (var line in input.Split(Environment.NewLine))
                    numbers.Enqueue(new Number(line));
            }

            public Number Sum { 
                get {
                    if (sum == null)
                        CalculateSum();
                    return sum; 
            } }

            public int FindMagnitudeOfFinalSum()
            {
                CalculateSum();
                return sum.CalculateMagnitude();
            }

            public int FindLargestMagnitudeWithTwoNumbers()
            {
                var numArray = numbers.ToArray();
                var largest = 0;

                for (var i = 0; i < numArray.Length; i++)
                {
                    for (int j = 0; j < numArray.Length; j++)
                    {
                        if (i == j)
                            continue;

                        var num = new Number(numArray[i].ToString());
                        num.Reduce();
                        num.AddNumber(new Number(numArray[j].ToString()));
                        num.Reduce();
                        
                        largest = largest > num.CalculateMagnitude() ? largest : num.CalculateMagnitude();
                    }
                }

                return largest;
            }

            private void CalculateSum()
            {
                sum = numbers.Dequeue();
                sum.Reduce();
                while (numbers.Count > 0)
                {
                    sum.AddNumber(numbers.Dequeue());
                    sum.Reduce();
                }
            }

            public class Number
            {
                public readonly List<Element> Elements;

                public Number(string input)
                {
                    Elements = new List<Element>();
                    foreach (var e in input)
                        Elements.Add(new Element(e));
                }

                public void AddNumber(Number other)
                {
                    Elements.Insert(0, new Element('['));
                    Elements.Insert(Elements.Count, new Element(','));
                    Elements.InsertRange(Elements.Count, other.Elements);
                    Elements.Insert(Elements.Count, new Element(']'));
                }

                public void Reduce()
                {
                    while (true)
                    {
                        // Search for explode or split candidate
                        int depth = 0;
                        int explodeIndex = 0, splitIndex = 0;
                        for (int i = 0; i < Elements.Count; i++)
                        {
                            switch (Elements[i].Type)
                            {
                                case Type.value:
                                    // Set to index of first split-ready number
                                    if (splitIndex == 0 && Elements[i].Value >= 10)
                                        splitIndex = i;
                                    break;
                                case Type.open:
                                    if (++depth > 4)
                                        explodeIndex = i;
                                    break;
                                case Type.close:
                                    depth--;
                                    break;
                                default:
                                    break;
                            }

                            if (explodeIndex > 0)
                                break;
                        }

                        // Perform explosion
                        if (explodeIndex > 0)
                        {
                            PerformExplosion(explodeIndex);
                            continue;
                        }

                        // Perform split
                        if (splitIndex > 0)
                        {
                            PerformSplit(splitIndex);
                            continue;
                        }

                        // No more operations
                        break;
                    }
                }

                public void PerformExplosion(int index)
                {
                    if (Elements[index].Type != Type.open || Elements[index + 1].Type != Type.value || Elements[index + 2].Type != Type.separator || Elements[index + 3].Type != Type.value || Elements[index + 4].Type != Type.close)
                        throw new Exception("Invalid index for explosion");

                    // Move left value
                    for (int i = index; i > 0; i--)
                    {
                        if (Elements[i].Type == Type.value)
                        {
                            Elements[i].Value += Elements[index + 1].Value;
                            break;
                        }
                    }

                    // Move right value
                    for (int i = index+5; i < Elements.Count; i++)
                    {
                        if (Elements[i].Type == Type.value)
                        {
                            Elements[i].Value += Elements[index + 3].Value;
                            break;
                        }
                    }

                    // Replace pair with 0
                    Elements.RemoveRange(index, 5);
                    Elements.Insert(index, new Element(0));
                }

                public void PerformSplit(int index)
                {
                    if (Elements[index].Type != Type.value && Elements[index].Value < 10)
                        throw new Exception("Invalid index for split");

                    var half = Elements[index].Value / 2;
                    var remainder = Elements[index].Value % 2;

                    Elements.Insert(index + 1, new Element(']'));
                    Elements.Insert(index + 1, new Element(half+remainder));
                    Elements.Insert(index + 1, new Element(','));
                    Elements[index].Value = half;
                    Elements.Insert(index, new Element('['));
                }

                public int CalculateMagnitude()
                {
                    var index = 0;
                    while (Elements.Count > 1)
                    {
                        // Search for closing bracket
                        while (Elements[++index].Type != Type.close) { }
                        // Move index to start of inner pair
                        index -= 4;
                        // Collapse
                        PerformMagnitudeCollapse(index);
                    }

                    return Elements[0].Value;
                }

                public void PerformMagnitudeCollapse(int index)
                {
                    if (Elements[index].Type != Type.open || Elements[index + 1].Type != Type.value || Elements[index + 2].Type != Type.separator || Elements[index + 3].Type != Type.value || Elements[index + 4].Type != Type.close)
                        throw new Exception("Invalid index for magnitude collapse");

                    var elem = new Element(Elements[index + 1].Value * 3 + Elements[index + 3].Value * 2);
                    Elements.RemoveRange(index, 5);
                    Elements.Insert(index, elem);
                }

                public override string ToString()
                {
                    return String.Join("", Elements);
                }

                public class Element
                {
                    public Type Type;
                    public int Value;

                    public Element(char input)
                    {
                        switch (input)
                        {
                            case '[':
                                Type = Type.open;
                                break;
                            case ']':
                                Type = Type.close;
                                break;
                            case ',':
                                Type = Type.separator;
                                break;
                            default:
                                Type = Type.value;
                                Value = (int)char.GetNumericValue(input);
                                break;
                        }
                    }

                    public Element(int value)
                    {
                        Type = Type.value;
                        Value = value;
                    }

                    public override string ToString()
                    {
                        return Type switch
                        {
                            Type.value => Value.ToString(),
                            Type.open => "[",
                            Type.close => "]",
                            Type.separator => ",",
                            _ => throw new Exception("Unknown type"),
                        };
                    }
                }
            }

            public enum Type
            {
                value,
                open,
                close,
                separator
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var sm = new SnailfishMath(input);

            return sm.FindMagnitudeOfFinalSum().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var sm = new SnailfishMath(input);

            return sm.FindLargestMagnitudeWithTwoNumbers().ToString();
        }
    }
}
