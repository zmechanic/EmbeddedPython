namespace EmbeddedPython
{
    public class PythonImportError : PythonException
    {
        private readonly string _name;
        private readonly string _path;

        public PythonImportError(string message, string name, string path)
            : base(message)
        {
            _name = name;
            _path = path;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }
        }
    }
}