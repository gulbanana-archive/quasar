using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quasar.DCPU;

namespace Quasar.ABI
{
    /// <summary>
    /// QFX format program, fully bound, with all strong refs resolved
    /// </summary>
    class FreestandingExecutable : IExecutable
    {
        /// <summary>executable code, located at address 0</summary>
        private readonly CodeSegment text;
        /// <summary>non-executable data, located between code and heap</summary>
        private readonly DataSegment globals;

        public FreestandingExecutable(CodeSegment csect, DataSegment dsect)
        {
            text = csect;
            globals = dsect;
        }

        public string FileExtension
        {
            get { return "qfx"; }
        }

        public ushort AssembledLength
        {
            get { return (ushort)(text.AssembledLength + globals.AssembledLength);}
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            ctx.FullyResolvedLabels = text.Labels.Concat(globals.Labels).ToList();

            return text.Assemble(ctx)
                .Concat(globals.Assemble(ctx))
                .ToArray();
        }
    }
}
