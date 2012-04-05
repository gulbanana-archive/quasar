using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU.ABI
{
    class CodeSection : ISection
    {
        private readonly List<IInstruction> instructions;

        public CodeSection(IEnumerable<IInstruction> instructions)
        {
            instructions = instructions.ToList();
        }

        public IList<IInstruction> Instructions
        {
            get
            {
                return instructions.AsReadOnly();
            }
        }

        public IList<Label> Labels
        {
            get
            {
                return null;
            }
        }
    }
}
