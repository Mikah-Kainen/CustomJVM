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
            [Enum.AttributeTypes.SourceFile] = (ushort name_index) => { return new Source_Attribute(name_index); }
        };
    }
}
