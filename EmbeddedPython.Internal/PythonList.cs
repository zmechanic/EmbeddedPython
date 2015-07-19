namespace EmbeddedPython.Internal
{
    using System;

    internal class PythonList : PythonObject, IPythonList
    {
        internal PythonList(int size)
        {
            var dictionary = IntPtr.Zero;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    dictionary = PythonInterop.PyList_New(size);
                    if (dictionary == IntPtr.Zero)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot create Python list. Encountered Python error \"{0}\".", ex.Message), ex);
            }

            NativePythonObject = dictionary;
        }

        internal PythonList(IntPtr nativePythonObject, bool incrementReference)
            : base(nativePythonObject, incrementReference)
        {
        }

        public object this[int index]
        {
            get
            {
                return Get<object>(index);
            }
            set
            {
                Set(index, value);
            }
        }

        public override int Size
        {
            get
            {
                var size = 0;

                try
                {
                    PythonInterop.PyGILState_Invoke(() =>
                    {
                        size = PythonInterop.PyList_Size(NativePythonObject);
                        if (size < 0)
                        {
                            throw PythonInterop.PyErr_Fetch();
                        }
                    });
                }
                catch (Exception ex)
                {
                    throw new PythonException(string.Format("Cannot get size of Python list. Encountered Python error \"{0}\".", ex.Message), ex);
                }

                return size;
            }
        }

        public void Set<T>(int index, T value)
        {
            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyVar = PythonTypeConverter.ConvertToPythonType(value);
                    if (PythonInterop.PyList_SetItem(NativePythonObject, index, pyVar) != 0)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot set list item at index {0}. Encountered Python error \"{1}\".", index, ex.Message), ex);
            }
        }

        public T Get<T>(int index)
        {
            T netVar = default(T);

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyVar = PythonInterop.PyList_GetItem(NativePythonObject, index);
                    if (pyVar == IntPtr.Zero)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }

                    netVar = PythonTypeConverter.ConvertToClrType<T>(pyVar);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot get list item at index {0}. Encountered Python error \"{1}\".", index, ex.Message), ex);
            }

            return netVar;
        }

        public object Get(int index, Type t)
        {
            object netVar = null;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyVar = PythonInterop.PyList_GetItem(NativePythonObject, index);
                    if (pyVar == IntPtr.Zero)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }

                    netVar = PythonTypeConverter.ConvertToClrType(pyVar, t);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot get list item at index {0}. Encountered Python error \"{1}\".", index, ex.Message), ex);
            }

            return netVar;
        }

        public void Insert<T>(int index, T value)
        {
            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyVar = PythonTypeConverter.ConvertToPythonType(value);
                    if (PythonInterop.PyList_Insert(NativePythonObject, index, pyVar) != 0)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot insert item into list at index {0}. Encountered Python error \"{1}\".", index, ex.Message), ex);
            }
        }

        public void Add<T>(T value)
        {
            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyVar = PythonTypeConverter.ConvertToPythonType(value);
                    if (PythonInterop.PyList_Append(NativePythonObject, pyVar) != 0)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot append item into list. Encountered Python error \"{0}\".", ex.Message), ex);
            }
        }

        public void Sort()
        {
            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    if (PythonInterop.PyList_Sort(NativePythonObject) != 0)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot sort list. Encountered Python error \"{0}\".", ex.Message), ex);
            }
        }

        public void Reverse()
        {
            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    if (PythonInterop.PyList_Reverse(NativePythonObject) != 0)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot reverse list. Encountered Python error \"{0}\".", ex.Message), ex);
            }
        }

        public IPythonTuple ToTuple()
        {
            IPythonTuple tuple = null;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyTuple = PythonInterop.PyList_AsTuple(NativePythonObject);
                    if (pyTuple == IntPtr.Zero)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }

                    tuple = new PythonTuple(pyTuple, false);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot convert Python list to Python tuple. Encountered Python error \"{0}\".", ex.Message), ex);
            }

            return tuple;
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