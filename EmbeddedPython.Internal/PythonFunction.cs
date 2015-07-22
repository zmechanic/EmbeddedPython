using System;

namespace EmbeddedPython.Internal
{
    internal class PythonFunction : PythonObject, IPythonFunction
    {
        internal PythonFunction(PythonObject pyObject, string functionName)
        {
            IntPtr function = IntPtr.Zero;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    function = PythonInterop.PyObject_GetAttrString(pyObject.NativePythonObject, functionName);
                    if (function == IntPtr.Zero || PythonInterop.PyErr_Occurred() != IntPtr.Zero)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }

                    if (PythonInterop.PyCallable_Check(function) == 0)
                    {
                        throw new PythonException(string.Format("Function {0} is not callable", functionName));
                    }

                    PythonInterop.Py_IncRef(NativePythonObject);
                });
            }
            catch (Exception ex)
            {
                if (pyObject is IPythonModule)
                {
                    throw new PythonException(
                        string.Format(
                            "Encountered error \"{0}\" while getting function \"{1}\" from module \"{2}\".",
                            ex.Message,
                            functionName,
                            ((IPythonModule)pyObject).FullName),
                        ex);
                }

                throw new PythonException(
                    string.Format(
                        "Encountered error \"{0}\" while getting function \"{1}\".",
                        ex.Message,
                        functionName),
                    ex);
            }

            NativePythonObject = function;
        }

        public void Invoke()
        {
            PythonInterop.PyGILState_Invoke(() => this.InvokeInternal());
        }

        public void Invoke<T>(T arg)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg);
                this.InvokeInternal(pyarg0);
                PythonInterop.Py_DecRef(pyarg0);
            });
        }

        public void Invoke<T1, T2>(T1 arg1, T2 arg2)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                this.InvokeInternal(pyarg0, pyarg1);
                PythonInterop.Py_DecRef(pyarg0);
                PythonInterop.Py_DecRef(pyarg1);
            });
        }

        public void Invoke<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
                this.InvokeInternal(pyarg0, pyarg1, pyarg2);
                PythonInterop.Py_DecRef(pyarg0);
                PythonInterop.Py_DecRef(pyarg1);
                PythonInterop.Py_DecRef(pyarg2);
            });
        }

        public void Invoke<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
                this.InvokeInternal(pyarg0, pyarg1, pyarg2, pyarg3);
                PythonInterop.Py_DecRef(pyarg0);
                PythonInterop.Py_DecRef(pyarg1);
                PythonInterop.Py_DecRef(pyarg2);
                PythonInterop.Py_DecRef(pyarg3);
            });
        }

        public void Invoke<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg5);
                this.InvokeInternal(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4);
                PythonInterop.Py_DecRef(pyarg0);
                PythonInterop.Py_DecRef(pyarg1);
                PythonInterop.Py_DecRef(pyarg2);
                PythonInterop.Py_DecRef(pyarg3);
                PythonInterop.Py_DecRef(pyarg4);
            });
        }

        public void Invoke<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg5);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg6);
                this.InvokeInternal(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5);
                PythonInterop.Py_DecRef(pyarg0);
                PythonInterop.Py_DecRef(pyarg1);
                PythonInterop.Py_DecRef(pyarg2);
                PythonInterop.Py_DecRef(pyarg3);
                PythonInterop.Py_DecRef(pyarg4);
                PythonInterop.Py_DecRef(pyarg5);
            });
        }

        public void Invoke<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg5);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg6);
                var pyarg6 = PythonTypeConverter.ConvertToPythonType(arg7);
                this.InvokeInternal(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6);
                PythonInterop.Py_DecRef(pyarg0);
                PythonInterop.Py_DecRef(pyarg1);
                PythonInterop.Py_DecRef(pyarg2);
                PythonInterop.Py_DecRef(pyarg3);
                PythonInterop.Py_DecRef(pyarg4);
                PythonInterop.Py_DecRef(pyarg5);
                PythonInterop.Py_DecRef(pyarg6);
            });
        }

        public void Invoke<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
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
                this.InvokeInternal(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6, pyarg7);
                PythonInterop.Py_DecRef(pyarg0);
                PythonInterop.Py_DecRef(pyarg1);
                PythonInterop.Py_DecRef(pyarg2);
                PythonInterop.Py_DecRef(pyarg3);
                PythonInterop.Py_DecRef(pyarg4);
                PythonInterop.Py_DecRef(pyarg5);
                PythonInterop.Py_DecRef(pyarg6);
                PythonInterop.Py_DecRef(pyarg7);
            });
        }

        public TResult Invoke<TResult>()
        {
            var result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var presult = InvokeInternal();
                result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                PythonInterop.Py_DecRef(presult);
            });

            return result;
        }

        public TResult Invoke<T, TResult>(T arg)
        {
            var result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg);
                var presult = this.InvokeInternal(pyarg0);
                result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                PythonInterop.Py_DecRef(presult);
                PythonInterop.Py_DecRef(pyarg0);
            });
            return result;
        }

        public TResult Invoke<T1, T2, TResult>(T1 arg1, T2 arg2)
        {
            var result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var presult = this.InvokeInternal(pyarg0, pyarg1);
                result = PythonTypeConverter.ConvertToClrType<TResult>(presult);
                PythonInterop.Py_DecRef(presult);
                PythonInterop.Py_DecRef(pyarg0);
                PythonInterop.Py_DecRef(pyarg1);
            });
            return result;
        }

        public TResult Invoke<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3)
        {
            var result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
                var presult = this.InvokeInternal(pyarg0, pyarg1, pyarg2);
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
            var result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
                var presult = this.InvokeInternal(pyarg0, pyarg1, pyarg2, pyarg3);
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
            var result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg5);
                var presult = this.InvokeInternal(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4);
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
            var result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg5);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg6);
                var presult = this.InvokeInternal(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5);
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
            var result = default(TResult);

            PythonInterop.PyGILState_Invoke(() =>
            {
                var pyarg0 = PythonTypeConverter.ConvertToPythonType(arg1);
                var pyarg1 = PythonTypeConverter.ConvertToPythonType(arg2);
                var pyarg2 = PythonTypeConverter.ConvertToPythonType(arg3);
                var pyarg3 = PythonTypeConverter.ConvertToPythonType(arg4);
                var pyarg4 = PythonTypeConverter.ConvertToPythonType(arg5);
                var pyarg5 = PythonTypeConverter.ConvertToPythonType(arg6);
                var pyarg6 = PythonTypeConverter.ConvertToPythonType(arg7);
                var presult = this.InvokeInternal(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6);
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
            var result = default(TResult);

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
                var presult = this.InvokeInternal(pyarg0, pyarg1, pyarg2, pyarg3, pyarg4, pyarg5, pyarg6, pyarg7);
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

        private IntPtr InvokeInternal(params IntPtr[] args)
        {
            IntPtr result = IntPtr.Zero;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    switch (args.Length)
                    {
                        case 0:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(NativePythonObject, IntPtr.Zero);
                            break;
                        case 1:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(NativePythonObject, args[0], IntPtr.Zero);
                            break;
                        case 2:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(NativePythonObject, args[0], args[1], IntPtr.Zero);
                            break;
                        case 3:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(NativePythonObject, args[0], args[1], args[2], IntPtr.Zero);
                            break;
                        case 4:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(NativePythonObject, args[0], args[1], args[2], args[3], IntPtr.Zero);
                            break;
                        case 5:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(NativePythonObject, args[0], args[1], args[2], args[3], args[4], IntPtr.Zero);
                            break;
                        case 6:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(NativePythonObject, args[0], args[1], args[2], args[3], args[4], args[5], IntPtr.Zero);
                            break;
                        case 7:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(NativePythonObject, args[0], args[1], args[2], args[3], args[4], args[5], args[6], IntPtr.Zero);
                            break;
                        case 8:
                            result = PythonInterop.PyObject_CallFunctionObjArgs(NativePythonObject, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], IntPtr.Zero);
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
                throw new PythonException(string.Format("Could not invoke Python function. Encountered Python error \"{0}\".", ex.Message), ex);
            }

            return result;
        }
    }
}