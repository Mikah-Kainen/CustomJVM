using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.ConstantPoolItems
{
    public abstract class CP_Info : IParseByRef
    {
        public abstract byte Tag { get; }
        public abstract void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool);
    }
}
