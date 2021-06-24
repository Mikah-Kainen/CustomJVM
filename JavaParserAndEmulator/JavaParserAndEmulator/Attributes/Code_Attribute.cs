using CustomJVM.ConstantPoolItems;

using System;
using System.Collections.Generic;
using System.Text;

using static CustomJVM.Enum;

namespace CustomJVM.Attributes
{
    public class Exception_Table
    {
        public ushort Start_PC { get; private set; }
        public ushort End_PC { get; private set; }
        public ushort Handler_PC { get; private set; }
        public ushort Catch_Type { get; private set; }
        public Exception_Table()
        {

        }

        public void Parse(ref Memory<byte> hexDump)
        {
            Start_PC = hexDump.Read2();
            End_PC = hexDump.Read2();
            Handler_PC = hexDump.Read2();
            Catch_Type = hexDump.Read2();
        }
    }

    public class Code_Attribute : Attribute_Info
    {
        public override uint Attribute_Length { get; protected set; }
        public ushort Max_Stack { get; private set; }
        public ushort Max_Locals { get; private set; }
        public uint Code_Length { get; private set; }
        public byte[] Code { get; private set; }
        public ushort Exception_Table_Length { get; private set; }

        public Exception_Table[] Exception_Tables { get; private set; }
        public ushort Attributes_Count { get; private set; }
        public Attribute_Info[] Attributes { get; private set; }

        public Code_Attribute(ushort name_Index) 
            :base(name_Index)
        { }

        public override void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            Attribute_Length = hexDump.Read4();
            Max_Stack = hexDump.Read2();
            Max_Locals = hexDump.Read2();

            Code_Length = hexDump.Read4();
            Code = new byte[Code_Length];
            for(int i = 0; i < Code_Length; i ++)
            {
                Code[i] = hexDump.Read1();
            }

            Exception_Table_Length = hexDump.Read2();
            Exception_Tables = new Exception_Table[Exception_Table_Length];
            for(int i = 0; i < Exception_Tables.Length; i ++)
            {
                Exception_Tables[i] = new Exception_Table();
                Exception_Tables[i].Parse(ref hexDump);
            }

            Attributes_Count = hexDump.Read2();
            Attributes = new Attribute_Info[Attributes_Count];
            for(int i = 0; i < Attributes.Length; i ++)
            {
                Attributes[i] = Program.CreateAttribute_Info(ref hexDump, constantPool);

                Attributes[i].Parse(ref hexDump, constantPool);
            }
        }

    }
}
