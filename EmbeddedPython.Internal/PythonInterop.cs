using System;
using System.Runtime.InteropServices;

namespace EmbeddedPython.Internal
{
    internal static class PythonInterop
    {
        internal static IntPtr Py_None;
        internal static IntPtr Py_True;
        internal static IntPtr Py_False;

        internal static IntPtr PyType_BaseObject;
        internal static IntPtr PyType_Module;
        internal static IntPtr PyType_Class;
        internal static IntPtr PyType_Instance;
        internal static IntPtr PyType_Method;
#if PY_MAJOR_VERSION_3
        internal static IntPtr PyType_Bytes;
#endif
#if PY_MAJOR_VERSION_2
        internal static IntPtr PyType_Int;
        internal static IntPtr PyType_LongLong;
        internal static IntPtr PyType_UnsignedLongLong;
#endif
        internal static IntPtr PyType_Unicode;
        internal static IntPtr PyType_String;
        internal static IntPtr PyType_Tuple;
        internal static IntPtr PyType_List;
        internal static IntPtr PyType_Dict;
        internal static IntPtr PyType_Long;
        internal static IntPtr PyType_UnsignedLong;
        internal static IntPtr PyType_Float;
        internal static IntPtr PyType_Bool;
        internal static IntPtr PyType_None;
        internal static IntPtr PyType_Type;

        internal static IntPtr PyType_BaseException;
        internal static IntPtr PyType_SyntaxError;
        internal static IntPtr PyType_IndentationError;
        internal static IntPtr PyType_TabError;
        internal static IntPtr PyType_ImportError;

        internal const int Py_single_input = 256;
        internal const int Py_file_input = 257;
        internal const int Py_eval_input = 258;

#if PY_MAJOR_VERSION_2

    #if PY_VERSION_22
        internal const string PY_DLL = "python22";
    #elif PY_VERSION_23
        internal const string PY_DLL = "python23";
    #elif PY_VERSION_24
        internal const string PY_DLL = "python24";
    #elif PY_VERSION_25
        internal const string PY_DLL = "python25";
    #elif PY_VERSION_26
        internal const string PY_DLL = "python26";
    #elif PY_VERSION_27
        internal const string PY_DLL = "python27";
    #elif PY_VERSION_28
        internal const string PY_DLL = "python28";
    #elif PY_VERSION_29
        internal const string PY_DLL = "python29";
    #else
    #error Incorrect Python version defined
    #endif

#elif PY_MAJOR_VERSION_3

