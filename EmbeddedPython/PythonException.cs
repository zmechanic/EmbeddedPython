using System;

namespace EmbeddedPython
{
    public class PythonException : Exception
    {
        private readonly string sourceFileName;

        private readonly int lineNumber;

        private readonly int colNumber;

        private readonly string line;

        public PythonException(string message)
            : base(message)
        {
        }

        public PythonException(string message, string sourceFileName, int lineNumber, int colNumber, string line)
            : base(message)
        {
            this.sourceFileName = sourceFileName;
            this.lineNumber = lineNumber;
            this.colNumber = colNumber;
            this.line = line;
        }

        public PythonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public PythonException()
        {
        }

        public string SourceFileName
        {
            get
            {
                return this.sourceFileName;
            }
        }

        public int LineNumber
        {
            get
            {
                return this.lineNumber;
            }
        }

        public int ColNumber
        {
            get
            {
                return this.colNumber;
            }
        }

        public string Line
        {
            get
            {
                return this.line;
            }
        }
    }
}