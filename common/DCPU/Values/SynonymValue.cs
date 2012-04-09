using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU.Values
{
    /// <summary>
    /// POP, PEEK and PUSH are three special synonyms which 
    /// </summary>
    public class SynonymValue : IValue
    {
        private readonly Synonym code;

        public SynonymValue(string synonymCode)
        {
            code = (Synonym)Enum.Parse(typeof(Synonym), synonymCode.ToUpper());
        }

        public ushort DirectAssemble()
        {
            return (ushort)code;
        }

        public ushort AssembledLength
        {
            get { return 0; }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            return new ushort[] { };
        }

        public override string ToString()
        {
            return Enum.GetName(typeof(Synonym), code);
        }
    }
}
