using System.Linq;
using Quasar.DCPU;

namespace Quasar.ABI
{
    /// <summary>
    /// simple executable type, not capable of relocation, containing intermixed code and data
    /// a qbx program assumes it is the only thing on the machine and that it can put anything
    /// anywhere it likes
    /// </summary>
    class BasicExecutable : IExecutable
    {
        private readonly BasicSegment text;

        public BasicExecutable(BasicSegment bsect)
        {
            text = bsect;
        }

        public string FileExtension
        {
            get { return "qbx"; }
        }

        public ushort AssembledLength
        {
            get { return (ushort)text.AssembledLength; }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            ctx.FullyResolvedLabels = text.Labels.Resolve(0).ToList();

            return text.Assemble(ctx);
        }
    }
}
