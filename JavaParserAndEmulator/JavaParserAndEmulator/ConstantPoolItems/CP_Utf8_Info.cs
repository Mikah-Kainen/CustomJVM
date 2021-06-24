using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.ConstantPoolItems
{
    public class CP_Utf8_Info : CP_Info
    {
        public override byte Tag { get => 1; }
        public ushort Length { get; private set; }
        public byte[] Bytes { get; private set; }
        public override void Parse(ref Memory<byte> hexDump)
        {
            Length = hexDump.Read2();
            Bytes = new byte[Length];
            for(int i = 0; i < Length; i++)
            {
                Bytes[i] = hexDump.Read1();
            }    
        }
    }
}
