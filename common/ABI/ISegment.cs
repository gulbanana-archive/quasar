using System.Collections.Generic;
using Quasar.DCPU;

namespace Quasar.ABI
{
    public interface ISegment : IAssemblable
    {
        IList<Label> Labels { get; }
    }
}
