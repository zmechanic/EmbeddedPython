using System;

namespace EmbeddedPython.Internal
{
    public class PythonFunction : IPythonFunction
    {
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
                        throw new PythonException(PythonInterop.PyErr_Fetch());
                    }

                    if (PythonInterop.PyCallable_Check(function) == 0)
                    {
                        throw new PythonException(string.Format("Function {0} is not callable", functionName));
                    }

                    PythonInterop.Py_IncRef(function);
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

        public TResult Invoke<T, TResult>(T arg)
        {
            return PythonTypeConverter.ConvertToClrType<TResult>(Invoke(
                    PythonTypeConverter.ConvertToPythonType(arg)));
        }

        public TResult Invoke<T1, T2, TResult>(T1 arg1, T2 arg2)
        {
            return PythonTypeConverter.ConvertToClrType<TResult>(Invoke(
                    PythonTypeConverter.ConvertToPythonType(arg1),
                    PythonTypeConverter.ConvertToPythonType(arg2)));
        }

        public TResult Invoke<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3)
        {
            return PythonTypeConverter.ConvertToClrType<TResult>(Invoke(
                    PythonTypeConverter.ConvertToPythonType(arg1),
                    PythonTypeConverter.ConvertToPythonType(arg2),
                    PythonTypeConverter.ConvertToPythonType(arg3)));
        }

        public TResult Invoke<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return PythonTypeConverter.ConvertToClrType<TResult>(Invoke(
                    PythonTypeConverter.ConvertToPythonType(arg1),
                    PythonTypeConverter.ConvertToPythonType(arg2),
                    PythonTypeConverter.ConvertToPythonType(arg3),
                    PythonTypeConverter.ConvertToPythonType(arg4)));
        }

        public TResult Invoke<T1, T2, T3, T4, T5, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return PythonTypeConverter.ConvertToClrType<TResult>(Invoke(
                    PythonTypeConverter.ConvertToPythonType(arg1),
                    PythonTypeConverter.ConvertToPythonType(arg2),
                    PythonTypeConverter.ConvertToPythonType(arg3),
                    PythonTypeConverter.ConvertToPythonType(arg4),
                    PythonTypeConverter.ConvertToPythonType(arg5)));
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            return PythonTypeConverter.ConvertToClrType<TResult>(Invoke(
                    PythonTypeConverter.ConvertToPythonType(arg1),
                    PythonTypeConverter.ConvertToPythonType(arg2),
                    PythonTypeConverter.ConvertToPythonType(arg3),
                    PythonTypeConverter.ConvertToPythonType(arg4),
                    PythonTypeConverter.ConvertToPythonType(arg5),
                    PythonTypeConverter.ConvertToPythonType(arg6)));
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            return PythonTypeConverter.ConvertToClrType<TResult>(Invoke(
                    PythonTypeConverter.ConvertToPythonType(arg1),
                    PythonTypeConverter.ConvertToPythonType(arg2),
                    PythonTypeConverter.ConvertToPythonType(arg3),
                    PythonTypeConverter.ConvertToPythonType(arg4),
                    PythonTypeConverter.ConvertToPythonType(arg5),
                    PythonTypeConverter.ConvertToPythonType(arg6),
                    PythonTypeConverter.ConvertToPythonType(arg7)));
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            return PythonTypeConverter.ConvertToClrType<TResult>(Invoke(
                    PythonTypeConverter.ConvertToPythonType(arg1),
                    PythonTypeConverter.ConvertToPythonType(arg2),
                    PythonTypeConverter.ConvertToPythonType(arg3),
                    PythonTypeConverter.ConvertToPythonType(arg4),
                    PythonTypeConverter.ConvertToPythonType(arg5),
                    PythonTypeConverter.ConvertToPythonType(arg6),
                    PythonTypeConverter.ConvertToPythonType(arg7),
                    PythonTypeConverter.ConvertToPythonType(arg8)));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                PythonInterop.PyGILState_Invoke(() =>PythonInterop.Py_DecRef(_function));
            }
        }

        private IntPtr Invoke(params IntPtr[] args)
        {
            IntPtr result = IntPtr.Zero;

            PythonInterop.PyGILState_Invoke(() =>
            {
                if (args.Length == 0)
                {
                    result = PythonInterop.PyObject_CallObject(_function, IntPtr.Zero);
                }
                else
                {
                    var tuple = PythonInterop.PyTuple_New(args.Length);

                    for (var i = 0; i < args.Length; i++)
                    {
                        PythonInterop.PyTuple_SetItem(tuple, i, args[i]);
                    }

                    result = PythonInterop.PyObject_CallObject(_function, tuple);
                }

                if (result == IntPtr.Zero)
                {
                    throw new PythonException(PythonInterop.PyErr_Fetch());
                }
            });

            return result;
        }
    }
}