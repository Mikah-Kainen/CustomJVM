using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.Managers
{
    public class AttributeManager : IEnumerable<Attribute_Info>, IParseByRef
    {
        Attribute_Info[] attributes;

        public AttributeManager()
        {

        }

        public void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            int attribute_Info_Count = hexDump.Read2();
            attributes = new Attribute_Info[attribute_Info_Count];
            for(int i = 0; i < attributes.Length; i ++)
            {
                attributes[i] = Program.CreateAttribute_Info(ref hexDump, constantPool);
                attributes[i].Parse(ref hexDump, constantPool);
            }
        }

        public IEnumerator<Attribute_Info> GetEnumerator()
        {
            for(int i = 0; i < attributes.Length; i ++)
            {
                yield return attributes[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
