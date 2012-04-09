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
        private IValue displacement;

        public RegisterPointer(string registerName, IValue offset)
        {
            displacement = offset;
            reg = (Register)Enum.Parse(typeof(Register), registerName.ToUpper());
        }

        public ushort DirectAssemble()
        {
            if (displacement == null)
                return (ushort)(reg + (ushort)Value.RegisterAddress);
            else
                return (ushort)(reg + (ushort)Value.RegDispAddress);
        }

        public ushort AssembledLength
        {
            get
            {
                if (displacement == null)
                    return 0;
                else
                    return 1;
            }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            if (displacement == null)
                return new ushort[] { };
            else
                return displacement.Assemble(ctx);
        }

        public override string ToString()
        {
            if (displacement == null)
                return string.Format("[{0}]", Enum.GetName(typeof(Register), reg));
            else
                return string.Format("[{0}+{1}]", Enum.GetName(typeof(Register), reg), displacement);
        }
    }
}
