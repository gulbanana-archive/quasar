using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU.Instructions
{
    //nonbasic instructions all follow the same format: aaaaaaoooooo0000
    //a is a 6-bit destination value, o is a 6 bit opcode, and 0000 is the basic opcode
    public class NonBasicInstruction : IInstruction
    {
        private readonly ExtendedOpcode operation;
        private readonly IValue argument;

        public NonBasicInstruction(ExtendedOpcode op, IValue arg)
        {
            operation = op;
            argument = arg;
        }

        public NonBasicInstruction(string op, IValue arg)
        {
            operation = ParseOpcode(op.ToUpper());
            argument = arg;
        }

        public ushort AssembledLength
        {
            get { return (ushort)(1 + argument.AssembledLength); }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            var instructionWord = (int)operation << 4;
            instructionWord |= (argument.DirectAssemble() << 10);

            return new[] { (ushort)instructionWord }.Concat(argument.Assemble(ctx)).ToArray();
        }

        private ExtendedOpcode ParseOpcode(string operation)
        {
            switch (operation)
            {
                case "JSR":
                    return ExtendedOpcode.JumpSubroutine;

                default:
                    throw new FormatException("Unrecognised instruction " + operation);
            }
        }

    }
}
