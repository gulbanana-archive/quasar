using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU.Instructions
{
    public class CharacterData : IInstruction
    {
        private readonly string characters;

        public CharacterData(string value)
        {
            characters = value;
        }

        public ushort AssembledLength
        {
            get { return (ushort)characters.Length; }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            return characters
                .Select(c => (ushort)c)
                .ToArray();
        }
    }
}
