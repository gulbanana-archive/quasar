using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU
{
    /// <summary>6 bit values which represent specific machine instructions</summary>
    public enum ExtendedOpcode : ushort
    {
        JumpSubroutine = 0x01
    }
}
