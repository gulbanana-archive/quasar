using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.ABI
{
    public class QFXBuilder : IExecutableFactory
    {
        public IExecutable CreateExecutable(IEnumerable<ISegment> segments)
        {
            var csect = segments
                .Where(sect => sect is CodeSegment)
                .Cast<CodeSegment>()
                .Single();

            var dsect = segments
                .Where(sect => sect is DataSegment)
                .Cast<DataSegment>()
                .Single();

            return new FreestandingExecutable(csect, dsect);
        }
    }
}
