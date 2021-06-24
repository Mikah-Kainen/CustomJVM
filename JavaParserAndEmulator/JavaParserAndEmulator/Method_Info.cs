using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM
{
    public class Method_Info
    {
        public ushort Access_Flags;
        public ushort Name_Index;
        public ushort Descriptor_Index;
        public ushort Attributes_Count;

        public Attribute_Info[] Attributes;

        public Method_Info()
        {

        }

        public void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            Access_Flags = hexDump.Read2();
            Name_Index = hexDump.Read2();
            Descriptor_Index = hexDump.Read2();
            Attributes_Count = hexDump.Read2();

            Attributes = new Attribute_Info[Attributes_Count];
            for (int i = 0; i < Attributes.Length; i++)
            {
                Attribute_Info current = Program.CreateAttribute_Info(ref hexDump, constantPool);
                current.Parse(ref hexDump, constantPool);
                Attributes[i] = current;
            }
        }
    }
}
