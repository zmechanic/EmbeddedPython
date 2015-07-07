﻿using System;
using System.Collections.Generic;

namespace EmbeddedPython
{
    /// <summary>
    /// Main entry point to interact with Python.
    /// </summary>
    public interface IPython : IDisposable
    {
        /// <summary>
        /// Converts Python type of variable given by <paramref name="value"/> pointer to native  to CLR type.
        /// </summary>
        /// <param name="value">Pointer to native Python type.</param>
        /// <returns>CLR type.</returns>
        Type GetClrType(IntPtr value);

        /// <summary>
        /// Gets main Python module.
        /// </summary>
        IPythonModule MainModule { get; }

        /// <summary>
        /// Imports Python module from ".py" file.
        /// </summary>
        /// <param name="modulePath">Relative or full path to the module.</param>
        /// <param name="moduleName">Name of the module. Usually file name without ".py" extension.</param>
        /// <returns>Reference to loaded <see cref="IPythonModule"/>.</returns>
        IPythonModule ImportModule(string modulePath, string moduleName);

        /// <summary>
        /// Gets factory to construct Python specific types.
        /// </summary>
        IPythonTypeFactory Factory { get; }
    }
}