using System;

namespace Quasar.DCPU.Values
{
    /// <summary>
    /// literals in the source can be represented in two ways: as a value from 0x00 to 0x1f, 
    /// encoded directly into the 6-bit value field as an offset from 0x20, or in the next 
    /// word - in which case the 6-bit value field contains 0x1f.
    /// </summary>
    public class LiteralValue : IValue
    {
        private const ushort maxDirectValue = 0x1f;
        private const ushort directValueOffset = 0x20;

        private readonly ushort literal;

        public LiteralValue(ushort constant)
        {
            literal = constant;
        }

        public ushort AssembledLength
        {
            get
            {
                if (literal > maxDirectValue)
                    return (ushort)1;
                else
                    return (ushort)0;
            }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            if (literal > maxDirectValue)
                return new ushort[] { literal };
            else
                return new ushort[] { };
        }

        public ushort DirectAssemble()
        {
            if (literal > maxDirectValue)
                return (ushort)(Value.NextWordLiteral);
            else
                return (ushort)(literal + directValueOffset);
        }

        public override string ToString()
        {
            return literal.ToString();
        }
    }
}
