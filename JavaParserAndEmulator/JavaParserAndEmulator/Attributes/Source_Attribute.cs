using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.Attributes
{
    public class Source_Attribute : Attribute_Info
    {
        public Source_Attribute(ushort attribute_Name_Index) 
            : base(attribute_Name_Index)
        {

        }

        public override uint Attribute_Length { get; protected set; }

        public override void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            throw new NotImplementedException();
        }
    }
}
