using System;
using System.Collections.Generic;

namespace EmbeddedPython.Internal
{
    internal class PythonDictionary : PythonObject, IPythonDictionary
    {
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
                throw new PythonException(string.Format("Cannot create Python dictionary. Encountered Python error \"{0}\".", ex.Message), ex);
            }

            NativePythonObject = dictionary;
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

            NativePythonObject = dictionary;
        }

        internal PythonDictionary(IntPtr nativePythonObject) 
        {
            NativePythonObject = nativePythonObject;
            PythonInterop.PyGILState_Invoke(() => PythonInterop.Py_IncRef(NativePythonObject));
        }

        public object this[string key]
        {
            get
            {
                return Get<object>(key);
            }
            set
            {
                Set(key, value);
            }
        }

        public IEnumerable<string> Keys
        {
            get
            {
                var pyList = PythonInterop.PyDict_Keys(NativePythonObject);
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
                    if (PythonInterop.PyDict_SetItemString(NativePythonObject, key, pyVar) != 0)
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
                    var pyVar = PythonInterop.PyDict_GetItemString(NativePythonObject, key);
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
                    var pyVar = PythonInterop.PyDict_GetItemString(NativePythonObject, key);
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
                hasKey = PythonInterop.PyMapping_HasKey(NativePythonObject, pyKey) != 0;
                PythonInterop.Py_DecRef(pyKey);
            });

            return hasKey;
        }

        public void Delete(string key)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyKey = PythonTypeConverter.ConvertToPythonType(key);
                PythonInterop.PyDict_DelItem(NativePythonObject, pyKey);
                PythonInterop.Py_DecRef(pyKey);
            });
        }

        public void Clear()
        {
            PythonInterop.PyGILState_Invoke(() => PythonInterop.PyDict_Clear(NativePythonObject));
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