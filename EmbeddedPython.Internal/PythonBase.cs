﻿using System;

namespace EmbeddedPython.Internal
{
    public abstract class PythonBase : IPython
    {
        public Type GetClrType(IntPtr value)
        {
            return PythonTypeConverter.GetClrType(value);
        }

        public IPythonModule MainModule
        {
            get { return PythonConcrete.MainModule; }
        }

        public IPythonModule ImportModule(string modulePath, string moduleName)
        {
            return PythonConcrete.ImportModule(modulePath, moduleName);
        }

        public IPythonTypeFactory Factory
        {
            get { return PythonConcrete.Factory; }
        }

        public abstract void Dispose();
    }
}