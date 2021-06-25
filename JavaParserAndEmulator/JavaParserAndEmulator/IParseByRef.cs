using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM
{
    interface IParseByRef
    {
        public void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool);
    }
}
