using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.ABI
{
    public class DataSegment : ISegment
    {
        public IList<Label> Labels
        {
            get { throw new NotImplementedException(); }
        }

        public byte[] Assemble()
        {
            throw new NotImplementedException();
        }
    }
}
