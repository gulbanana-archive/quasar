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
            var csects = segments
                .Where(sect => sect is CodeSegment)
                .Cast<CodeSegment>();

            var dsects = segments
                .Where(sect => sect is DataSegment)
                .Cast<DataSegment>();

            if (csects.Count() != 1 || dsects.Count() > 1)
            {
                throw new BadImageFormatException(".qfx: must be built from exactly one code segment and one or zero data segments");
            }

            return new FreestandingExecutable(csects.Single(), dsects.SingleOrDefault());
        }
    }
}
