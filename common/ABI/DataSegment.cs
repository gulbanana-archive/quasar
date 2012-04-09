using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quasar.DCPU;

namespace Quasar.ABI
{
    public class DataSegment : ISegment
    {
        public IList<Label> Labels
        {
            get { throw new NotImplementedException(); }
        }

        public ushort Base
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ushort AssembledLength
        {
            get { throw new NotImplementedException(); }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}
