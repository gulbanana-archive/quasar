using System;

namespace Quasar.DCPU.Values
{
    /// <summary>
    /// like other pointers, literal pointers are just a specific value encoding
    /// and then data in the next word
    /// </summary>
    public class LiteralPointer : IValue
    {
        private readonly ushort pointer;

        public LiteralPointer(ushort constant)
        {
            pointer = constant;
        }

        public ushort AssembledLength
        {
            get { return (ushort)1; }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            return new ushort[] { pointer };
        }

        public ushort DirectAssemble()
        {
            return (ushort)(Value.NextWordAddress);
        }

        public override string ToString()
        {
            return "[" + pointer.ToString() + "]";
        }
    }
}
