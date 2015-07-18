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

            //var tuple = python3.MainModule.Execute<Tuple<int, int>>("b=(1,2)\n", "b");

            var py3_ms = python3.ImportModule("Common", "Satellite");
            var py3_m1 = python3.ImportModule("Module1", "Main");
            var py3_m2 = python3.ImportModule("Module2", "Main");

            var o = py3_m1.Execute<IPythonObject>("b=MyClass()", "b");
            var s = o.ToString();
            var rrr = o.CallMethod<object>("doSomething");

            var py3_func = py3_m2.GetFunction<int, string>("bbb");
            var py3_func_result = py3_func(10);

            python3.Dispose();

            Console.ReadLine();
        }
    }
}
