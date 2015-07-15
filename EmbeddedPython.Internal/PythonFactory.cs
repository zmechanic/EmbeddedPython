﻿namespace EmbeddedPython.Internal
{
    internal class PythonFactory : IPythonTypeFactory
    {
        public IPythonDictionary CreateDictionary()
        {
            return new PythonDictionary();
        }

        public IPythonTuple CreateTuple(int size)
        {
            return new PythonTuple(size);
        }

        public IPythonList CreateList()
        {
            return new PythonList();
        }
    }
}