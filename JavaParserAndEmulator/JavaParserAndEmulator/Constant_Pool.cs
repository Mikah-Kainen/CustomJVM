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
        ushort length;

        public CP_Info this[int index]
        {
            get
            {
                return cp_Info[index];
            }
        }
        public Constant_Pool()
        {
        }

        public void Set(CP_Info input)
        {
            cp_Info[index] = input;
            index++;
        }

        public void Parse(ref Memory<byte> hexDump, Dictionary<byte, Func<CP_Info>> map)
        {
            length = (ushort)(hexDump.Read2() - 1);
            cp_Info = new CP_Info[length];

            for (int i = 0; i < length; i++)
            {
                byte tag = hexDump.Read1();
                CP_Info currentInfo = map[tag]();

                currentInfo.Parse(ref hexDump);
                Set(currentInfo);
            }
        }

    }
}
