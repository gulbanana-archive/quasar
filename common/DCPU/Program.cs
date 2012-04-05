using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quasar.DCPU.ABI;

namespace Quasar.DCPU
{
    /// <summary>
    /// a Program object represents a list of DCPU instructions, in symbolic form 
    /// </summary>
    public class Program
    {
        private readonly List<ISection> sections;

        public Program(IEnumerable<IInstruction> instructions)
        {
            sections = new List<ISection>();
            sections.Add(new CodeSection(instructions));
        }

        public byte[] GetObjectCode()
        {
            throw new NotImplementedException();
        }
    }
}
