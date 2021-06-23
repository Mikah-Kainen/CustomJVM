using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.ConstantPoolItems
{
    public class CP_NameAndType : CP_Info
    {
        public override byte Tag { get => 12; }

        public ushort Name_Index { get; private set; }
        public ushort Descriptor_Index { get; private set; }

        public override void Parse(ref Memory<byte> hexDump)
        {
            Name_Index = hexDump.Read2();
            Descriptor_Index = hexDump.Read2();
        }
    }
}
