using System;

namespace EmbeddedPython
{
    /// <summary>
    /// Generic Python exception/error.
    /// All other specific Python errors are derived from this class.
    /// </summary>
    public class PythonException : Exception
    {
        public PythonException(string message)
            : base(message)
        {
        }

        public PythonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public PythonException()
        {
        }
    }
}