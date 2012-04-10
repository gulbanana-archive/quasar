using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU
{
    /// <summary>6 bit values which are encoded directly into the first word of an instruction</summary>
    public enum Value : ushort  
    {
        RegisterLiteral = 0x00,
        RegisterAddress = 0x08,
        RegDispAddress  = 0x10,
        Pop             = 0x18,
        Peek            = 0x19,
        Push            = 0x1a,
        StackPointer    = 0x1b,
        ProgramCounter  = 0x1c,
        Overflow        = 0x1d,
        NextWordAddress = 0x1e,
        NextWordLiteral = 0x1f,
        DirectLiteral   = 0x20
    }
}
