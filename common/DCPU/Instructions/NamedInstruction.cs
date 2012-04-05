using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU.Instructions
{
    class NamedInstruction
    {
        private readonly IInstruction inner;

        public NamedInstruction(IInstruction source, string name)
        {
            inner = source;
        }
    }
}
