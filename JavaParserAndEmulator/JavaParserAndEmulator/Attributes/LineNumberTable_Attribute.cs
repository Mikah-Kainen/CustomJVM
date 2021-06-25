using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.Attributes
{
    public class Line_Number_Table : IParseByRef
    {
        public ushort Start_PC { get; private set; }
        public ushort Line_Number { get; private set; }


        public Line_Number_Table()
        {

        }

        public void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            Start_PC = hexDump.Read2();
            Line_Number = hexDump.Read2();
        }
    }

    public class LineNumberTable_Attribute : Attribute_Info
    {
        public override uint Attribute_Length { get; protected set; }
        public ushort Line_Number_Table_Length { get; private set; }
        public Line_Number_Table[] Line_Number_Tables { get; private set; }



        public LineNumberTable_Attribute(ushort number_Index)
            : base(number_Index)
        { }
        public override void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            Attribute_Length = hexDump.Read4();

            Line_Number_Table_Length = hexDump.Read2();
            Line_Number_Tables = new Line_Number_Table[Line_Number_Table_Length];
            for(int i = 0; i < Line_Number_Tables.Length; i ++)
            {
                Line_Number_Tables[i] = new Line_Number_Table();
                Line_Number_Tables[i].Parse(ref hexDump, constantPool);
            }

        }
    }
}
