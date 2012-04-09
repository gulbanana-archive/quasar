using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU
{
    /// <summary>4 bit values which represent specific machine instructions</summary>
    public enum Opcode : ushort
    {
        Nonbasic    = 0x0,
        Set         = 0x1,
        Add         = 0x2,
        Subtract    = 0x3,
        Multiply    = 0x4,
        Divide      = 0x5,
        Mod         = 0x6,
        ShiftLeft   = 0x7,
        ShiftRight  = 0x8,
        And         = 0x9,
        Or          = 0xa,
        ExclusiveOr = 0xb,
        IfEqual     = 0xc,
        IfNotEqual  = 0xd,
        IfGreater   = 0xe,
        IfBitwise   = 0xf
    }
}
