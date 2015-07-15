using System;

namespace EmbeddedPython.Internal
{
    internal class PythonObject : IDisposable
    {
        private bool _disposed;

        private IntPtr _nativePythonObject;

        public PythonObject()
        {
        }

        public PythonObject(IntPtr nativePythonObject)
        {
            _nativePythonObject = nativePythonObject;
        }

        public Type ClrType
        {
            get
            {
                return PythonTypeConverter.GetClrType(_nativePythonObject);
            }
        }

        internal IntPtr NativePythonObject
        {
            get
            {
                return _nativePythonObject;
            }

            set
            {
                if (_nativePythonObject != IntPtr.Zero)
                {
                    throw new PythonException("Pointer to native Python object cannot be changed once assigned.");
                }

                _nativePythonObject = value;
            }
        }

        public bool IsDisposed
        {
            get
            {
                return _disposed;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _disposed = true;
                }
            }
        }
    }
}