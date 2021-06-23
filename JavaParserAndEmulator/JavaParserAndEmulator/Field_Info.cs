using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM
{
    public class Field_Info
    {

        public ushort Access_Flags { get; private set; }
        public ushort Name_Index { get; private set; }
        public ushort Descriptor_Index { get; private set; }
        public ushort Attributes_Count { get; private set; }
        public Attribute_Info[] Attributes { get; private set;}

        public Field_Info()
        {

        }

        public void Parse(ref Memory<byte> hexDump)
        {
            Access_Flags = hexDump.Read2();
            Name_Index = hexDump.Read2();
            Descriptor_Index = hexDump.Read2();
            Attributes_Count = hexDump.Read2();

            Attributes = new Attribute_Info[Attributes_Count];
            for (int i = 0; i < Attributes.Length; i ++)
            {
                Attribute_Info current = new Attribute_Info();
                current.Parse(ref hexDump);
                Attributes[i] = current;
            }
        }
    }
}
