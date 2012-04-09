using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU.Values
{
    public class RegisterValue : IValue
    {
        private readonly Register reg;

        public RegisterValue(string registerName)
        {
            reg = (Register)Enum.Parse(typeof(Register), registerName.ToUpper());
        }

        public ushort DirectAssemble()
        {
            return (ushort)reg;
        }

        public ushort AssembledLength
        {
            get { return 0; }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            return new ushort[] { };
        }

        public override string ToString()
        {
            return Enum.GetName(typeof(Register), reg);
        }
    }
}
