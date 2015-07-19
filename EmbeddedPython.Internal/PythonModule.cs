using System;
using System.Collections.Generic;
using System.IO;

namespace EmbeddedPython.Internal
{
    internal class PythonModule : PythonObject, IPythonModule
    {
        private readonly string _modulePath;
        private readonly string _moduleName;
        private readonly PythonDictionary _dictionary;

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
                        throw PythonInterop.PyErr_Fetch();
                    }

                    PythonInterop.Py_IncRef(module);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Cannot create module \"{0}\". Encountered Python error \"{1}\".", moduleName, ex.Message), ex);
            }

            NativePythonObject = module;
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
                    IntPtr pathListItem = IntPtr.Zero;
                    
                    if (!string.IsNullOrEmpty(modulePath))
                    {
                        var path = PythonInterop.PySys_GetObject("path");
                        pathListItem = PythonInterop.PyString_FromString(Path.Combine(Environment.CurrentDirectory, modulePath));

                        PythonInterop.PyList_Append(path, pathListItem);
                    }

                    var name = PythonInterop.PyString_FromString(moduleName);
                    module = PythonInterop.PyImport_Import(name);

                    if (pathListItem != IntPtr.Zero)
                    {
                        PythonInterop.Py_DecRef(pathListItem);
                    }

                    PythonInterop.Py_DecRef(name);

                    if (module == IntPtr.Zero)
                    {
                        throw PythonInterop.PyErr_Fetch();
                    }

                    PythonInterop.Py_IncRef(module);
                });
            }
            catch (Exception ex)
            {
                throw new PythonException(string.Format("Encountered error \"{0}\" while attempting to load module \"{1}\" from path \"{2}\".", ex.Message, moduleName, modulePath), ex);
            }

            NativePythonObject = module;
            _dictionary = new PythonDictionary(this);
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
                        //    throw PythonInterop.PyErr_Fetch();
                        //}

                        var pyResult = PythonInterop.PyRun_String(code, PythonInterop.Py_file_input, _dictionary.NativePythonObject, userDictionary.NativePythonObject);
                        //var pyValue = PythonInterop.PyEval_EvalCode(cc, _mainModuleDictionary, userDictionary);
                        if (pyResult == IntPtr.Zero || PythonInterop.PyErr_Occurred() != IntPtr.Zero)
                        {
                            throw PythonInterop.PyErr_Fetch();
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
                                userDictionary.Set(variable.Key, variable.Value);
                            }
                        }

                        var pyResult = PythonInterop.PyRun_String(code, PythonInterop.Py_file_input, _dictionary.NativePythonObject, userDictionary.NativePythonObject);
                        if (pyResult == IntPtr.Zero || PythonInterop.PyErr_Occurred() != IntPtr.Zero)
                        {
                            throw PythonInterop.PyErr_Fetch();
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
                                userDictionary.Set(variable.Key, variable.Value);
                            }
                        }

                        var pyResult = PythonInterop.PyRun_String(code, PythonInterop.Py_file_input, _dictionary.NativePythonObject, userDictionary.NativePythonObject);
                        if (pyResult == IntPtr.Zero || PythonInterop.PyErr_Occurred() != IntPtr.Zero)
                        {
                            throw PythonInterop.PyErr_Fetch();
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

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    _dictionary.Dispose();
                    PythonInterop.PyGILState_Invoke(() => PythonInterop.Py_DecRef(NativePythonObject));
                }

                base.Dispose(disposing);
            }
        }
    }
}
