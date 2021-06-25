using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.ConstantPoolItems
{
    public class CP_Class_info : CP_Info
    {
        public override byte Tag { get => 0x7; }
        public ushort Name_Index { get; private set; }

        public override void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            Name_Index = hexDump.Read2();
        }
    }
}
