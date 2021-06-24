using CustomJVM;

using System;

using Xunit;

namespace ParserTests
{
    public class UnitTest1
    {

        [Theory]
        [InlineData(0x12345678, 0x78563412)]
        [InlineData(0x01234567, 0x67452301)]

        public void DoesReverseBytesUIntWork(uint input, uint expected)
        {
            Assert.Equal(expected, input.ReverseBytes());
        }

        [Theory]
        [InlineData(0x1324, 0x2413)]
        [InlineData(0x0123, 0x2301)]

        public void DoesReverseBytesUShortWork(ushort input, ushort expected)
        {
            Assert.Equal(expected, input.ReverseBytes());
        }

        //[Theory]
        //[]
        //[]

        //public void DoesSettupWork()
        //{
        //    CP_Info[] allConstantPoolItems = GetAllTypesThatInheritT<CP_Info>();


        //    Dictionary<byte, Func<CP_Info>> map = new Dictionary<byte, Func<CP_Info>>();

        //    foreach (CP_Info item in allConstantPoolItems)
        //    {
        //        map.Add(item.Tag, new Func<CP_Info>(() =>
        //        {
        //            return (CP_Info)Activator.CreateInstance(item.GetType());
        //        }));
        //    }


        //    string filePath = "Program.class";
        //    byte[] bytes = File.ReadAllBytes(filePath);
        //    Memory<byte> hexDump = bytes.AsMemory();

        //    uint magic = hexDump.Read4();
        //    ushort minor_version = hexDump.Read2();
        //    ushort major_version = hexDump.Read2();
        //}
    }
}
