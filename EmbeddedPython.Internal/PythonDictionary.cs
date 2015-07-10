using System;

namespace EmbeddedPython.Internal
{
    internal class PythonDictionary : IPythonDictionary
    {
        private readonly IntPtr _dictionary;

        internal PythonDictionary()
        {
            var dictionary = IntPtr.Zero;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    dictionary = PythonInterop.PyDict_New();
                    if (dictionary == IntPtr.Zero)
                    {
                        throw new PythonException(PythonInterop.PyErr_Fetch());
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot create dictionary. Encountered Python error \"{0}\".", ex.Message), ex);
            }

            _dictionary = dictionary;
        }

        internal PythonDictionary(PythonModule module)
        {
            var dictionary = IntPtr.Zero;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    dictionary = PythonInterop.PyModule_GetDict(module.NativePythonModule);
                    if (dictionary == IntPtr.Zero)
                    {
                        throw new PythonException(PythonInterop.PyErr_Fetch());
                    }

                    PythonInterop.Py_IncRef(dictionary);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot create dictionary for module \"{0}\". Encountered Python error \"{1}\".", module.FullName, ex.Message), ex);
            }

            _dictionary = dictionary;
        }

        internal PythonDictionary(IntPtr nativePythonDictionary)
        {
            _dictionary = nativePythonDictionary;
            PythonInterop.PyGILState_Invoke(() =>PythonInterop.Py_IncRef(_dictionary));
        }

        public IntPtr NativePythonDictionary
        {
            get { return _dictionary; }
        }

        public object this[string key]
        {
            get
            {
                return Get<object>(key);
            }
            set
            {
                this.Set(key, value);
            }
        }

        public void Set<T>(string key, T value)
        {
            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyVar = PythonTypeConverter.ConvertToPythonType(value);
                    if (PythonInterop.PyDict_SetItemString(_dictionary, key, pyVar) != 0)
                    {
                        throw new PythonException(PythonInterop.PyErr_Fetch());
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot set dictionary item {0}. Encountered Python error \"{1}\".", key, ex.Message), ex);
            }
        }

        public T Get<T>(string key)
        {
            T netVar = default(T);

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyVar = PythonInterop.PyDict_GetItemString(_dictionary, key);
                    if (pyVar == IntPtr.Zero)
                    {
                        throw new PythonException(PythonInterop.PyErr_Fetch());
                    }

                    netVar = PythonTypeConverter.ConvertToClrType<T>(pyVar);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot get dictionary item {0}. Encountered Python error \"{1}\".", key, ex.Message), ex);
            }

            return netVar;
        }

        public object Get(string key, Type t)
        {
            object netVar = null;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyVar = PythonInterop.PyDict_GetItemString(_dictionary, key);
                    if (pyVar == IntPtr.Zero)
                    {
                        throw new PythonException(PythonInterop.PyErr_Fetch());
                    }

                    netVar = PythonTypeConverter.ConvertToClrType(pyVar, t);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot get dictionary item {0}. Encountered Python error \"{1}\".", key, ex.Message), ex);
            }

            return netVar;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                PythonInterop.PyGILState_Invoke(() => PythonInterop.Py_DecRef(this._dictionary));
            }
        }
    }
}