using CustomJVM.ConstantPoolItems;

using System;
using System.Collections.Generic;
using System.Text;

namespace CustomJVM
{
    public class Constant_Pool
    {
        CP_Info[] cp_Info;
        int index;
        public Constant_Pool(int size)
        {
            cp_Info = new CP_Info[size];
        }

        public void Set(CP_Info input)
        {
            cp_Info[index] = input;
            index++;
        }

    }
}
