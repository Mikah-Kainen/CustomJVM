using CustomJVM.Attributes;

using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM
{
    public static class Dictionaries
    {
        public static Dictionary<Enum.AttributeTypes, Func<ushort, Attribute_Info>> EnumToAttribute = new Dictionary<Enum.AttributeTypes, Func<ushort, Attribute_Info>>()
        {
            [Enum.AttributeTypes.Code] = (ushort name_Index) => {return new Code_Attribute(name_Index); },
            [Enum.AttributeTypes.LineNumberTable] = (ushort name_Index) => { return new LineNumberTable_Attribute(name_Index); },
            [Enum.AttributeTypes.SourceFile] = (ushort name_index) => { return new SourceFile_Attribute(name_index); }
        };


        public static Dictionary<byte, Action<byte[]>> OpToCommand = new Dictionary<byte, Action<byte[]>>()
        {
            [0x03] = (byte[] input) => { Program.Stack.Push(0); },
            [0x04] = (byte[] input) => { Program.Stack.Push(1); },
            [0x05] = (byte[] input) => { Program.Stack.Push(2); },
            [0x06] = (byte[] input) => { Program.Stack.Push(3); },
            [0x07] = (byte[] input) => { Program.Stack.Push(4); },
            [0x08] = (byte[] input) => { Program.Stack.Push(5); },

            //[0x3c] = 
            //[0x10] = 
            //[0x0a] = 
            //[0x3d] = 
            //[0x1c] = 
            //[0x1b] = 
            //[0x60] = 
            //[0x3e] = 
            //[0xb1] = 
        };
    }
}
