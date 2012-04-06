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

        public CodeSegment(IEnumerable<IInstruction> text)
        {
            instructions = text.ToList();
        }

        public IList<Label> Labels
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public byte[] Assemble()
        {
            throw new NotImplementedException();
        }

        public IList<IInstruction> Instructions
        {
            get
            {
                return instructions.AsReadOnly();
            }
        }
    }
}
