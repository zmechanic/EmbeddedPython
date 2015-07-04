using System;

namespace EmbeddedPython
{
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