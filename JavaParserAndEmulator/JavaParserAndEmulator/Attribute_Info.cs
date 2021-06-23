using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM
{
    public class Attribute_Info
    {

        public ushort Attribute_Name_Index;
        public uint Attribute_Length;
        public byte[] Info;

        public Attribute_Info()
        {

        }

        public void Parse(ref Memory<byte> hexDump)
        {
            Attribute_Name_Index = hexDump.Read2();
            Attribute_Length = hexDump.Read4();

            Info = new byte[Attribute_Length];
            for(int i = 0; i < Info.Length; i ++)
            {
                Info[i] = hexDump.Read1();
            }
        }
    }
}
