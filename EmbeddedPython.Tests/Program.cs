using System;
using System.Collections.Generic;

namespace EmbeddedPython.Tests
{
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            var python3 = EmbeddedPython.v3.Python.Instance;

            //var version2 = python3.Execute<byte[]>("import io\nb=io.BytesIO(b'0123456789')\nc=b.getvalue()\n", "c");
            //var version2 = python3.Execute<byte[]>("import io\nb=bytes(b'0123456789')\n", "b");
            //var version2 = python3.Execute<byte[]>("b=a\n", new Dictionary<string, object>{{"a", new byte[]{1,2,3,4,5}}}, "b");
            //Console.WriteLine(version2.Length);

            Console.WriteLine();
            var version2 = python3.MainModule.Execute<Tuple<int, int>>("b=(1,2)\n", "b");

            var py3_ms = python3.ImportModule("Common", "Satellite");
            var py3_m1 = python3.ImportModule("Module1", "Main");
            var py3_m2 = python3.ImportModule("Module2", "Main");

            var py3_func = py3_m2.GetFunction<int, string>("bbb");
            var py3_func_result = py3_func(10);

            TestPassThrough<string>(null, py3_m1);
            TestPassThrough(string.Empty, py3_m1);
            TestPassThrough("Test String", py3_m1);
            TestPassThrough(PythonUnicodeString.Empty, py3_m1);
            TestPassThrough(PythonUnicodeString.FromString("«κόσμε»"), py3_m1);
            TestPassThrough(byte.MinValue, py3_m1);
            TestPassThrough(byte.MaxValue, py3_m1);
            TestPassThrough(short.MinValue, py3_m1);
            TestPassThrough(short.MaxValue, py3_m1);
            TestPassThrough(ushort.MinValue, py3_m1);
            TestPassThrough(ushort.MaxValue, py3_m1);
            TestPassThrough(int.MinValue, py3_m1);
            TestPassThrough(int.MaxValue, py3_m1);
            TestPassThrough(uint.MinValue, py3_m1);
            TestPassThrough(uint.MaxValue, py3_m1);
            TestPassThrough(long.MinValue, py3_m1);
            TestPassThrough(long.MaxValue, py3_m1);
            TestPassThrough(ulong.MinValue, py3_m1);
            TestPassThrough(ulong.MaxValue, py3_m1);

            python3.Dispose();

            Console.ReadLine();
        }

        static void TestPassThrough<T>(T value, IPythonModule module)
        {
            var func = module.GetFunction<T, T>("pass_value_through");

            T result = func(value);

            if (!EqualityComparer<T>.Default.Equals(result, value))
            {
                throw new Exception();
            }
        }
    }
}
