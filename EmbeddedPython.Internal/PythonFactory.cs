namespace EmbeddedPython.Internal
{
    internal class PythonFactory : IPythonTypeFactory
    {
        public IPythonDictionary CreateDictionary()
        {
            return new PythonDictionary();
        }

        public IPythonTuple CreateTuple()
        {
            return new PythonTuple();
        }

        public IPythonList CreateList()
        {
            return new PythonList();
        }
    }
}