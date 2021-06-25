using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.Attributes
{
    public class SourceFile_Attribute : Attribute_Info
    {
        public override uint Attribute_Length { get; protected set; }
        public byte[] Debug_Extension { get; private set; }
        
        public SourceFile_Attribute(ushort attribute_Name_Index) 
            : base(attribute_Name_Index)
        {

        }


        public override void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            Attribute_Length = hexDump.Read4();
            Debug_Extension = new byte[Attribute_Length];
            for(int i = 0; i < Debug_Extension.Length; i ++)
            {
                Debug_Extension[i] = hexDump.Read1();
            }
        }
    }
}
