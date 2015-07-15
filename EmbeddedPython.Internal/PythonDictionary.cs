using System;
using System.Collections.Generic;

namespace EmbeddedPython.Internal
{
    internal class PythonDictionary : IPythonDictionary
    {
        private bool _disposed;
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
                        throw PythonInterop.PyErr_Fetch();
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
                        throw PythonInterop.PyErr_Fetch();
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
            PythonInterop.PyGILState_Invoke(() => PythonInterop.Py_IncRef(_dictionary));
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

        public IEnumerable<string> Keys
        {
            get
            {
                var pyList = PythonInterop.PyDict_Keys(_dictionary);
                var list = PythonTypeConverter.ConvertToClrType<IEnumerable<string>>(pyList);

                return list;
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
                        throw PythonInterop.PyErr_Fetch();
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
                        throw PythonInterop.PyErr_Fetch();
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
                        throw PythonInterop.PyErr_Fetch();
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

        public bool HasKey(string key)
        {
            bool hasKey = false;

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyKey = PythonTypeConverter.ConvertToPythonType(key);
                hasKey = PythonInterop.PyMapping_HasKey(_dictionary, pyKey) != 0;
                PythonInterop.Py_DecRef(pyKey);
            });

            return hasKey;
        }

        public void Delete(string key)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyKey = PythonTypeConverter.ConvertToPythonType(key);
                PythonInterop.PyDict_DelItem(_dictionary, pyKey);
                PythonInterop.Py_DecRef(pyKey);
            });
        }

        public void Clear()
        {
            PythonInterop.PyGILState_Invoke(() => PythonInterop.PyDict_Clear(this._dictionary));
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
                    PythonInterop.PyGILState_Invoke(() => PythonInterop.Py_DecRef(this._dictionary));
                }

                _disposed = true;
            }
        }
    }
}