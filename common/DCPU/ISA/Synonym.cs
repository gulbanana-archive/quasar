using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU
{
    public enum Synonym : ushort
    {
        POP  = 0x18,
        PEEK = 0x19,
        PUSH = 0x1a
    }
}