    #if PY_VERSION_32
        internal const string PY_DLL = "python32";
    #elif PY_VERSION_33
        internal const string PY_DLL = "python33";
    #elif PY_VERSION_34
        internal const string PY_DLL = "python34";
    #elif PY_VERSION_35
        internal const string PY_DLL = "python35";
    #elif PY_VERSION_36
        internal const string PY_DLL = "python36";
    #elif PY_VERSION_37
        internal const string PY_DLL = "python37";
    #elif PY_VERSION_38
        internal const string PY_DLL = "python38";
    #elif PY_VERSION_39
        internal const string PY_DLL = "python39";
    #else
    #error Incorrect Python version defined
    #endif

#else
#error Incorrect Python major version defined
#endif

#if PY_UCS2
        internal const string PY_UCS = "UCS2";
#elif PY_UCS4
        internal const string PY_UCS = "UCS4";
#else
        internal const string PY_UCS = "";
#endif

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        Py_IncRef(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        Py_DecRef(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        Py_Initialize();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        Py_IsInitialized();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        Py_Finalize();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        Py_NewInterpreter();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        Py_EndInterpreter(IntPtr threadState);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyThreadState_New(IntPtr istate);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyThreadState_Get();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyThread_get_key_value(IntPtr key);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyThread_get_thread_ident();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyThread_set_key_value(IntPtr key, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyThreadState_Swap(IntPtr key);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyGILState_Ensure();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyGILState_Release(IntPtr gs);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyGILState_GetThisThreadState();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        Py_Main(int argc, string[] argv);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyEval_InitThreads();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyEval_AcquireLock();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyEval_ReleaseLock();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyEval_AcquireThread(IntPtr tstate);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyEval_ReleaseThread(IntPtr tstate);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyEval_SaveThread();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyEval_RestoreThread(IntPtr tstate);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyEval_GetBuiltins();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyEval_GetGlobals();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyEval_GetLocals();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyEval_EvalCode(IntPtr code, IntPtr globals, IntPtr locals);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyEval_GetFrame();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyFrame_GetLineNumber(IntPtr frame);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern string
        Py_GetProgramName();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        Py_SetProgramName(string name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern string
        Py_GetPythonHome();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        Py_SetPythonHome(string home);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern string
        Py_GetVersion();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern string
        Py_GetPlatform();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern string
        Py_GetCopyright();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern string
        Py_GetCompiler();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern string
        Py_GetBuildInfo();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyRun_SimpleString(string code);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyRun_String(string code, int start, IntPtr globals, IntPtr locals);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        Py_CompileString(string code, string file, int start);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyImport_ExecCodeModule(string name, IntPtr code);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyCFunction_New(IntPtr ml, IntPtr self);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyCFunction_NewEx(IntPtr ml, IntPtr self, IntPtr mod);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyCFunction_Call(IntPtr func, IntPtr args, IntPtr kw);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyClass_New(IntPtr bases, IntPtr dict, IntPtr name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyInstance_New(IntPtr cls, IntPtr args, IntPtr kw);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyInstance_NewRaw(IntPtr cls, IntPtr dict);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyMethod_New(IntPtr func, IntPtr self, IntPtr cls);


        // ====================================================================
        // Python abstract object API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_Type(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_HasAttrString(IntPtr pointer, string name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_GetAttrString(IntPtr pointer, string name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_SetAttrString(IntPtr pointer, string name, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_DelAttrString(IntPtr pointer, IntPtr name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_HasAttr(IntPtr pointer, IntPtr name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_GetAttr(IntPtr pointer, IntPtr name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_SetAttr(IntPtr pointer, IntPtr name, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_DelAttr(IntPtr pointer, IntPtr name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_GetItem(IntPtr pointer, IntPtr key);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_SetItem(IntPtr pointer, IntPtr key, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_DelItem(IntPtr pointer, IntPtr key);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_GetIter(IntPtr op);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_Call(IntPtr pointer, IntPtr args, IntPtr kw);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallObject(IntPtr pointer, IntPtr args);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallFunctionObjArgs(IntPtr pointer, IntPtr arg1);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallFunctionObjArgs(IntPtr pointer, IntPtr arg1, IntPtr arg2);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallFunctionObjArgs(IntPtr pointer, IntPtr arg1, IntPtr arg2, IntPtr arg3);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallFunctionObjArgs(IntPtr pointer, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallFunctionObjArgs(IntPtr pointer, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallFunctionObjArgs(IntPtr pointer, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallFunctionObjArgs(IntPtr pointer, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6, IntPtr arg7);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallFunctionObjArgs(IntPtr pointer, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6, IntPtr arg7, IntPtr arg8);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallFunctionObjArgs(IntPtr pointer, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6, IntPtr arg7, IntPtr arg8, IntPtr arg9);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallMethodObjArgs(IntPtr pointer, IntPtr name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallMethodObjArgs(IntPtr pointer, IntPtr name, IntPtr arg1);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallMethodObjArgs(IntPtr pointer, IntPtr name, IntPtr arg1, IntPtr arg2);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallMethodObjArgs(IntPtr pointer, IntPtr name, IntPtr arg1, IntPtr arg2, IntPtr arg3);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallMethodObjArgs(IntPtr pointer, IntPtr name, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallMethodObjArgs(IntPtr pointer, IntPtr name, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallMethodObjArgs(IntPtr pointer, IntPtr name, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallMethodObjArgs(IntPtr pointer, IntPtr name, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6, IntPtr arg7);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallMethodObjArgs(IntPtr pointer, IntPtr name, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6, IntPtr arg7, IntPtr arg8);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_CallMethodObjArgs(IntPtr pointer, IntPtr name, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6, IntPtr arg7, IntPtr arg8, IntPtr arg9);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_Compare(IntPtr value1, IntPtr value2);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_IsInstance(IntPtr ob, IntPtr type);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_IsSubclass(IntPtr ob, IntPtr type);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyCallable_Check(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_IsTrue(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_Size(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_Length(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern long
        PyObject_Hash(IntPtr op);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_Repr(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_Str(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_Unicode(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_Dir(IntPtr pointer);

        // ====================================================================
        // Python number API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyNumber_Int(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyNumber_Long(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyNumber_Float(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern bool
        PyNumber_Check(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyInt_FromLong(long value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyInt_AsLong(IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyInt_FromString(string value, IntPtr end, int radix);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyInt_GetMax();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyLong_FromLong(long value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyLong_FromUnsignedLong(ulong value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyLong_FromDouble(double value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyLong_FromLongLong(long value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyLong_FromUnsignedLongLong(ulong value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyLong_FromString(string value, IntPtr end, int radix);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyLong_AsLong(IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern uint
        PyLong_AsUnsignedLong(IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern long
        PyLong_AsLongLong(IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern ulong
        PyLong_AsUnsignedLongLong(IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyFloat_FromDouble(double value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyFloat_FromString(IntPtr value, IntPtr junk);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern double
        PyFloat_AsDouble(IntPtr ob);


        // ====================================================================
        // Python sequence API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern bool
        PySequence_Check(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PySequence_GetItem(IntPtr pointer, int index);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PySequence_SetItem(IntPtr pointer, int index, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PySequence_DelItem(IntPtr pointer, int index);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PySequence_GetSlice(IntPtr pointer, int i1, int i2);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PySequence_SetSlice(IntPtr pointer, int i1, int i2, IntPtr v);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PySequence_DelSlice(IntPtr pointer, int i1, int i2);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PySequence_Size(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PySequence_Contains(IntPtr pointer, IntPtr item);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PySequence_Concat(IntPtr pointer, IntPtr other);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PySequence_Repeat(IntPtr pointer, int count);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PySequence_Index(IntPtr pointer, IntPtr item);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PySequence_Count(IntPtr pointer, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PySequence_Tuple(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PySequence_List(IntPtr pointer);


        // ====================================================================
        // Python string API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PyBytes_FromStringAndSize", ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyBytes_FromByteArray(byte[] bytes, int size);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PyBytes_FromStringAndSize", ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyBytes_FromByteArray(byte* bytes, int size);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyBytes_FromObject(IntPtr op);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyBytes_Size(IntPtr op);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PyBytes_AsString", ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern byte*
        PyBytes_AsByteArray(IntPtr op);

        // ====================================================================
        // Python string API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyString_FromStringAndSize(string value, int size);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyString_AsString(IntPtr op);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyString_Size(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PyUnicode" + PY_UCS + "_FromObject", ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyUnicode_FromObject(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PyUnicode" + PY_UCS + "_FromEncodedObject", ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyUnicode_FromEncodedObject(IntPtr ob, IntPtr enc, IntPtr err);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PyUnicode" + PY_UCS + "_DecodeUnicodeEscape", ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static extern unsafe IntPtr
        PyUnicode_DecodeUnicodeEscape(string s, int size, string e);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PyUnicode" + PY_UCS + "_FromUnicode", ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyUnicode_FromByteArray(byte[] bytes, int size);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PyUnicode" + PY_UCS + "_FromStringAndSize", ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyUnicode_FromStringAndSize(string s, int size);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PyUnicode" + PY_UCS + "_GetSize", ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyUnicode_GetSize(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PyUnicode" + PY_UCS + "_AsUnicode", ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern byte*
        PyUnicode_AsByteArray(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PyUnicode" + PY_UCS + "_FromOrdinal", ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyUnicode_FromOrdinal(int c);

        // ====================================================================
        // Python dictionary API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyDict_New();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyDictProxy_New(IntPtr dict);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyDict_GetItem(IntPtr pointer, IntPtr key);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyDict_GetItemString(IntPtr pointer, string key);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyDict_SetItem(IntPtr pointer, IntPtr key, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyDict_SetItemString(IntPtr pointer, string key, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyDict_DelItem(IntPtr pointer, IntPtr key);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyMapping_HasKey(IntPtr pointer, IntPtr key);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyDict_Keys(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyDict_Values(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyDict_Items(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyDict_Copy(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyDict_Update(IntPtr pointer, IntPtr other);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyDict_Clear(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyDict_Size(IntPtr pointer);


        // ====================================================================
        // Python list API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyList_New(int size);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyList_AsTuple(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyList_GetItem(IntPtr pointer, int index);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyList_SetItem(IntPtr pointer, int index, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyList_Insert(IntPtr pointer, int index, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyList_Append(IntPtr pointer, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyList_Reverse(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyList_Sort(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyList_GetSlice(IntPtr pointer, int start, int end);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyList_SetSlice(IntPtr pointer, int start, int end, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyList_Size(IntPtr pointer);


        // ====================================================================
        // Python tuple API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyTuple_New(int size);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyTuple_GetItem(IntPtr pointer, int index);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyTuple_SetItem(IntPtr pointer, int index, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyTuple_GetSlice(IntPtr pointer, int start, int end);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyTuple_Size(IntPtr pointer);


        // ====================================================================
        // Python iterator API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern bool
        PyIter_Check(IntPtr pointer);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyIter_Next(IntPtr pointer);

        // ====================================================================
        // Python module API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern string
        PyModule_GetName(IntPtr module);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyModule_GetDict(IntPtr module);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern string
        PyModule_GetFilename(IntPtr module);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyImport_Import(IntPtr name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyImport_ImportModule(string name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyImport_ReloadModule(IntPtr module);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyImport_AddModule(string name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyImport_GetModuleDict();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PySys_SetArgv(int argc, IntPtr argv);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PySys_GetObject(string name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PySys_SetObject(string name, IntPtr ob);


        // ====================================================================
        // Python type object API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern bool
        PyType_IsSubtype(IntPtr t1, IntPtr t2);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyType_GenericNew(IntPtr type, IntPtr args, IntPtr kw);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyType_GenericAlloc(IntPtr type, int n);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyType_Ready(IntPtr type);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        _PyType_Lookup(IntPtr type, IntPtr name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_GenericGetAttr(IntPtr obj, IntPtr name);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyObject_GenericSetAttr(IntPtr obj, IntPtr name, IntPtr value);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        _PyObject_GetDictPtr(IntPtr obj);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyObject_GC_New(IntPtr tp);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyObject_GC_Del(IntPtr tp);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyObject_GC_Track(IntPtr tp);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyObject_GC_UnTrack(IntPtr tp);


        // ====================================================================
        // Python memory API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyMem_Malloc(int size);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyMem_Realloc(IntPtr ptr, int size);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyMem_Free(IntPtr ptr);


        // ====================================================================
        // Python exception API
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyErr_SetString(IntPtr ob, string message);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyErr_SetObject(IntPtr ob, IntPtr message);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyErr_SetFromErrno(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyErr_SetNone(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyErr_ExceptionMatches(IntPtr exception);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern int
        PyErr_GivenExceptionMatches(IntPtr ob, IntPtr val);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyErr_NormalizeException(ref IntPtr ob, ref IntPtr val, ref IntPtr tb);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyErr_Occurred();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyErr_Fetch(ref IntPtr ob, ref IntPtr val, ref IntPtr tb);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyErr_Restore(IntPtr ob, IntPtr val, IntPtr tb);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyErr_Clear();

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern void
        PyErr_Print();


        // ====================================================================
        // Miscellaneous
        // ====================================================================
        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyMethod_Self(IntPtr ob);

        [DllImport(PY_DLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        internal static unsafe extern IntPtr
        PyMethod_Function(IntPtr ob);

        // ====================================================================
        // Helper functions
        // ====================================================================
        public static IntPtr PyBytes_FromString(string value)
        {
            var pythonBytes = PyBytes_FromByteArray(System.Text.Encoding.ASCII.GetBytes(value), value.Length);
            if (pythonBytes == IntPtr.Zero)
            {
                throw PyErr_Fetch();
            }

            return pythonBytes;
        }

        public static IntPtr PyBytes_FromByteArray(byte[] bytes)
        {
            var pythonBytes = PyBytes_FromByteArray(bytes, bytes.Length);

            if (pythonBytes == IntPtr.Zero)
            {
                throw PyErr_Fetch();
            }

            return pythonBytes;
        }

        public static IntPtr PyBytes_FromByteArray(byte[] bytes, int start, int length)
        {
            IntPtr pythonBytes;

            unsafe
            {
                fixed (byte* p = &bytes[start])
                {
                    pythonBytes = PyBytes_FromByteArray(p, length);
                }
            }

            if (pythonBytes == IntPtr.Zero)
            {
                throw PyErr_Fetch();
            }

            return pythonBytes;
        }

        public static IntPtr PyString_FromString(string value)
        {
#if PY_MAJOR_VERSION_2
            var pythonString = PyString_FromStringAndSize(value, value.Length);
            if (pythonString == IntPtr.Zero)
            {
                throw PyErr_Fetch();
            }

            return pythonString;
#elif PY_MAJOR_VERSION_3
            return PyUnicode_FromString(value);
#endif
        }

        public static string PyString_ToString(IntPtr value)
        {
#if PY_MAJOR_VERSION_2
            var pythonString = PyString_AsString(value);
            return pythonString != IntPtr.Zero ? Marshal.PtrToStringAnsi(pythonString) : null;
#elif PY_MAJOR_VERSION_3
            return PyUnicode_ToString(value);
#endif
        }

        public static IntPtr PyUnicode_FromString(string value)
        {
#if PY_UCS2 || PY_UCSNONE
            var pythonString = PyUnicode_FromByteArray(System.Text.Encoding.Unicode.GetBytes(value), value.Length);
#elif PY_UCS4
            var pythonString = PyUnicode_FromByteArray(System.Text.Encoding.UTF32.GetBytes(value), value.Length);
#endif
            if (pythonString == IntPtr.Zero)
            {
                throw PyErr_Fetch();
            }

            return pythonString;
        }

        public static string PyUnicode_ToString(IntPtr value)
        {
            var bytes = new byte[10000];
            var count = 0;

            unsafe
            {
                var pyPointer = PyUnicode_AsByteArray(value);
                if (pyPointer == null)
                {
                    throw PyErr_Fetch();
                }

#if PY_UCS2 || PY_UCSNONE
                for (int i = 0; i < 0xffff; i += 2)
                {
                    var b0 = *pyPointer++;
                    var b1 = *pyPointer++;

                    if (b0 == 0 && b1 == 0)
                    {
                        break;
                    }

                    bytes[i] = b0;
                    bytes[i + 1] = b1;
                    count += 2;
                }
#elif PY_UCS4
                for (int i = 0; i < 0xffff; i += 4)
                {
                    var b0 = *pyPointer++;
                    var b1 = *pyPointer++;
                    var b2 = *pyPointer++;
                    var b3 = *pyPointer++;

                    if (b0 == 0 && b1 == 0 && b2 == 0 && b3 == 0)
                    {
                        break;
                    }

                    bytes[i] = b0;
                    bytes[i + 1] = b1;
                    bytes[i + 2] = b2;
                    bytes[i + 3] = b3;
                    count += 4;
                }
#endif
            }

#if PY_UCS2 || PY_UCSNONE
            return System.Text.Encoding.Unicode.GetString(bytes, 0, count);
#elif PY_UCS4
            return System.Text.Encoding.UTF32.GetString(bytes, 0, count);
#endif
        }

        public static PythonException PyErr_Fetch()
        {
            var type = IntPtr.Zero;
            var value = IntPtr.Zero;
            var traceback = IntPtr.Zero;

            PyErr_Fetch(ref type, ref value, ref traceback);

            if (type != IntPtr.Zero)
            {
                var errorConverted = PythonTypeConverter.ConvertToClrType<object>(value);

                if (errorConverted is PythonException)
                {
                    return (PythonException)errorConverted;
                }

                if (errorConverted is string)
                {
                    return new PythonException((string)errorConverted);
                }
            }

            return null;
        }

        public static void PyGILState_Invoke(Action func)
        {
            var gstate = PyGILState_Ensure();

            try
            {
                func();
            }
            finally
            {
                PyGILState_Release(gstate);
            }
        }
    }
}