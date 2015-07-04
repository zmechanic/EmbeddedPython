using System;
using System.Collections.Generic;
using System.IO;

namespace EmbeddedPython.Internal
{
    internal class PythonModule : IPythonModule
    {
        private readonly string _modulePath;
        private readonly string _moduleName;
        private readonly IntPtr _module;
        private readonly PythonDictionary _dictionary;
        private readonly Dictionary<Tuple<string, int>, PythonFunction> _registeredFunctions = new Dictionary<Tuple<string, int>, PythonFunction>();

        internal PythonModule(string moduleName)
        {
            _moduleName = moduleName;

            IntPtr module = IntPtr.Zero;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    module = PythonInterop.PyImport_AddModule(moduleName);

                    if (module == IntPtr.Zero)
                    {
                        throw new PythonException(PythonInterop.PyErr_Fetch());
                    }

                    PythonInterop.Py_IncRef(module);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot create module \"{0}\". Encountered Python error \"{1}\".", moduleName, ex.Message), ex);
            }

            _module = module;
            _dictionary = new PythonDictionary(this);
        }

        internal PythonModule(string modulePath, string moduleName)
        {
            _modulePath = modulePath;
            _moduleName = moduleName;

            IntPtr module = IntPtr.Zero;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var path = PythonInterop.PySys_GetObject("path");
                    var item = PythonInterop.PyString_FromString(Path.Combine(Environment.CurrentDirectory, modulePath));

                    PythonInterop.PyList_Append(path, item);

                    var name = PythonInterop.PyString_FromString(moduleName);
                    module = PythonInterop.PyImport_Import(name);

                    PythonInterop.Py_DecRef(item);
                    PythonInterop.Py_DecRef(name);

                    if (module == IntPtr.Zero)
                    {
                        throw new PythonException(PythonInterop.PyErr_Fetch());
                    }

                    PythonInterop.Py_IncRef(module);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Encountered error \"{0}\" while attempting to load module \"{1}\" from path \"{2}\".", ex.Message, moduleName, modulePath), ex);
            }

            _module = module;
            _dictionary = new PythonDictionary(this);
        }

        internal IntPtr NativePythonModule
        {
            get
            {
                return _module;
            }
        }

        public string FullName
        {
            get
            {
                if (_modulePath != null)
                {
                    return _modulePath + "/" + _moduleName;
                }

                return _moduleName;
            }
        }

        public IPythonDictionary Dictionary
        {
            get { return _dictionary; }
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

        public void Execute(string code)
        {
            Execute<object>(code, null);
        }

        public T Execute<T>(string code, string resultVariable)
        {
            var result = default(T);

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var userDictionary = new PythonDictionary();

                    try
                    {
                        //var cc = PythonInterop.Py_CompileString(expression, "asdf", PythonInterop.Py_file_input);
                        //if (cc == IntPtr.Zero || PythonInterop.PyErr_Occurred() != 0)
                        //{
                        //    throw new PythonException(PythonInterop.PyErr_Fetch());
                        //}

                        var pyResult = PythonInterop.PyRun_String(code, PythonInterop.Py_file_input, _dictionary.NativePythonDictionary, userDictionary.NativePythonDictionary);
                        //var pyValue = PythonInterop.PyEval_EvalCode(cc, _mainModuleDictionary, userDictionary);
                        if (pyResult == IntPtr.Zero || PythonInterop.PyErr_Occurred() != 0)
                        {
                            throw new PythonException(PythonInterop.PyErr_Fetch());
                        }

                        PythonInterop.Py_DecRef(pyResult);

                        if (resultVariable != null)
                        {
                            result = userDictionary.Get<T>(resultVariable);
                        }
                    }
                    finally
                    {
                        userDictionary.Dispose();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Encountered error \"{0}\" while attempting to execute code.", ex.Message), ex);
            }

            return result;
        }

        public void Execute(string code, IDictionary<string, object> variables)
        {
            Execute<object>(code, variables, null);
        }

        public T Execute<T>(string code, IDictionary<string, object> variables, string resultVariable)
        {
            var result = default(T);

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var userDictionary = new PythonDictionary();

                    try
                    {
                        if (variables != null)
                        {
                            foreach (var variable in variables)
                            {
                                userDictionary.Add(variable.Key, variable.Value);
                            }
                        }

                        var pyResult = PythonInterop.PyRun_String(code, PythonInterop.Py_file_input, _dictionary.NativePythonDictionary, userDictionary.NativePythonDictionary);
                        if (pyResult == IntPtr.Zero || PythonInterop.PyErr_Occurred() != 0)
                        {
                            throw new PythonException(PythonInterop.PyErr_Fetch());
                        }

                        PythonInterop.Py_DecRef(pyResult);

                        if (resultVariable != null)
                        {
                            result = userDictionary.Get<T>(resultVariable);
                        }
                    }
                    finally
                    {
                        userDictionary.Dispose();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Encountered error \"{0}\" while attempting to execute code.", ex.Message), ex);
            }

            return result;
        }

        public object[] Execute(string code, IDictionary<string, object> variables, IDictionary<string, Type> resultVariables)
        {
            object[] result = null;

            try
            {
                PythonInterop.PyGILState_Invoke(() =>
                {
                    var userDictionary = new PythonDictionary();

                    try
                    {
                        if (variables != null)
                        {
                            foreach (var variable in variables)
                            {
                                userDictionary.Add(variable.Key, variable.Value);
                            }
                        }

                        var pyResult = PythonInterop.PyRun_String(code, PythonInterop.Py_file_input, _dictionary.NativePythonDictionary, userDictionary.NativePythonDictionary);
                        if (pyResult == IntPtr.Zero || PythonInterop.PyErr_Occurred() != 0)
                        {
                            throw new PythonException(PythonInterop.PyErr_Fetch());
                        }

                        PythonInterop.Py_DecRef(pyResult);

                        if (resultVariables != null)
                        {
                            result = new object[resultVariables.Count];

                            var resultVariableCount = 0;
                            foreach (var resultVariable in resultVariables)
                            {
                                result[resultVariableCount++] = userDictionary.Get(resultVariable.Key, resultVariable.Value);
                            }
                        }
                    }
                    finally
                    {
                        userDictionary.Dispose();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Encountered error \"{0}\" while attempting to execute code.", ex.Message), ex);
            }

            return result;
        }

        public object[] Execute(string code, IDictionary<string, Type> resultVariables)
        {
            return Execute(code, null, resultVariables);
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
                foreach (var nameAndFunctionKeyValuePair in _registeredFunctions)
                {
                    nameAndFunctionKeyValuePair.Value.Dispose();
                }

                _dictionary.Dispose();
                PythonInterop.Py_DecRef(_module);
            }
        }

        private PythonFunction GetFunction(string functionName, int typeHash)
        {
            PythonFunction function;
            var functionHash = Tuple.Create(functionName, typeHash);

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

            foreach (var type in types)
            {
                hash = (hash * 31) + type.GetHashCode();
            }

            return hash;
        }
    }
}
