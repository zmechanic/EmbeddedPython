using System;
using System.Collections.Generic;

namespace EmbeddedPython.Internal
{
    internal class PythonObject : IPythonObject
    {
        private bool _disposed;

        private IntPtr _nativePythonObject;

        private Dictionary<Tuple<string, int>, IPythonFunction> _registeredFunctions;

        internal PythonObject()
        {
        }

        internal PythonObject(IntPtr nativePythonObject, bool incrementReference)
        {
            NativePythonObject = nativePythonObject;

            if (incrementReference)
            {
                PythonInterop.PyGILState_Invoke(() => PythonInterop.Py_IncRef(NativePythonObject));
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

        public Type ClrType
        {
            get
            {
                return PythonTypeConverter.GetClrType(_nativePythonObject);
            }
        }

        public virtual int Size
        {
            get
            {
                var size = 0;

                try
                {
                    PythonInterop.PyGILState_Invoke(() =>
                    {
                        size = PythonInterop.PyObject_Size(NativePythonObject);
                        if (size < 0)
                        {
                            throw PythonInterop.PyErr_Fetch();
                        }
                    });
                }
                catch (Exception ex)
                {
                    throw new PythonException(string.Format("Cannot get size of Python object. Encountered Python error \"{0}\".", ex.Message), ex);
                }

                return size;
            }
        }

        public IEnumerable<string> Dir
        {
            get
            {
                IEnumerable<string> result = null;

                try
                {
                    PythonInterop.PyGILState_Invoke(() =>
                    {
                        var pyList = PythonInterop.PyObject_Dir(NativePythonObject);
                        if (pyList == IntPtr.Zero)
                        {
                            throw PythonInterop.PyErr_Fetch();
                        }

                        result = PythonTypeConverter.ConvertToClrType<IEnumerable<string>>(pyList);

                        PythonInterop.Py_DecRef(pyList);
                    });
                }
                catch (Exception ex)
                {
                    throw new PythonException(string.Format("Cannot get size of Python object. Encountered Python error \"{0}\".", ex.Message), ex);
                }

                return result;
            }
        }

        public bool IsDisposed
        {
            get
            {
                return _disposed;
            }
        }

        public long Hash
        {
            get
            {
                long hash = -1;

                PythonInterop.PyGILState_Invoke(() =>
                {
                    hash = PythonInterop.PyObject_Hash(NativePythonObject);
                });

                return hash;
            }
        }

        public bool HasAttr(string attributeName)
        {
            var result = false;

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyAttrNameStr = PythonInterop.PyString_FromString(attributeName);

                result = PythonInterop.PyObject_HasAttr(NativePythonObject, pyAttrNameStr) == 1;

                PythonInterop.Py_DecRef(pyAttrNameStr);
            });

            return result;
        }

        public T GetAttr<T>(string attributeName)
        {
            var result = default(T);

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyAttrNameStr = PythonInterop.PyString_FromString(attributeName);

                    try
                    {
                        var pyAttrValue = PythonInterop.PyObject_GetAttr(NativePythonObject, pyAttrNameStr);
                        if (pyAttrValue == IntPtr.Zero)
                        {
                            throw PythonInterop.PyErr_Fetch();
                        }

                        result = PythonTypeConverter.ConvertToClrType<T>(pyAttrValue);

                        PythonInterop.Py_DecRef(pyAttrValue);
                    }
                    finally
                    {
                        PythonInterop.Py_DecRef(pyAttrNameStr);
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot get attribute {0}. Encountered Python error \"{1}\".", attributeName, ex.Message), ex);
            }

