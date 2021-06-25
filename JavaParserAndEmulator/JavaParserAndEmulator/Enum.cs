using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM
{
    public class Enum
    {
        public enum AccessFlags : ushort
        {
            ACC_PUBLIC =	0x0001,
            ACC_PRIVATE =	0x0002,
            ACC_PROTECTED = 0x0004,
            ACC_STATIC =	0x0008,
        };

        public enum AttributeTypes
        {
            Code,
            LineNumberTable,
            SourceFile
        }

        public enum OpCodes : byte
        {
            iconst_0 = 0x03,
            iconst_1 = 0x04,
            iconst_2 = 0x05,
            iconst_3 = 0x06,
            iconst_4 = 0x07,
            iconst_5 = 0x08,

            istore_0 = 0x3b,
            istore_1 = 0x3c,
            istore_2 = 0x3d,
            istore_3 = 0x3e,

            iload_0 = 0x1a,
            iload_1 = 0x1b,
            iload_2 = 0x1c,
            iload_3 = 0x1d,

            @return = 0xb1,
            ireturn = 0xac,

            bipush = 0x10,
            iadd = 0x60,
            invokestatic = 0xb8,
        };
        //MethodAccessFlags

        //FieldAccessFlags
    }
}
