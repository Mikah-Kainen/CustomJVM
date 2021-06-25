using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.Managers
{
    public class FieldManager : IEnumerable<Field_Info>, IParseByRef
    {
        Field_Info[] fields;

        public FieldManager()
        {

        }

        public void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            ushort field_Info_Count = hexDump.Read2();
            fields = new Field_Info[field_Info_Count];
            for(int i = 0; i < fields.Length; i ++)
            {
                fields[i] = new Field_Info();
                fields[i].Parse(ref hexDump, constantPool);
            }
        }
        public IEnumerator<Field_Info> GetEnumerator()
        {
            for(int i = 0; i < fields.Length; i ++)
            {
                yield return fields[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
