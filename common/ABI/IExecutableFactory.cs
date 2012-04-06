using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.ABI
{
    public interface IExecutableFactory
    {
        IExecutable CreateExecutable(IEnumerable<ISegment> segments);
    }
}
