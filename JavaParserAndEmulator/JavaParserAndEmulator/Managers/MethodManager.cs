using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM.Managers
{
    public class MethodManager : IEnumerable<Method_Info>, IParseByRef
    {

        Method_Info[] methods;
        
        public MethodManager()
        {
        }

        public void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            ushort method_Info_Count = hexDump.Read2();
            methods = new Method_Info[method_Info_Count];
            for (int i = 0; i < methods.Length; i ++)
            {
                methods[i] = new Method_Info();
                methods[i].Parse(ref hexDump, constantPool);
            }
        }

        public IEnumerator<Method_Info> GetEnumerator()
        {
            for(int i = 0; i < methods.Length; i ++)
            {
                yield return methods[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
