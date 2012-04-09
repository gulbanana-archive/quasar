using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU.Instructions
{
    //basic instructions all follow the same format: bbbbbbaaaaaaoooo
    //b is a 6-bit source value, a is a 6-bit destination value, and o is a 4 bit opcode
    public class BasicInstruction : IInstruction
    {
        private readonly Opcode operation;
        private readonly IValue destination;
        private readonly IValue source;

        public BasicInstruction(Opcode op, IValue dest, IValue src)
        {
            operation = op;
            destination = dest;
            source = src;
        }

        public BasicInstruction(string op, IValue dest, IValue src)
        {
            operation = ParseOpcode(op.ToUpper());
            destination = dest;
            source = src;
        }

        public ushort AssembledLength
        {
            get { return (ushort)(1 + destination.AssembledLength + source.AssembledLength); }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            var instructionWord = (int)operation;
            instructionWord |= (destination.DirectAssemble() << 4);
            instructionWord |= (source.DirectAssemble() << 10);

            return new[] { (ushort)instructionWord }.Concat(destination.Assemble(ctx)).Concat(source.Assemble(ctx)).ToArray();
        }

        private Opcode ParseOpcode(string operation)
        {
            switch (operation)
            {
                case "ADD":
                    return Opcode.Add;

                case "SET":
                    return Opcode.Set;

                case "SUB":
                case "SUBTRACT":
                    return Opcode.Subtract;

                case "MUL":
                case "MULTIPLY":
                    return Opcode.Multiply;

                case "DIV":
                case "DIVIDE":
                    return Opcode.Divide;

                case "MOD":
                    return Opcode.Mod;

                case "SHL":
                case "SHIFTLEFT":
                case "LEFTSHIFT":
                    return Opcode.ShiftLeft;

                case "SHR":
                case "SHIFTRIGHT":
                case "RIGHTSHIFT":
                    return Opcode.ShiftRight;

                case "AND":
                    return Opcode.And;

                case "OR":
                case "BOR":
                    return Opcode.Or;

                case "XOR":
                    return Opcode.ExclusiveOr;

                case "IFE":
                case "EQUAL":
                    return Opcode.IfEqual;

                case "IFN":
                case "NOTEQUAL":
                    return Opcode.IfNotEqual;

                case "IFG":
                case "GREATER":
                    return Opcode.IfGreater;

                case "IFB":
                    return Opcode.IfBitwise;

                default:
                    throw new FormatException("Unrecognised instruction " + operation);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}, {2}", operation, destination, source);
        }
    }
}