            return result;
        }

        public object GetAttr(string attributeName)
        {
            return GetAttr<object>(attributeName);
        }

        public void SetAttr(string attributeName, object value)
        {
            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyAttrNameStr = PythonInterop.PyString_FromString(attributeName);
                    var pyAttrValue = PythonTypeConverter.ConvertToPythonType(value);

                    try
                    {
                        if (PythonInterop.PyObject_SetAttr(NativePythonObject, pyAttrNameStr, pyAttrValue) == -1)
                        {
                            throw PythonInterop.PyErr_Fetch();
                        }
                    }
                    finally
                    {
                        PythonInterop.Py_DecRef(pyAttrValue);
                        PythonInterop.Py_DecRef(pyAttrNameStr);
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot set attribute {0}. Encountered Python error \"{1}\".", attributeName, ex.Message), ex);
            }
        }

        public void DelAttr(string attributeName)
        {
            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyAttrNameStr = PythonInterop.PyString_FromString(attributeName);

                    try
                    {
                        if (PythonInterop.PyObject_DelAttr(NativePythonObject, pyAttrNameStr) == 1)
                        {
                            throw PythonInterop.PyErr_Fetch();
                        }
                    }
                    finally
                    {
                        PythonInterop.Py_DecRef(pyAttrNameStr);
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot delete attribute {0}. Encountered Python error \"{1}\".", attributeName, ex.Message), ex);
            }
        }

        public IPythonFunction GetPythonFunction(string functionName)
        {
            return new PythonFunction(this, functionName);
        }

        public Action GetVoidFunction(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(void)));

            return function.Invoke;
        }

        public Action<T> GetVoidFunction<T>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T), typeof(void)));

            return function.Invoke<T>;
        }

        public Action<T1, T2> GetVoidFunction<T1, T2>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(void)));

            return function.Invoke<T1, T2>;
        }

        public Action<T1, T2, T3> GetVoidFunction<T1, T2, T3>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(void)));

            return function.Invoke<T1, T2, T3>;
        }

        public Action<T1, T2, T3, T4> GetVoidFunction<T1, T2, T3, T4>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(void)));

            return function.Invoke<T1, T2, T3, T4>;
        }

        public Action<T1, T2, T3, T4, T5> GetVoidFunction<T1, T2, T3, T4, T5>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(void)));

            return function.Invoke<T1, T2, T3, T4, T5>;
        }

        public Action<T1, T2, T3, T4, T5, T6> GetVoidFunction<T1, T2, T3, T4, T5, T6>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(void)));

            return function.Invoke<T1, T2, T3, T4, T5, T6>;
        }

        public Action<T1, T2, T3, T4, T5, T6, T7> GetVoidFunction<T1, T2, T3, T4, T5, T6, T7>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(void)));

            return function.Invoke<T1, T2, T3, T4, T5, T6, T7>;
        }

        public Action<T1, T2, T3, T4, T5, T6, T7, T8> GetVoidFunction<T1, T2, T3, T4, T5, T6, T7, T8>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(void)));

            return function.Invoke<T1, T2, T3, T4, T5, T6, T7, T8>;
        }

        public Func<TResult> GetFunction<TResult>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(TResult)));

            return function.Invoke<TResult>;
        }

        public Func<T, TResult> GetFunction<T, TResult>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T), typeof(TResult)));

            return function.Invoke<T, TResult>;
        }

        public Func<T1, T2, TResult> GetFunction<T1, T2, TResult>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(TResult)));

            return function.Invoke<T1, T2, TResult>;
        }

        public Func<T1, T2, T3, TResult> GetFunction<T1, T2, T3, TResult>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(TResult)));

            return function.Invoke<T1, T2, T3, TResult>;
        }

        public Func<T1, T2, T3, T4, TResult> GetFunction<T1, T2, T3, T4, TResult>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(TResult)));

            return function.Invoke<T1, T2, T3, T4, TResult>;
        }

        public Func<T1, T2, T3, T4, T5, TResult> GetFunction<T1, T2, T3, T4, T5, TResult>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(TResult)));

            return function.Invoke<T1, T2, T3, T4, T5, TResult>;
        }

        public Func<T1, T2, T3, T4, T5, T6, TResult> GetFunction<T1, T2, T3, T4, T5, T6, TResult>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(TResult)));

            return function.Invoke<T1, T2, T3, T4, T5, T6, TResult>;
        }

        public Func<T1, T2, T3, T4, T5, T6, T7, TResult> GetFunction<T1, T2, T3, T4, T5, T6, T7, TResult>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(TResult)));

            return function.Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>;
        }

        public Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> GetFunction<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(string functionName)
        {
            var function = GetFunction(functionName, GetTypeHash(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(TResult)));

            return function.Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>;
        }

        public void CallMethod(string methodName)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var presult = CallMethodInternal(methodName);
                PythonInterop.Py_DecRef(presult);
            });
        }

        public void CallMethod<T>(string methodName, T arg)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg is IntPtr) && !(arg is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                }
            });
        }

        public void CallMethod<T1, T2>(string methodName, T1 arg1, T2 arg2)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                }
            });
        }

        public void CallMethod<T1, T2, T3>(string methodName, T1 arg1, T2 arg2, T3 arg3)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                }
            });
        }

        public void CallMethod<T1, T2, T3, T4>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg4);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3, pyarg4);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                    if (!(arg4 is IntPtr) && !(arg4 is IPythonObject)) PythonInterop.Py_DecRef(pyarg4);
                }
            });
        }

        public void CallMethod<T1, T2, T3, T4, T5>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg5);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                    if (!(arg4 is IntPtr) && !(arg4 is IPythonObject)) PythonInterop.Py_DecRef(pyarg4);
                    if (!(arg5 is IntPtr) && !(arg5 is IPythonObject)) PythonInterop.Py_DecRef(pyarg5);
                }
            });
        }

        public void CallMethod<T1, T2, T3, T4, T5, T6>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg5);
                var pyarg6 = PythonTypeConverter.ConvertToPythonType(arg6);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                    if (!(arg4 is IntPtr) && !(arg4 is IPythonObject)) PythonInterop.Py_DecRef(pyarg4);
                    if (!(arg5 is IntPtr) && !(arg5 is IPythonObject)) PythonInterop.Py_DecRef(pyarg5);
                    if (!(arg6 is IntPtr) && !(arg6 is IPythonObject)) PythonInterop.Py_DecRef(pyarg6);
                }
            });
        }

        public void CallMethod<T1, T2, T3, T4, T5, T6, T7>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg5);
                var pyarg6 = PythonTypeConverter.ConvertToPythonType(arg6);
                var pyarg7 = PythonTypeConverter.ConvertToPythonType(arg7);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6, pyarg7);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                    if (!(arg4 is IntPtr) && !(arg4 is IPythonObject)) PythonInterop.Py_DecRef(pyarg4);
                    if (!(arg5 is IntPtr) && !(arg5 is IPythonObject)) PythonInterop.Py_DecRef(pyarg5);
                    if (!(arg6 is IntPtr) && !(arg6 is IPythonObject)) PythonInterop.Py_DecRef(pyarg6);
                    if (!(arg7 is IntPtr) && !(arg7 is IPythonObject)) PythonInterop.Py_DecRef(pyarg7);
                }
            });
        }

        public void CallMethod<T1, T2, T3, T4, T5, T6, T7, T8>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg5);
                var pyarg6 = PythonTypeConverter.ConvertToPythonType(arg6);
                var pyarg7 = PythonTypeConverter.ConvertToPythonType(arg7);
                var pyarg8 = PythonTypeConverter.ConvertToPythonType(arg8);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6, pyarg7, pyarg8);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                    if (!(arg4 is IntPtr) && !(arg4 is IPythonObject)) PythonInterop.Py_DecRef(pyarg4);
                    if (!(arg5 is IntPtr) && !(arg5 is IPythonObject)) PythonInterop.Py_DecRef(pyarg5);
                    if (!(arg6 is IntPtr) && !(arg6 is IPythonObject)) PythonInterop.Py_DecRef(pyarg6);
                    if (!(arg7 is IntPtr) && !(arg7 is IPythonObject)) PythonInterop.Py_DecRef(pyarg7);
                    if (!(arg8 is IntPtr) && !(arg8 is IPythonObject)) PythonInterop.Py_DecRef(pyarg8);
                }
            });
        }

        public TResult CallMethod<TResult>(string methodName)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var presult = CallMethodInternal(methodName);
                result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                PythonInterop.Py_DecRef(presult);
            });

            return result;
        }

        public TResult CallMethod<T, TResult>(string methodName, T arg)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1);
                    result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg is IntPtr) && !(arg is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                }
            });

            return result;
        }

        public TResult CallMethod<T1, T2, TResult>(string methodName, T1 arg1, T2 arg2)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2);
                    result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                }
            });

            return result;
        }

        public TResult CallMethod<T1, T2, T3, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3);
                    result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                }
            });

            return result;
        }

        public TResult CallMethod<T1, T2, T3, T4, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg4);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3, pyarg4);
                    result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                    if (!(arg4 is IntPtr) && !(arg4 is IPythonObject)) PythonInterop.Py_DecRef(pyarg4);
                }
            });

            return result;
        }

        public TResult CallMethod<T1, T2, T3, T4, T5, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg5);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5);
                    result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                    if (!(arg4 is IntPtr) && !(arg4 is IPythonObject)) PythonInterop.Py_DecRef(pyarg4);
                    if (!(arg5 is IntPtr) && !(arg5 is IPythonObject)) PythonInterop.Py_DecRef(pyarg5);
                }
            });

            return result;
        }

        public TResult CallMethod<T1, T2, T3, T4, T5, T6, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg5);
                var pyarg6 = PythonTypeConverter.ConvertToPythonType(arg6);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6);
                    result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                    if (!(arg4 is IntPtr) && !(arg4 is IPythonObject)) PythonInterop.Py_DecRef(pyarg4);
                    if (!(arg5 is IntPtr) && !(arg5 is IPythonObject)) PythonInterop.Py_DecRef(pyarg5);
                    if (!(arg6 is IntPtr) && !(arg6 is IPythonObject)) PythonInterop.Py_DecRef(pyarg6);
                }
            });

            return result;
        }

        public TResult CallMethod<T1, T2, T3, T4, T5, T6, T7, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg5);
                var pyarg6 = PythonTypeConverter.ConvertToPythonType(arg6);
                var pyarg7 = PythonTypeConverter.ConvertToPythonType(arg7);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6, pyarg7);
                    result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                    if (!(arg4 is IntPtr) && !(arg4 is IPythonObject)) PythonInterop.Py_DecRef(pyarg4);
                    if (!(arg5 is IntPtr) && !(arg5 is IPythonObject)) PythonInterop.Py_DecRef(pyarg5);
                    if (!(arg6 is IntPtr) && !(arg6 is IPythonObject)) PythonInterop.Py_DecRef(pyarg6);
                    if (!(arg7 is IntPtr) && !(arg7 is IPythonObject)) PythonInterop.Py_DecRef(pyarg7);
                }
            });

            return result;
        }

        public TResult CallMethod<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg5);
                var pyarg6 = PythonTypeConverter.ConvertToPythonType(arg6);
                var pyarg7 = PythonTypeConverter.ConvertToPythonType(arg7);
                var pyarg8 = PythonTypeConverter.ConvertToPythonType(arg8);

                try
                {
                    var presult = CallMethodInternal(methodName, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6, pyarg7, pyarg8);
                    result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                    PythonInterop.Py_DecRef(presult);
                }
                finally
                {
                    if (!(arg1 is IntPtr) && !(arg1 is IPythonObject)) PythonInterop.Py_DecRef(pyarg1);
                    if (!(arg2 is IntPtr) && !(arg2 is IPythonObject)) PythonInterop.Py_DecRef(pyarg2);
                    if (!(arg3 is IntPtr) && !(arg3 is IPythonObject)) PythonInterop.Py_DecRef(pyarg3);
                    if (!(arg4 is IntPtr) && !(arg4 is IPythonObject)) PythonInterop.Py_DecRef(pyarg4);
                    if (!(arg5 is IntPtr) && !(arg5 is IPythonObject)) PythonInterop.Py_DecRef(pyarg5);
                    if (!(arg6 is IntPtr) && !(arg6 is IPythonObject)) PythonInterop.Py_DecRef(pyarg6);
                    if (!(arg7 is IntPtr) && !(arg7 is IPythonObject)) PythonInterop.Py_DecRef(pyarg7);
                    if (!(arg8 is IntPtr) && !(arg8 is IPythonObject)) PythonInterop.Py_DecRef(pyarg8);
                }
            });

            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override string ToString()
        {
            string str = null;

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyStr = PythonInterop.PyObject_Str(NativePythonObject);
                str = PythonInterop.PyString_ToString(pyStr);
                PythonInterop.Py_DecRef(pyStr);
            });

            return str;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_registeredFunctions != null)
                    {
                        foreach (var nameAndFunctionKeyValuePair in _registeredFunctions)
                        {
                            nameAndFunctionKeyValuePair.Value.Dispose();
                        }
                    }

                    _disposed = true;
                }
            }
        }

        private IPythonFunction GetFunction(string functionName, int typeHash)
        {
            IPythonFunction function;
            var functionHash = Tuple.Create(functionName, typeHash);

            lock (this)
            {
                if (_registeredFunctions == null)
                {
                    _registeredFunctions = new Dictionary<Tuple<string, int>, IPythonFunction>();
                }
            }
            
            if (_registeredFunctions.TryGetValue(functionHash, out function))
            {
                return function;
            }

            function = new PythonFunction(this, functionName);

            _registeredFunctions.Add(functionHash, function);

            return function;
        }

        private int GetTypeHash(params Type[] types)
        {
            var hash = 23;

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var type in types)
            {
                hash = (hash * 31) + type.GetHashCode();
            }

            return hash;
        }

        private IntPtr CallMethodInternal(string methodName, params IntPtr[] args)
        {
            IntPtr result = IntPtr.Zero;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var pyMethodName = PythonInterop.PyString_FromString(methodName);

                    try
                    {
                        switch (args.Length)
                        {
                            case 0:
                                result = PythonInterop.PyObject_CallMethodObjArgs(NativePythonObject, pyMethodName, IntPtr.Zero);
                                break;
                            case 1:
                                result = PythonInterop.PyObject_CallMethodObjArgs(NativePythonObject, pyMethodName, args[0], IntPtr.Zero);
                                break;
                            case 2:
                                result = PythonInterop.PyObject_CallMethodObjArgs(NativePythonObject, pyMethodName, args[0], args[1], IntPtr.Zero);
                                break;
                            case 3:
                                result = PythonInterop.PyObject_CallMethodObjArgs(NativePythonObject, pyMethodName, args[0], args[1], args[2], IntPtr.Zero);
                                break;
                            case 4:
                                result = PythonInterop.PyObject_CallMethodObjArgs(NativePythonObject, pyMethodName, args[0], args[1], args[2], args[3], IntPtr.Zero);
                                break;
                            case 5:
                                result = PythonInterop.PyObject_CallMethodObjArgs(NativePythonObject, pyMethodName, args[0], args[1], args[2], args[3], args[4], IntPtr.Zero);
                                break;
                            case 6:
                                result = PythonInterop.PyObject_CallMethodObjArgs(NativePythonObject, pyMethodName, args[0], args[1], args[2], args[3], args[4], args[5], IntPtr.Zero);
                                break;
                            case 7:
                                result = PythonInterop.PyObject_CallMethodObjArgs(NativePythonObject, pyMethodName, args[0], args[1], args[2], args[3], args[4], args[5], args[6], IntPtr.Zero);
                                break;
                            case 8:
                                result = PythonInterop.PyObject_CallMethodObjArgs(NativePythonObject, pyMethodName, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], IntPtr.Zero);
                                break;
                            default:
                                throw new PythonException(string.Format("Not supported number of {0} arguments for CallMethod method.", args.Length));
                        }

                        if (result == IntPtr.Zero)
                        {
                            throw PythonInterop.PyErr_Fetch();
                        }
                    }
                    finally
                    {
                        PythonInterop.Py_DecRef(pyMethodName);
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Could not invoke Python method \"{0}\". Encountered Python error \"{1}\".", methodName, ex.Message), ex);
            }

            return result;
        }
    }
}