namespace Quasar.DCPU
{
    /// <summary>
    /// represents a relocatable or fixed address within the DCPU's heap
    /// </summary>
    class Address
    {
        public readonly bool Absolute;
        public readonly short Pointer;

        public Address(short address, bool absolute = false)
        {
            Pointer = address;
            Absolute = absolute;
        }
    }
}
