namespace EmbeddedPython
{
    using System;

    public interface IPythonObject : IDisposable
    {
        /// <summary>
        /// Gets corresponding CLR type for the current Python object.
        /// </summary>
        Type ClrType { get; }
    }
}