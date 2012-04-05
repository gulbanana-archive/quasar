using Quasar.DCPU;

namespace Quasar.ABI
{
    /// <summary>
    /// a DCPU executable in various formats
    /// </summary>
    public interface IExecutable : IAssemblable
    {
        string FileExtension { get; }
    }
}
