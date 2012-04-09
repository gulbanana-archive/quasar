using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quasar.DCPU;

namespace Quasar.ABI
{
    public class CodeSegment : ISegment
    {
        private readonly List<IInstruction> instructions;
        private readonly List<Label> relativeLabels;

        public CodeSegment(IEnumerable<IInstruction> text, IEnumerable<Label> references)
        {
            instructions = text.ToList();
            relativeLabels = references.ToList();
        }

        public IList<Label> Labels
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IList<IInstruction> Instructions
        {
            get
            {
                return instructions.AsReadOnly();
            }
        }

        public ushort AssembledLength
        {
            get { throw new NotImplementedException(); }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            throw new NotImplementedException();
        }

        public ushort Base { get; set; }
    }
}
