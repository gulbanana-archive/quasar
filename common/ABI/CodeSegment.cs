using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quasar.DCPU;

namespace Quasar.ABI
{
    /// <summary>
    /// relocatable segment type containing only instructions
    /// </summary>
    public class CodeSegment : BasicSegment
    {
        public CodeSegment(IEnumerable<IInstruction> text, IEnumerable<Label> references) : base(text, references)
        {
            foreach (var instruction in text)
            {
                //if instruction is data
                //throw
            }

            throw new NotImplementedException();
        }
    }
}
