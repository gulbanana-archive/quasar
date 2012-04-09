using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quasar.ABI;

namespace Quasar.DCPU
{
    /// <summary>
    /// contains information which someone up the assembly tree thought might be needed later on
    /// </summary>
    public struct AssemblyContext
    {
        public IList<Label> FullyResolvedLabels;
    }
}
