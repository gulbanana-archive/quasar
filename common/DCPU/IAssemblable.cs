using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU
{
    /// <summary>
    /// represents an object which can be assembled to machine code
    /// </summary>
    public interface IAssemblable
    {
        ushort AssembledLength { get; }
        ushort[] Assemble(AssemblyContext ctx);
    }
}
