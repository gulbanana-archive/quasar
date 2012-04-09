using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU.Values
{
    public class UnresolvedSymbolValue : IValue
    {
        private readonly string symbol;

        public UnresolvedSymbolValue(string labelName)
        {
            symbol = labelName;
        }

        public ushort AssembledLength
        {
            get { return (ushort)1; }
        }

        public virtual ushort DirectAssemble()
        {
            return (ushort)Value.NextWordLiteral;
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            return new[]{ctx.FullyResolvedLabels
                .Where(label => label.Name == symbol)
                .Where(label => label.Address.Absolute)
                .Select(label => label.Address.Pointer)
                .Single()};
        }

        public override string ToString()
        {
            return symbol;
        }
    }
}
