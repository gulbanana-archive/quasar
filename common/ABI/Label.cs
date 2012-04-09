using Quasar.DCPU;

namespace Quasar.ABI
{
    /// <summary>
    /// a named pointer, used in jumps and lookups - maybe stored in a header for late binding
    /// </summary>
    public class Label
    {
        public readonly string Name;
        public readonly Address Address;

        public Label(string name, ushort address, bool absolute = false)
        {
            Address = new Address(address, absolute);
            Name = name;
        }
    }
}
