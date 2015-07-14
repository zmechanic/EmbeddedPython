using System;

namespace EmbeddedPython.Internal
{
    public class PythonFunction : IPythonFunction
    {
        private bool _disposed;
        private readonly IntPtr _function;

        internal PythonFunction(PythonModule module, string functionName)
        {
            IntPtr function = IntPtr.Zero;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    function = PythonInterop.PyObject_GetAttrString(module.NativePythonModule, functionName);
                    if (function == IntPtr.Zero || PythonInterop.PyErr_Occurred() != IntPtr.Zero)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }

                    if (PythonInterop.PyCallable_Check(function) == 0)
                    {
                        throw new PythonException(string.Format("Function {0} is not callable", functionName));
                    }

                    //PythonInterop.Py_IncRef(function);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Encountered error \"{0}\" while getting function \"{1}\" from module \"{2}\".", ex.Message, functionName, module.FullName), ex);
            }

            _function = function;
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
                    //PythonInterop.PyGILState_Invoke(() => PythonInterop.Py_DecRef(_function));
                }

                _disposed = true;
            }
        }

        public TResult Invoke<TResult>()
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var presult = Invoke();
                result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                PythonInterop.Py_DecRef(presult);
            });

            return result;
        }

        public TResult Invoke<T, TResult>(T arg)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg);
                var presult = Invoke(pyarg0);
                result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                PythonInterop.Py_DecRef(presult);
                PythonInterop.Py_DecRef(pyarg0);
            });
            return result;
        }

        public TResult Invoke<T1, T2, TResult>(T1 arg1, T2 arg2)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var presult = Invoke(pyarg0, pyarg1);
                result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                PythonInterop.Py_DecRef(presult);
                PythonInterop.Py_DecRef(pyarg0);
                PythonInterop.Py_DecRef(pyarg1);
            });
            return result;
        }

        public TResult Invoke<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
                var presult = Invoke(pyarg0, pyarg1, pyarg2);
                result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                PythonInterop.Py_DecRef(presult);
                PythonInterop.Py_DecRef(pyarg0);
                PythonInterop.Py_DecRef(pyarg1);
                PythonInterop.Py_DecRef(pyarg2);
            });
            return result;
        }

        public TResult Invoke<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
            var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
            var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
            var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
            var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
                var presult = Invoke(pyarg0, pyarg1, pyarg2, pyarg3);
            result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
            PythonInterop.Py_DecRef(presult);
            PythonInterop.Py_DecRef(pyarg0);
            PythonInterop.Py_DecRef(pyarg1);
            PythonInterop.Py_DecRef(pyarg2);
            PythonInterop.Py_DecRef(pyarg3);
            });
            return result;
        }

        public TResult Invoke<T1, T2, T3, T4, T5, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
            var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
            var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
            var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
            var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
            var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg5);
                var presult = Invoke(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4);
            result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
            PythonInterop.Py_DecRef(presult);
            PythonInterop.Py_DecRef(pyarg0);
            PythonInterop.Py_DecRef(pyarg1);
            PythonInterop.Py_DecRef(pyarg2);
            PythonInterop.Py_DecRef(pyarg3);
            PythonInterop.Py_DecRef(pyarg4);
            });
            return result;
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
            var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
            var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
            var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
            var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
            var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg5);
            var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg6);
                var presult = Invoke(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5);
            result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
            PythonInterop.Py_DecRef(presult);
            PythonInterop.Py_DecRef(pyarg0);
            PythonInterop.Py_DecRef(pyarg1);
            PythonInterop.Py_DecRef(pyarg2);
            PythonInterop.Py_DecRef(pyarg3);
            PythonInterop.Py_DecRef(pyarg4);
            PythonInterop.Py_DecRef(pyarg5);
            });
            return result;
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
            var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
            var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
            var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
            var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
            var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg5);
            var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg6);
            var pyarg6 = PythonTypeConverter.ConvertToPythonType(arg7);
                var presult = Invoke(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6);
            result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
            PythonInterop.Py_DecRef(presult);
            PythonInterop.Py_DecRef(pyarg0);
            PythonInterop.Py_DecRef(pyarg1);
            PythonInterop.Py_DecRef(pyarg2);
            PythonInterop.Py_DecRef(pyarg3);
            PythonInterop.Py_DecRef(pyarg4);
            PythonInterop.Py_DecRef(pyarg5);
            PythonInterop.Py_DecRef(pyarg6);
            });
            return result;
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            TResult result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
            var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
            var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
            var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
            var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg5);
            var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg6);
            var pyarg6 = PythonTypeConverter.ConvertToPythonType(arg7);
            var pyarg7 = PythonTypeConverter.ConvertToPythonType(arg8);
                var presult = Invoke(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6, pyarg7);
            result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
            PythonInterop.Py_DecRef(presult);
            PythonInterop.Py_DecRef(pyarg0);
            PythonInterop.Py_DecRef(pyarg1);
            PythonInterop.Py_DecRef(pyarg2);
            PythonInterop.Py_DecRef(pyarg3);
            PythonInterop.Py_DecRef(pyarg4);
            PythonInterop.Py_DecRef(pyarg5);
            PythonInterop.Py_DecRef(pyarg6);
            PythonInterop.Py_DecRef(pyarg7);
            });
            return result;
        }

        private IntPtr Invoke(params IntPtr[] args)
        {
            IntPtr result = IntPtr.Zero;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    switch (args.Length)
                    {
                        case 0:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(_function, IntPtr.Zero);
                            break;
                        case 1:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(_function, args[0], IntPtr.Zero);
                            break;
                        case 2:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(_function, args[0], args[1], IntPtr.Zero);
                            break;
                        case 3:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(_function, args[0], args[1], args[2], IntPtr.Zero);
                            break;
                        case 4:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(_function, args[0], args[1], args[2], args[3], IntPtr.Zero);
                            break;
                        case 5:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(_function, args[0], args[1], args[2], args[3], args[4], IntPtr.Zero);
                            break;
                        case 6:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(_function, args[0], args[1], args[2], args[3], args[4], args[5], IntPtr.Zero);
                            break;
                        case 7:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(_function, args[0], args[1], args[2], args[3], args[4], args[5], args[6], IntPtr.Zero);
                            break;
                        case 8:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(_function, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], IntPtr.Zero);
                            break;
                        default:
                            throw new PythonException(string.Format("Not supported number of {0} arguments for CallFunction method.", args.Length));
                    }

                    if (result == IntPtr.Zero)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Could not invoke function. Encountered Python error \"{0}\".", ex.Message), ex);
            }

            return result;
        }
    }
}