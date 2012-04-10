using System;

namespace Quasar.DCPU.Exceptions
{
    public class DCPUException : Exception
    {
        public DCPUException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}
