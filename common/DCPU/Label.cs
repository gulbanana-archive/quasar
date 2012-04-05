namespace Quasar.DCPU
{
    /// <summary>
    /// a named pointer, used in jumps and lookups - maybe stored in a header for late binding
    /// </summary>
    class Label
    {
        public readonly string Name;
        public readonly Address Address;

        public Label(string name, Address address)
        {
            Address = address;
            Name = name;
        }
    }
}
