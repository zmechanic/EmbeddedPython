using System;
using System.Collections.Generic;

namespace EmbeddedPython.Internal
{
    internal static class PythonConcrete
    {
        private static readonly Dictionary<string, PythonModule> Modules = new Dictionary<string, PythonModule>();

        private static PythonModule _mainModule;
        private static readonly PythonFactory _factory = new PythonFactory();

        internal static void Initialize()
        {
            PythonInterop.Py_Initialize();

            if (PythonInterop.Py_IsInitialized() != 1)
            {
                throw new PythonException("Python is not initialized.");
            }

            PythonInterop.PyEval_InitThreads();

            _mainModule = new PythonModule("__main__");

            var pyBuiltIn = PythonInterop.PyDict_GetItemString(((PythonDictionary)_mainModule.Dictionary).NativePythonDictionary, "__builtins__");

            PythonInterop.Py_IncRef(pyBuiltIn);

            PythonInterop.Py_None = PythonInterop.PyObject_GetAttrString(pyBuiltIn, "None");
            PythonInterop.Py_True = PythonInterop.PyObject_GetAttrString(pyBuiltIn, "True");
            PythonInterop.Py_False = PythonInterop.PyObject_GetAttrString(pyBuiltIn, "False");

            PythonInterop.PyType_Module = PythonInterop.PyObject_Type(pyBuiltIn);
            PythonInterop.PyType_BaseObject = PythonInterop.PyObject_GetAttrString(pyBuiltIn, "object");

            PythonInterop.Py_DecRef(pyBuiltIn);

            PythonInterop.PyType_Bool = PythonInterop.PyObject_Type(PythonInterop.Py_True);
            PythonInterop.PyType_None = PythonInterop.PyObject_Type(PythonInterop.Py_None);
            PythonInterop.PyType_Type = PythonInterop.PyObject_Type(PythonInterop.PyType_None);

            var op = PythonInterop.PyObject_GetAttrString(((PythonDictionary)_mainModule.Dictionary).NativePythonDictionary, "keys");
            PythonInterop.PyType_Method = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

#if PY_MAJOR_VERSION_3
            op = PythonInterop.PyBytes_FromString("string");
            PythonInterop.PyType_Bytes = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);
#endif

            op = PythonInterop.PyString_FromString("string");
            PythonInterop.PyType_String = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

            op = PythonInterop.PyUnicode_FromString("unicode");
            PythonInterop.PyType_Unicode = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

            op = PythonInterop.PyTuple_New(0);
            PythonInterop.PyType_Tuple = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

            op = PythonInterop.PyList_New(0);
            PythonInterop.PyType_List = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

            op = PythonInterop.PyDict_New();
            PythonInterop.PyType_Dict = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

#if PY_MAJOR_VERSION_2
            op = PythonInterop.PyInt_FromLong(0);
            PythonInterop.PyType_Int = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

            op = PythonInterop.PyLong_FromLongLong(0);
            PythonInterop.PyType_LongLong = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

            op = PythonInterop.PyLong_FromUnsignedLongLong(0);
            PythonInterop.PyType_UnsignedLongLong = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);
#endif

            op = PythonInterop.PyLong_FromLong(0);
            PythonInterop.PyType_Long = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

            op = PythonInterop.PyLong_FromUnsignedLong(0);
            PythonInterop.PyType_UnsignedLong = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

            op = PythonInterop.PyFloat_FromDouble(0);
            PythonInterop.PyType_Float = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

            op = _mainModule.Execute<IntPtr>(
                "class TempClass:" + "\n" +
                "    \"\"\"Temporary class to get Python class type\"\"\"" + "\n"
                + "tempClass = TempClass",
                "tempClass");
            PythonInterop.PyType_Class = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);

            op = _mainModule.Execute<IntPtr>(
                "class TempClass:" + "\n" +
                "    \"\"\"Temporary class to get Python class type\"\"\"" + "\n"
                + "tempClass = TempClass()",
                "tempClass");
            PythonInterop.PyType_Instance = PythonInterop.PyObject_Type(op);
            PythonInterop.Py_DecRef(op);
        }

        internal static void Deinitialize()
        {
            _mainModule.Dispose();
            _mainModule = null;

            PythonInterop.Py_DecRef(PythonInterop.Py_None);
            PythonInterop.Py_DecRef(PythonInterop.Py_True);
            PythonInterop.Py_DecRef(PythonInterop.Py_False);

            PythonInterop.Py_DecRef(PythonInterop.PyType_BaseObject);
            PythonInterop.Py_DecRef(PythonInterop.PyType_Module);
            PythonInterop.Py_DecRef(PythonInterop.PyType_Class);
            PythonInterop.Py_DecRef(PythonInterop.PyType_Instance);
            PythonInterop.Py_DecRef(PythonInterop.PyType_Method);
#if PY_MAJOR_VERSION_3
            PythonInterop.Py_DecRef(PythonInterop.PyType_Bytes);
#endif
#if PY_MAJOR_VERSION_2
            PythonInterop.Py_DecRef(PythonInterop.PyType_Int);
            PythonInterop.Py_DecRef(PythonInterop.PyType_LongLong);
            PythonInterop.Py_DecRef(PythonInterop.PyType_UnsignedLongLong);
#endif
            PythonInterop.Py_DecRef(PythonInterop.PyType_Unicode);
            PythonInterop.Py_DecRef(PythonInterop.PyType_String);
            PythonInterop.Py_DecRef(PythonInterop.PyType_Tuple);
            PythonInterop.Py_DecRef(PythonInterop.PyType_List);
            PythonInterop.Py_DecRef(PythonInterop.PyType_Dict);
            PythonInterop.Py_DecRef(PythonInterop.PyType_Long);
            PythonInterop.Py_DecRef(PythonInterop.PyType_UnsignedLong);
            PythonInterop.Py_DecRef(PythonInterop.PyType_Float);
            PythonInterop.Py_DecRef(PythonInterop.PyType_Bool);
            PythonInterop.Py_DecRef(PythonInterop.PyType_None);
            PythonInterop.Py_DecRef(PythonInterop.PyType_Type);

            foreach (var nameAndModuleKeyValuePair in Modules)
            {
                nameAndModuleKeyValuePair.Value.Dispose();
            }

            Modules.Clear();

            PythonInterop.Py_Finalize();

            if (PythonInterop.Py_IsInitialized() != 0)
            {
                throw new PythonException("Python is not de-initialized.");
            }
        }

        internal static IPythonModule MainModule
        {
            get { return _mainModule; }
        }

        internal static IPythonTypeFactory Factory
        {
            get { return _factory; }
        }

        internal static IPythonModule ImportModule(string modulePath, string moduleName)
        {
            PythonModule module;

            var moduleHash = modulePath + "/" + moduleName;

            if (!Modules.TryGetValue(moduleHash, out module))
            {
                module = new PythonModule(modulePath, moduleName);

                Modules.Add(moduleHash, module);
            }

            return module;
        }
    }
}