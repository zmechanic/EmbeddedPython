﻿using System;
using System.Collections.Generic;

namespace EmbeddedPython.Internal
{
    public abstract class PythonBase : IPython
    {
        public Type GetClrType(IntPtr value)
        {
            return PythonInterop.GetClrType(value);
        }

        public IPythonModule MainModule
        {
            get { return PythonConcrete.MainModule; }
        }

        public IPythonModule ImportModule(string modulePath, string moduleName)
        {
            return PythonConcrete.ImportModule(modulePath, moduleName);
        }

        public abstract void Dispose();
    }
}