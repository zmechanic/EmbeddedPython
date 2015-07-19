namespace EmbeddedPython
{
    /// <summary>
    /// Python syntax error.
    /// </summary>
    public class PythonSyntaxError : PythonException
    {
        private readonly string _sourceFileName;

        private readonly int _lineNumber;

        private readonly int _colNumber;

        private readonly string _line;

        public PythonSyntaxError(string message, string sourceFileName, int lineNumber, int colNumber, string line)
            : base(message)
        {
            _sourceFileName = sourceFileName;
            _lineNumber = lineNumber;
            _colNumber = colNumber;
            _line = line;
        }

        public string SourceFileName
        {
            get
            {
                return this._sourceFileName;
            }
        }

        public int LineNumber
        {
            get
            {
                return this._lineNumber;
            }
        }

        public int ColNumber
        {
            get
            {
                return this._colNumber;
            }
        }

        public string Line
        {
            get
            {
                return this._line;
            }
        }
    }
}