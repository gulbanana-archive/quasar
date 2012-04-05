using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.ABI
{
    public class QFXBuilder : IExecutableFactory
    {
        public IExecutable CreateExecutable(params ISection[] sections)
        {
            var csect = sections
                .Where(sect => sect is CodeSection)
                .Cast<CodeSection>()
                .Single();

            var dsect = sections
                .Where(sect => sect is DataSection)
                .Cast<DataSection>()
                .Single();

            return new FreestandingExecutable(csect, dsect);
        }
    }
}
