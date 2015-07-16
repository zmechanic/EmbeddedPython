using System;

namespace EmbeddedPython.Internal
{
    internal class PythonTuple : PythonObject, IPythonTuple
    {
        internal PythonTuple(int size)
        {
            var dictionary = IntPtr.Zero;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    dictionary = PythonInterop.PyTuple_New(size);
                    if (dictionary == IntPtr.Zero)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot create Python tuple. Encountered Python error \"{0}\".", ex.Message), ex);
            }

            NativePythonObject = dictionary;
        }

        internal PythonTuple(IntPtr nativePythonObject, bool incrementReference)
            : base(nativePythonObject, incrementReference)
        {
        }

        public object this[int position]
        {
            get
            {
                return Get<object>(position);
            }
            set
            {
                Set(position, value);
            }
        }

        public int Size
        {
            get
            {
                var size = 0;

                try
                {
                    PythonInterop.PyGILState_Invoke(() =>
                    {
                        size = PythonInterop.PyTuple_Size(NativePythonObject);
                        if (size == 0)
                        {
                            throw PythonInterop.PyErr_Fetch();
                        }
                    });
                }
                catch (Exception ex)
                {
                    throw new PythonException(string.Format("Cannot get size of Python tuple. Encountered Python error \"{0}\".", ex.Message), ex);
                }

                return size;
            }
        }

        public void Set<T>(int position, T value)
        {
            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyVar = PythonTypeConverter.ConvertToPythonType(value);
                    if (PythonInterop.PyTuple_SetItem(NativePythonObject, position, pyVar) != 0)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot set tuple item at position {0}. Encountered Python error \"{1}\".", position, ex.Message), ex);
            }
        }

        public T Get<T>(int position)
        {
            T netVar = default(T);

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyVar = PythonInterop.PyTuple_GetItem(NativePythonObject, position);
                    if (pyVar == IntPtr.Zero)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }

                    netVar = PythonTypeConverter.ConvertToClrType<T>(pyVar);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot get tuple item at position {0}. Encountered Python error \"{1}\".", position, ex.Message), ex);
            }

            return netVar;
        }

        public object Get(int position, Type t)
        {
            object netVar = null;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyVar = PythonInterop.PyTuple_GetItem(NativePythonObject, position);
                    if (pyVar == IntPtr.Zero)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }

                    netVar = PythonTypeConverter.ConvertToClrType(pyVar, t);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot get tuple item at position {0}. Encountered Python error \"{1}\".", position, ex.Message), ex);
            }

            return netVar;
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    PythonInterop.PyGILState_Invoke(() => PythonInterop.Py_DecRef(NativePythonObject));
                }

                base.Dispose(disposing);
            }
        }
    }
}