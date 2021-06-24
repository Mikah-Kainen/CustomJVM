using CustomJVM;
using CustomJVM.ConstantPoolItems;
using CustomJVM.Managers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using static CustomJVM.Enum;

namespace CustomJVM
{
    class Program
    {
        public static Attribute_Info CreateAttribute_Info(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            ushort name_Index = hexDump.Read2();
            CP_Utf8_Info info = (CP_Utf8_Info)constantPool[name_Index - 1];
            AttributeTypes type = (AttributeTypes)System.Enum.Parse(typeof(AttributeTypes), info.String());
            return Dictionaries.EnumToAttribute[type](name_Index);
        }

        public static T[] GetAllTypesThatInheritT<T>()
        {
            Type[] allTypes = Assembly.GetAssembly(typeof(T)).GetTypes().ToArray();
            Type[] allCP_InfoTypes = allTypes.Where((Type x) => x.IsSubclassOf(typeof(CP_Info))).ToArray();
            return allCP_InfoTypes.Select((Type x) => (T)Activator.CreateInstance(x)).ToArray();
        }

        static void Main(string[] args)
        {
            #region Setup
            CP_Info[] allConstantPoolItems = GetAllTypesThatInheritT<CP_Info>();

            
            Dictionary<byte, Func<CP_Info>> map = new Dictionary<byte, Func<CP_Info>>();

            foreach (CP_Info item in allConstantPoolItems)
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
            #endregion

            #region ConstantPool
            Constant_Pool constant_Pool = new Constant_Pool();
            constant_Pool.Parse(ref hexDump, map);
            #endregion

            #region Interfaces
            ushort access_Flags = hexDump.Read2();
            ushort this_Class = hexDump.Read2();
            ushort super_Class = hexDump.Read2();

            ushort interfaces_Count = hexDump.Read2();
            ushort[] interfaces = new ushort[interfaces_Count];
            for(int i = 0; i < interfaces.Length; i ++)
            {
                interfaces[i] = hexDump.Read2();
            }
            #endregion

            #region FieldInfo
            FieldManager fieldManager = new FieldManager();
            fieldManager.Parse(ref hexDump, constant_Pool);
            #endregion

            #region MethodInfo
            MethodManager methodManger = new MethodManager();
            methodManger.Parse(ref hexDump, constant_Pool);
            #endregion

            #region AttributeInfo
            AttributeManager attributeManager = new AttributeManager();
            attributeManager.Parse(ref hexDump, constant_Pool);
            #endregion


            //make an enum for the commands and 09
            //make a byte[] for parser
            foreach(var info in methodManger)
            {
                CP_Utf8_Info currentUtf8 = (CP_Utf8_Info)constant_Pool[info.Name_Index - 1];
                string currentString = currentUtf8.String();

                CP_Utf8_Info descriptorUtf8 = (CP_Utf8_Info)constant_Pool[info.Descriptor_Index - 1];
                string descriptorString = descriptorUtf8.String();

                string MainDescriptorString = "([Ljava/lang/String;)V";
                if (currentString == "main" && info.Access_Flags == 9 && descriptorString == MainDescriptorString)
                {

                }
            }

        }

        //////////////////////////////https://docs.oracle.com/javase/specs/jvms/se16/html/jvms-4.html
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
