using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.ABI
{
    public class DataSection : ISection
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
