using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU.Instructions
{
    public class LiteralData : IInstruction
    {
        private readonly ushort literal;

        public LiteralData(ushort value)
        {
            literal = value;
        }

        public ushort AssembledLength
        {
            get { return (ushort)1; }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            return new[] { literal };
        }
    }
}
