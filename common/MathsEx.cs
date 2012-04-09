using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar
{
    public static class MathsEx
    {
        public static ushort Add(this ushort source, ushort increment)
        {
            return unchecked((ushort)(source + increment));
        }

        public static byte[] Bytes(this ushort source)
        {
            var array = new byte[2];
            array[0] = (byte)(source & 0x00ff);
            array[1] = (byte)(source & 0xff00 >> 8);

            return array;
        }
    }
}
