using CustomJVM;
using CustomJVM.ConstantPoolItems;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CustomJVM
{
    class Program
    {
        static CP_Info[] GetAllTypesThatInheritCpInfo()
        {
            Type[] allTypes = Assembly.GetAssembly(typeof(CP_Info)).GetTypes().ToArray();
            Type[] allCP_InfoTypes = allTypes.Where((Type x) => x.IsSubclassOf(typeof(CP_Info))).ToArray();
            return allCP_InfoTypes.Select((Type x) => (CP_Info)Activator.CreateInstance(x)).ToArray();
        }

        static void Main(string[] args)
        {
            CP_Info[] allConstantPoolItems = GetAllTypesThatInheritCpInfo();

            Dictionary<byte, Func<CP_Info>> map = new Dictionary<byte, Func<CP_Info>>(); 

            foreach(CP_Info item in allConstantPoolItems)
            {
                map.Add(item.Tag, new Func<CP_Info>(() =>
                {
                    return (CP_Info)Activator.CreateInstance(item.GetType());
                }));
            }


            string filePath = "Program.class";
            byte[] bytes = File.ReadAllBytes(filePath);
            Memory<byte> hexDump = bytes.AsMemory();

            uint magic = hexDump.Read4();
            ushort minor_version = hexDump.Read2();
            ushort major_version = hexDump.Read2();
            ushort constant_pool_count = hexDump.Read2();

            Constant_Pool constant_Pool = new Constant_Pool(constant_pool_count - 1);

            for(int i = 0; i < constant_pool_count - 1; i ++)
            {
                byte tag = hexDump.Read1();
                CP_Info currentInfo = map[tag]();

                currentInfo.Parse(ref hexDump);
                constant_Pool.Set(currentInfo);
            }
        }
    }
}
