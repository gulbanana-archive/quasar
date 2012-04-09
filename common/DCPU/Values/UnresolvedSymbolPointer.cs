namespace Quasar.DCPU.Values
{
    public class UnresolvedSymbolPointer : UnresolvedSymbolValue
    {
        public UnresolvedSymbolPointer(string labelName) : base(labelName) {}
        
        public override ushort DirectAssemble()
        {
            return (ushort)Value.NextWordAddress;
        }
    }
}
