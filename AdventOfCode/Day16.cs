using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// https://adventofcode.com/2021/day/16
    /// </summary>
    public class Day16
    {
        public class PacketDecoder
        {
            private readonly Packet rootPacket;

            public PacketDecoder(string input)
            {
                var bits = Common.Common.ConvertHexStringToBitArray(input);

                rootPacket  = new Packet(bits);
            }

            public int GetSumOfPacketVersions()
            {
                return rootPacket.GetVersionNumberWithSubPackets();
            }

            public long GetFinalValue()
            {
                return rootPacket.Value;
            }

            public class Packet
            {
                public int[] data; // This can be removed, change to ref
                public int Version;
                public PacketType Type;
                public List<Packet> Packets;
                private long value = 0;
                private int readPos = 0;
                public int Length;

                public long Value { get { return CalculateValue(); } }

                public Packet(int[] input)
                {
                    data = input;
                    Version = ReadAsInt(3);
                    Type = (PacketType)ReadAsInt(3);

                    if (Type == PacketType.literalValue)
                    {
                        ParseLiteralValue();
                    }
                    else
                    {
                        ParseSubPackets();
                    }

                    Length = readPos;
                }

                public int GetVersionNumberWithSubPackets()
                {
                    if (Type == PacketType.literalValue)
                        return Version;

                    var version = Version;
                    foreach (var packet in Packets)
                        version += packet.GetVersionNumberWithSubPackets();
                    return version;
                }

                private void ParseLiteralValue()
                {
                    var numGroups = 0;
                    while (data[readPos + numGroups++ * 5] == 1) ;

                    for (int g = 0; g < numGroups; g++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            var index = readPos + (g*5) + i + 1;
                            value += (long)data[index] << (numGroups - g - 1) * 4 + 3 - i;
                        }
                    }

                    readPos += numGroups * 5;
                }

                private void ParseSubPackets()
                {
                    Packets = new List<Packet>();

                    // Total length of packages
                    if(data[readPos++] == 0)
                    {
                        var len = ReadAsInt(15);
                        var i = 0;
                        while (i < len)
                        {
                            var pData = data[(readPos)..(readPos+len-i)];
                            var p = new Packet(pData);
                            Packets.Add(p);
                            i += p.Length;
                            readPos += p.Length;
                        }
                    }
                    // Number of packets
                    else
                    {
                        var numPackets = ReadAsInt(11);
                        for (int i = 0; i < numPackets; i++)
                        {
                            var pData = data[(readPos)..^0]; // Don't do this... Copies all remaining data
                            var p = new Packet(pData);
                            Packets.Add(p);
                            readPos += p.Length;
                        }
                    }
                }

                private long CalculateValue()
                {
                    switch (Type)
                    {
                        case PacketType.sum:
                            return Packets.Select(p => p.Value).Sum();
                        case PacketType.product:
                            return Packets.Select(p => p.Value).Aggregate((long)1, (acc, val) => acc * val);
                        case PacketType.minimum:
                            return Packets.Select(p => p.Value).Min();
                        case PacketType.maximum:
                            return Packets.Select(p => p.Value).Max();
                        case PacketType.literalValue:
                            return value;
                        case PacketType.greaterthan:
                            return Packets[0].Value > Packets[1].Value ? 1 : 0;
                        case PacketType.lessthan:
                            return Packets[0].Value < Packets[1].Value ? 1 : 0;
                        case PacketType.equal:
                            return Packets[0].Value == Packets[1].Value ? 1 : 0;
                        default:
                            throw new Exception("Unknown Type");
                    }
                }

                private int ReadAsInt(int length)
                {
                    var value = 0;
                    for (int i = 0; i < length; i++)
                    {
                        var index = readPos + i;
                        if (data[index] == 1)
                            value += 1 << (length - 1 - i);
                    }

                    readPos += length;

                    return value;
                }

                public override string ToString()
                {
                    if (Type == PacketType.literalValue)
                        return $"Value: {value}";
                    return $"{Type}[{Packets.Count}]: {Value}";
                }
            }

            public enum PacketType
            {
                sum,
                product,
                minimum,
                maximum,
                literalValue,
                greaterthan,
                lessthan,
                equal
            }
        }
        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var pd = new PacketDecoder(input);

            return pd.GetSumOfPacketVersions().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var pd = new PacketDecoder(input);

            return pd.GetFinalValue().ToString();
        }
    }
}
