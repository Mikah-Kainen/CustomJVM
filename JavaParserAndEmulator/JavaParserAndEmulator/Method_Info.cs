using CustomJVM.Attributes;
using CustomJVM.ConstantPoolItems;
using CustomJVM.Managers;

using System;
using System.Collections.Generic;
using System.Text;

using static CustomJVM.Enum;

namespace CustomJVM
{
    public class Method_Info : IParseByRef
    {
        public ushort Access_Flags;
        public ushort Name_Index;
        public ushort Descriptor_Index;
        public ushort Attributes_Count;
        public uint?[] Locals;
        public Code_Attribute CodeAttribute;

        public Attribute_Info[] Attributes;

        public Method_Info()
        {

        }

        public void Parse(ref Memory<byte> hexDump, Constant_Pool constantPool)
        {
            Access_Flags = hexDump.Read2();
            Name_Index = hexDump.Read2();
            Descriptor_Index = hexDump.Read2();
            Attributes_Count = hexDump.Read2();

            Attributes = new Attribute_Info[Attributes_Count];
            for (int i = 0; i < Attributes.Length; i++)
            {
                Attribute_Info current = Program.CreateAttribute_Info(ref hexDump, constantPool);
                current.Parse(ref hexDump, constantPool);
                Attributes[i] = current;
            }

            foreach (Attribute_Info attribute in Attributes)
            {
                if (attribute.GetType() == typeof(Code_Attribute))
                {
                    CodeAttribute = (Code_Attribute)attribute;
                    Locals = new uint?[CodeAttribute.Max_Locals];
                }
            }
        }

        public uint? Execute(Constant_Pool constantPool, MethodManager manager)
        {

            Memory<byte> bytes = CodeAttribute.Code.AsMemory();
            while (bytes.Length > 0)
            {
                OpCodes currentOp = (OpCodes)bytes.Read1();
                switch (currentOp)
                {
                    #region iconst
                    case OpCodes.iconst_0:
                        Program.Stack.Push(0);
                        break;

                    case OpCodes.iconst_1:
                        Program.Stack.Push(1);
                        break;

                    case OpCodes.iconst_2:
                        Program.Stack.Push(2);
                        break;

                    case OpCodes.iconst_3:
                        Program.Stack.Push(3);
                        break;

                    case OpCodes.iconst_4:
                        Program.Stack.Push(4);
                        break;

                    case OpCodes.iconst_5:
                        Program.Stack.Push(5);
                        break;
                    #endregion

                    #region istore
                    case OpCodes.istore_0:
                        Locals[0] = Program.Stack.Pop();
                        break;

                    case OpCodes.istore_1:
                        Locals[1] = Program.Stack.Pop();
                        break;

                    case OpCodes.istore_2:
                        Locals[2] = Program.Stack.Pop();
                        break;

                    case OpCodes.istore_3:
                        Locals[3] = Program.Stack.Pop();
                        break;
                    #endregion

                    #region iload;
                    case OpCodes.iload_0:
                        Program.Stack.Push(Locals[0]);
                        break;

                    case OpCodes.iload_1:
                        Program.Stack.Push(Locals[1]);
                        break;

                    case OpCodes.iload_2:
                        Program.Stack.Push(Locals[2]);
                        break;

                    case OpCodes.iload_3:
                        Program.Stack.Push(Locals[3]);
                        break;
                    #endregion;

                    case OpCodes.@return:
                        return null;
                        break;

                    case OpCodes.ireturn:
                        return Program.Stack.Pop();
                        break;

                    case OpCodes.bipush:
                        Program.Stack.Push(bytes.Read1());
                        break;

                    case OpCodes.iadd:
                        Program.Stack.Push(Program.Stack.Pop() + Program.Stack.Pop());
                        break;


                    case OpCodes.invokestatic:
                        ushort methodIndex = (ushort)((bytes.Read1() << 8) | bytes.Read1());
                        CP_MethodRef_Info methodRef = (CP_MethodRef_Info)constantPool[methodIndex - 1];
                        CP_NameAndType methodName = (CP_NameAndType)constantPool[methodRef.Name_And_Type_Index - 1];

                        foreach (Method_Info method in manager)
                        {
                            if (method.Name_Index == methodName.Name_Index)
                            {
                                method.Locals[0] = Program.Stack.Pop();
                                method.Locals[1] = Program.Stack.Pop();
                                Program.Stack.Push(method.Execute(constantPool, manager));
                            }
                        }
                        break;


                    default:
                        throw new Exception("Something is kinda wrong");
                        break;
                }
            }
            return null;
        }
    }
}
