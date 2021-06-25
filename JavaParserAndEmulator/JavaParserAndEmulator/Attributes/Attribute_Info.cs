using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM
{
    public abstract class Attribute_Info : IParseByRef
    {

        public ushort Attribute_Name_Index { get; }
        public abstract uint Attribute_Length { get; protected set; }
        //public abstract byte[] Info { get; protected set; }

        public Attribute_Info(ushort attribute_Name_Index)
        {
            Attribute_Name_Index = attribute_Name_Index;
        }

        public abstract void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool);
        
        
        /*
        public override void Parse(ref Memory<byte> hexDump)
        {
            Attribute_Name_Index = hexDump.Read2();
            Attribute_Length = hexDump.Read4();

            Info = new byte[Attribute_Length];
            for(int i = 0; i < Info.Length; i ++)
            {
                Info[i] = hexDump.Read1();
            }
        }
        */
    }
}
