using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.ABI
{
    public class QBXBuilder : IExecutableFactory
    {
        public IExecutable CreateExecutable(IEnumerable<ISegment> segments)
        {
            var bsects = segments
                .Where(sect => sect is BasicSegment)
                .Cast<BasicSegment>();

            if (bsects.Count() != 1 || segments.Count() != 1)
            {
                throw new BadImageFormatException(".qbx: must be built from a single nonrelocatable segment");
            }

            return new BasicExecutable(bsects.Single());
        }
    }
}
