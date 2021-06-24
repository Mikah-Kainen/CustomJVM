using CustomJVM.ConstantPoolItems;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using static CustomJVM.Enum;

namespace CustomJVM
{
    public static class Extensions
    {

        public static string Example(this string example)
        {
            return "example";
        }

        public static byte Read1(this ref Memory<byte> hexDump)
        {
            byte returnValue = hexDump.Span[0];
            hexDump = hexDump.Slice(1);
            return returnValue;
        }

        public static ushort Read2(this ref Memory<byte> hexDump)
        {
            Span<ushort> newMemory = MemoryMarshal.Cast<byte, ushort>(hexDump.Span);
            ushort returnValue = newMemory[0].ReverseBytes();
            hexDump = hexDump.Slice(2);
            return returnValue;
        }

        public static uint Read4(this ref Memory<byte> hexDump)
        {
            Span<uint> newMemory = MemoryMarshal.Cast<byte, uint>(hexDump.Span);
            uint returnValue = newMemory[0].ReverseBytes();
            hexDump = hexDump.Slice(4);
            return returnValue;
        }

        public static ushort ReverseBytes(this ushort input)
        {
            byte firstByte = (byte)(input >> 8);
            byte secondByte = (byte)input;

            return (ushort)((secondByte << 8) | firstByte);
        }

        public static uint ReverseBytes(this uint input)
        {
            byte firstByte = (byte)(input >> 24);
            byte secondByte = (byte)(input >> 16);
            byte thirdByte = (byte)(input >> 8);
            byte fourthByte = (byte)input;

            return (uint)((fourthByte << 24) | (thirdByte << 16) | (secondByte << 8) | firstByte);
        }

        public static string String(this CP_Utf8_Info info)
        {
            string returnString = new string(info.Bytes.Select((byte x) => (char)x).ToArray());

            return returnString;
        }
    }
}
