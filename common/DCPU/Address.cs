namespace Quasar.DCPU
{
    /// <summary>
    /// represents a relocatable or fixed address within the DCPU's heap
    /// </summary>
    public class Address
    {
        public readonly bool Absolute;
        public readonly ushort Pointer;

        public Address(ushort address, bool absolute = false)
        {
            Pointer = address;
            Absolute = absolute;
        }
    }
}
