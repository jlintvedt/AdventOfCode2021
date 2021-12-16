using System;
using System.Collections;
using System.Collections.Generic;

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

            public class Packet
            {
                public int[] data;
                public int Version;
                public PacketType Type;
                public List<Packet> Packets;
                public int Value = 0;
                private int readPos = 0;
                public int Length;

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
                            Value += data[index] << (numGroups - g - 1) * 4 + 3 - i;
                        }
                    }

                    readPos += numGroups * 5;
                }

                private void ParseSubPackets()
                {
                    Packets = new List<Packet>();

                    // Total length of packet
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

                public int ReadAsInt(int length)
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
            }

            public enum PacketType
            {
                a,
                b,
                c,
                d,
                literalValue,
                f,
                g
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
            return "Puzzle2";
        }
    }
}
