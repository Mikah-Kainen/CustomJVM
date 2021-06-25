using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.ConstantPoolItems
{
    class CP_MethodRef_Info : CP_Info
    {
        public override byte Tag { get => 0x0A; }
        public ushort Class_Index { get; private set; }
        public ushort Name_And_Type_Index { get; private set; }

        public override void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            Class_Index = hexDump.Read2();
            Name_And_Type_Index = hexDump.Read2();
        }
    }
}
