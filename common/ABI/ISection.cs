using System.Collections.Generic;
using Quasar.DCPU;

namespace Quasar.ABI
{
    public interface ISection : IAssemblable
    {
        IList<Label> Labels { get; }
    }
}
