namespace EmbeddedPython.Internal
{
    internal class PythonFactory : IPythonTypeFactory
    {
        public IPythonDictionary CreateDictionary()
        {
            return new PythonDictionary();
        }
    }
}