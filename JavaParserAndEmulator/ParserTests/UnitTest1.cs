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
    }
}
