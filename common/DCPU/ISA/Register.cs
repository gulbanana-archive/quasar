using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU
{
    /// <summary>
    /// 6-bit register code
    /// </summary>
    public enum Register : ushort
    {
        A  = 0x00,
        B  = 0x01,
        C  = 0x02,
        X  = 0x03,
        Y  = 0x04,
        Z  = 0x05,
        I  = 0x06,
        J  = 0x07,
        SP = 0x1b,
        PC = 0x1c,
        O  = 0x1d
    }
}
