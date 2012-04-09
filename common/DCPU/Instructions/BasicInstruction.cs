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

        public ushort AssembledLength
        {
            get { return (ushort)(1 + destination.AssembledLength + source.AssembledLength); }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}
