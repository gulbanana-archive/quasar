using System;
using Irony.Parsing;
using Quasar.DCPU;

namespace Quasar.Assembler
{
    interface IValueBuilder
    {
        IValue BuildValue(ParseTreeNode visitee);
    }
}
