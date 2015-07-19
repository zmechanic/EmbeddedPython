namespace EmbeddedPython
{
    public class PythonTabError : PythonIndentationError
    {
        public PythonTabError(string message, string sourceFileName, int lineNumber, int colNumber, string line)
            : base(message, sourceFileName, lineNumber, colNumber, line)
        {
        }
    }
}