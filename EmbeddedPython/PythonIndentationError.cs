namespace EmbeddedPython
{
    public class PythonIndentationError :PythonSyntaxError
    {
        public PythonIndentationError(string message, string sourceFileName, int lineNumber, int colNumber, string line)
            : base(message, sourceFileName, lineNumber, colNumber, line)
        {
        }
    }
}