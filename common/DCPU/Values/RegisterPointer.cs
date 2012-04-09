using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU.Values
{
    public class RegisterPointer : IValue
    {
        private readonly Register reg;
        private ushort displacement;

        public RegisterPointer(string registerName, ushort offset)
        {
            displacement = offset;
            reg = (Register)Enum.Parse(typeof(Register), registerName.ToUpper());
        }

        public ushort DirectAssemble()
        {
            if (displacement == 0)
                return (ushort)(reg + (ushort)Value.RegisterAddress);
            else
                return (ushort)(reg + (ushort)Value.RegDispAddress);
        }

        public ushort AssembledLength
        {
            get
            {
                if (displacement == 0)
                    return 0;
                else
                    return 1;
            }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            if (displacement == 0)
                return new ushort[] { };
            else
                return new ushort[] { displacement };
        }

        public override string ToString()
        {
            if (displacement > 0)
                return string.Format("[{0}+{1}]", Enum.GetName(typeof(Register), reg), displacement);
            else if (displacement < 0)
                return string.Format("[{0}-{1}]", Enum.GetName(typeof(Register), reg), displacement);
            else return string.Format("[{0}]", Enum.GetName(typeof(Register), reg));
        }
    }
}
