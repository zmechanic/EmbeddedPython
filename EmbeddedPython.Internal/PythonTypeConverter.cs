using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EmbeddedPython.Internal
{
    internal static class PythonTypeConverter
    {
        public static IntPtr ConvertToPythonType(object value)
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            if (value == null)
            {
                return PythonInterop.Py_None;
            }

            var t = value.GetType();

            if (t == typeof(IntPtr))
            {
                return (IntPtr)value;
            }

            if (t == typeof(PythonObject))
            {
                return ((PythonObject)value).NativePythonObject;
            }

            if (t == typeof(bool))
            {
                return (bool)value ? PythonInterop.Py_True : PythonInterop.Py_False;
            }

            if (t == typeof(byte[]))
            {
                return PythonInterop.PyBytes_FromByteArray((byte[])value);
            }

            if (t == typeof(IEnumerable<byte>))
            {
                return PythonInterop.PyBytes_FromByteArray(((IEnumerable<byte>)value).ToArray());
            }

            if (t == typeof(byte))
            {
#if PY_MAJOR_VERSION_2
                return PythonInterop.PyInt_FromLong((byte)value);
#elif PY_MAJOR_VERSION_3
                return PythonInterop.PyLong_FromLong((byte)value);
#endif
            }

            if (t == typeof(short))
            {
#if PY_MAJOR_VERSION_2
                return PythonInterop.PyInt_FromLong((short)value);
#elif PY_MAJOR_VERSION_3
                return PythonInterop.PyLong_FromLong((short)value);
#endif
            }

            if (t == typeof(ushort))
            {
#if PY_MAJOR_VERSION_2
                return PythonInterop.PyInt_FromLong((ushort)value);
#elif PY_MAJOR_VERSION_3
                return PythonInterop.PyLong_FromLong((ushort)value);
#endif
            }

            if (t == typeof(int))
            {
#if PY_MAJOR_VERSION_2
                return PythonInterop.PyInt_FromLong((int)value);
#elif PY_MAJOR_VERSION_3
                return PythonInterop.PyLong_FromLong((int)value);
#endif
            }

            if (t == typeof(uint))
            {
                return PythonInterop.PyLong_FromUnsignedLong((uint)value);
            }

            if (t == typeof(long))
            {
                return PythonInterop.PyLong_FromLongLong((long)value);
            }

            if (t == typeof(ulong))
            {
                return PythonInterop.PyLong_FromUnsignedLongLong((ulong)value);
            }

            if (t == typeof(float))
            {
                return PythonInterop.PyFloat_FromDouble((float)value);
            }

            if (t == typeof(double))
            {
                return PythonInterop.PyFloat_FromDouble((double)value);
            }

            if (t == typeof(string))
            {
                return PythonInterop.PyString_FromString((string)value);
            }

            if (t == typeof(PythonAsciiString))
            {
                return PythonInterop.PyString_FromString(value.ToString());
            }

            if (t == typeof(PythonUnicodeString))
            {
                return PythonInterop.PyUnicode_FromString(value.ToString());
            }

            if (t.IsGenericType)
            {
                var genericType = t.GetGenericTypeDefinition();
                var genericArgTypes = t.GetGenericArguments();
                var numberOfArgTypes = genericArgTypes.Length;

                if (genericType == typeof(Tuple<>) ||
                    genericType == typeof(Tuple<,>) ||
                    genericType == typeof(Tuple<,,>) ||
                    genericType == typeof(Tuple<,,,>) ||
                    genericType == typeof(Tuple<,,,,>) ||
                    genericType == typeof(Tuple<,,,,,>) ||
                    genericType == typeof(Tuple<,,,,,,>) ||
                    genericType == typeof(Tuple<,,,,,,,>))
                {
                    var pyTuple = PythonInterop.PyTuple_New(numberOfArgTypes);

                    for (var i = 0; i < Math.Min(7, numberOfArgTypes); i++)
                    {
                        var pi = t.GetProperty("Item" + (i + 1));
                        PythonInterop.PyTuple_SetItem(pyTuple, i, ConvertToPythonType(pi.GetValue(value)));
                    }

                    if (numberOfArgTypes == 8)
                    {
                        var pi = t.GetProperty("Rest");
                        PythonInterop.PyTuple_SetItem(pyTuple, 7, ConvertToPythonType(pi.GetValue(value)));
                    }

                    return pyTuple;
                }
            }

            throw new PythonException(string.Format("Unsupported CLR to Python conversion for source type {0}.", t));
        }

        public static object ConvertToClrType(IntPtr value, Type t)
        {
            if (value == IntPtr.Zero)
            {
                throw new PythonException("Value does not exist.");
            }

            if (t == typeof(IntPtr))
            {
                return value;
            }

            if (value == PythonInterop.Py_None)
            {
                return null;
            }

            if (t == typeof(bool))
            {
                if (value == PythonInterop.Py_True)
                {
                    return true;
                }

                if (value == PythonInterop.Py_False)
                {
                    return false;
                }
            }

            if (t == typeof(byte[]))
            {
                var length = PythonInterop.PyBytes_Size(value);
                if (length < 0)
                {
                    throw new PythonException(string.Format("Conversion to bytes array failed with error \"{0}\".", PythonInterop.PyErr_Fetch()));
                }

                var result = new byte[length];

                unsafe
                {
                    var pyPointer = PythonInterop.PyBytes_AsByteArray(value);

                    for (int i = 0; i < length; i++)
                    {
                        result[i] = *pyPointer;
                        pyPointer++;
                    }
                }

                return result;
            }

            if (t == typeof(byte))
            {
#if PY_MAJOR_VERSION_2
                return (byte)PythonInterop.PyInt_AsLong(value);
#elif PY_MAJOR_VERSION_3
                return (byte)PythonInterop.PyLong_AsLong(value);
#endif
            }

            if (t == typeof(short))
            {
#if PY_MAJOR_VERSION_2
                return (short)PythonInterop.PyInt_AsLong(value);
#elif PY_MAJOR_VERSION_3
                return (short)PythonInterop.PyLong_AsLong(value);
#endif
            }

            if (t == typeof(ushort))
            {
#if PY_MAJOR_VERSION_2
                return (ushort)PythonInterop.PyInt_AsLong(value);
#elif PY_MAJOR_VERSION_3
                return (ushort)PythonInterop.PyLong_AsLong(value);
#endif
            }

            if (t == typeof(int))
            {
#if PY_MAJOR_VERSION_2
                return PythonInterop.PyInt_AsLong(value);
#elif PY_MAJOR_VERSION_3
                return PythonInterop.PyLong_AsLong(value);
#endif
            }

            if (t == typeof(uint))
            {
                return PythonInterop.PyLong_AsUnsignedLong(value);
            }

            if (t == typeof(long))
            {
                return PythonInterop.PyLong_AsLongLong(value);
            }

            if (t == typeof(ulong))
            {
                return PythonInterop.PyLong_AsUnsignedLongLong(value);
            }

            if (t == typeof(float))
            {
                return (float)PythonInterop.PyFloat_AsDouble(value);
            }

            if (t == typeof(double))
            {
                return PythonInterop.PyFloat_AsDouble(value);
            }

            if (t == typeof(string))
            {
                return PythonInterop.PyString_ToString(value);
            }

            if (t == typeof(PythonAsciiString))
            {
                return PythonAsciiString.FromString(PythonInterop.PyString_ToString(value));
            }

            if (t == typeof(PythonUnicodeString))
            {
                return PythonUnicodeString.FromString(PythonInterop.PyUnicode_ToString(value));
            }

            if (t == typeof(IPythonDictionary))
            {
                return new PythonDictionary(value);
            }

            if (t == typeof(Tuple))
            {
                var pyTupleSize = PythonInterop.PyTuple_Size(value);
                if (pyTupleSize < 0)
                {
                    throw new PythonException(string.Format("Failed to obtain Python tuple size with error \"{0}\".", PythonInterop.PyErr_Fetch()));
                }

                var values = new object[pyTupleSize];
                for (var i = 0; i < pyTupleSize; i++)
                {
                    values[i] = ConvertToClrType(PythonInterop.PyTuple_GetItem(value, i), typeof(object));
                }

                switch (pyTupleSize)
                {
                    case 1:
                        t = typeof(Tuple<object>);
                        break;
                    case 2:
                        t = typeof(Tuple<object, object>);
                        break;
                    case 3:
                        t = typeof(Tuple<object, object, object>);
                        break;
                    case 4:
                        t = typeof(Tuple<object, object, object, object>);
                        break;
                    case 5:
                        t = typeof(Tuple<object, object, object, object, object>);
                        break;
                    case 6:
                        t = typeof(Tuple<object, object, object, object, object, object>);
                        break;
                    case 7:
                        t = typeof(Tuple<object, object, object, object, object, object, object>);
                        break;
                    case 8:
                        t = typeof(Tuple<object, object, object, object, object, object, object, object>);
                        break;
                }

                return Activator.CreateInstance(t, values);
            }

            if (t.IsGenericType)
            {
                var genericType = t.GetGenericTypeDefinition();
                var genericArgTypes = t.GetGenericArguments();
                var numberOfArgTypes = genericArgTypes.Length;

                if (genericType == typeof(Tuple<>) ||
                    genericType == typeof(Tuple<,>) ||
                    genericType == typeof(Tuple<,,>) ||
                    genericType == typeof(Tuple<,,,>) ||
                    genericType == typeof(Tuple<,,,,>) ||
                    genericType == typeof(Tuple<,,,,,>) ||
                    genericType == typeof(Tuple<,,,,,,>) ||
                    genericType == typeof(Tuple<,,,,,,,>))
                {
                    var pyTupleSize = PythonInterop.PyTuple_Size(value);
                    if (pyTupleSize < 0)
                    {
                        throw new PythonException(string.Format("Failed to obtain Python tuple size with error \"{0}\".", PythonInterop.PyErr_Fetch()));
                    }

                    if (pyTupleSize != numberOfArgTypes) throw new PythonException(string.Format("Expected tuple of size {0} got size {1}.", numberOfArgTypes, pyTupleSize));

                    var values = new object[pyTupleSize];
                    for (var i = 0; i < pyTupleSize; i++)
                    {
                        values[i] = ConvertToClrType(PythonInterop.PyTuple_GetItem(value, i), genericArgTypes[i]);
                    }

                    return Activator.CreateInstance(t, values);
                }

                if (genericType == typeof(IEnumerable<>) ||
                    genericType == typeof(IReadOnlyList<>) ||
                    genericType == typeof(IReadOnlyCollection<>))
                {
                    return PythonListToClrArray(value, genericArgTypes[0]);
                }
            }

            if (t == typeof(IEnumerable))
            {
                return PythonListToClrArray(value, typeof(object));
            }

            if (t.IsArray && t.GetArrayRank() == 1)
            {
                return PythonListToClrArray(value, t.GetElementType());
            }

            if (t == typeof(object))
            {
                return ConvertToClrType(value, GetClrType(value));
            }

            throw new PythonException(string.Format("Unsupported Python to CLR conversion for target type {0}.", t));
        }

        public static T ConvertToClrType<T>(IntPtr value)
        {
            var t = typeof(T);

            var obj = ConvertToClrType(value, t);
            if (obj is T)
            {
                return (T)obj;
            }

            return (T)Convert.ChangeType(ConvertToClrType(value, t), t);
        }

        public static Type GetClrType(IntPtr value)
        {
            if (value == IntPtr.Zero)
            {
                throw new PythonException("Value does not exist.");
            }

            if (value == PythonInterop.Py_None)
            {
                return typeof(object);
            }

            var pyType = PythonInterop.PyObject_Type(value);

            try
            {
                if (pyType == PythonInterop.PyType_Bool)
                {
                    return typeof(bool);
                }
                else if (pyType == PythonInterop.PyType_String)
                {
                    return typeof(string);
                }
                else if (pyType == PythonInterop.PyType_Unicode)
                {
                    return typeof(string);
                }
#if PY_MAJOR_VERSION_3
                else if (pyType == PythonInterop.PyType_Bytes)
                {
                    return typeof(byte[]);
                }
#endif
#if PY_MAJOR_VERSION_2
                else if (pyType == PythonInterop.PyType_Int)
                {
                    return typeof(int);
                }
                else if (pyType == PythonInterop.PyType_LongLong)
                {
                    return typeof(long);
                }
                else if (pyType == PythonInterop.PyType_UnsignedLongLong)
                {
                    return typeof(ulong);
                }
#endif
                else if (pyType == PythonInterop.PyType_Long)
                {
                    return typeof(int);
                }
                else if (pyType == PythonInterop.PyType_UnsignedLong)
                {
                    return typeof(uint);
                }
                else if (pyType == PythonInterop.PyType_Float)
                {
                    return typeof(double);
                }
                else if (pyType == PythonInterop.PyType_Tuple)
                {
                    return typeof(IPythonTuple);
                }
                else if (pyType == PythonInterop.PyType_Dict)
                {
                    return typeof(IPythonDictionary);
                }
                else if (pyType == PythonInterop.PyType_List)
                {
                    return typeof(IPythonList);
                }

                throw new PythonException("Unsupported Python type.");
            }
            finally
            {
                PythonInterop.Py_DecRef(pyType);
            }
        }

        private static Array PythonListToClrArray(IntPtr pyList, Type elementType)
        {
            var numberOfItems = PythonInterop.PyList_Size(pyList);

            var array = (object[])Activator.CreateInstance(elementType.MakeArrayType(), numberOfItems);

            for (var i = 0; i < numberOfItems; i++)
            {
                array[i] = ConvertToClrType(PythonInterop.PyList_GetItem(pyList, i), elementType);
            }

            return array;
        }
    }
}