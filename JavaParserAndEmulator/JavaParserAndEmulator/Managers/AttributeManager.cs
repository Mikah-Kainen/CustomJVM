using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.Managers
{
    public class AttributeManager : IEnumerable<Attribute_Info>
    {
        Attribute_Info[] attributes;

        public AttributeManager()
        {

        }

        public void Parse(ref Memory<byte> hexDump)
        {
            int attribute_Info_Count = hexDump.Read2();
            attributes = new Attribute_Info[attribute_Info_Count];
            for(int i = 0; i < attributes.Length; i ++)
            {
                attributes[i] = new Attribute_Info();
                attributes[i].Parse(ref hexDump);
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
