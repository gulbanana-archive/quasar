using System.Collections.Generic;
using Irony.Parsing;
using Quasar.ABI;

namespace Quasar.Assembler
{
    public interface ISegmentFactory
    {
        void BuildSegments(ParseTreeNode source);
        IEnumerable<ISegment> Segments { get; }
    }
}
