using System;
using System.Diagnostics;

namespace EmbeddedPython.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var python3 = EmbeddedPython.v3.Python.Instance;

            //var version2 = python3.Execute<byte[]>("import io\nb=io.BytesIO(b'0123456789')\nc=b.getvalue()\n", "c");
            //var version2 = python3.Execute<byte[]>("import io\nb=bytes(b'0123456789')\n", "b");
            //var version2 = python3.Execute<byte[]>("b=a\n", new Dictionary<string, object>{{"a", new byte[]{1,2,3,4,5}}}, "b");

            //var tuple = python3.MainModule.Execute<Tuple<int, int>>("b=(1,2)\n", "b");

            var moduleMain = python3.ImportModule("Module1", "Main");
            var moduleScreen = python3.ImportModule("Module1", "Screen");

            var main = moduleMain.Execute<IPythonObject>("instance = Main()", "instance");
            var screen = moduleScreen.Execute<IPythonObject>("instance = Screen()", "instance");

            var pygameDisplay = screen.CallMethod<IPythonModule>("get_display");
            var funcGetDriver = pygameDisplay.GetFunction<string>("get_driver");
            var funcUpdate = pygameDisplay.GetFunction<int>("update");

            var pygameScreen = screen.CallMethod<IPythonObject>("get_screen");
            pygameScreen.CallMethod<byte[], int>("fill", new byte[] { 255, 0, 0 });
            funcUpdate();
/*
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < 1000; i++)
            {
                screen.CallMethod("clear1");
                screen.CallMethod("update");
                screen.CallMethod("clear0");
                screen.CallMethod("update");
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
*/
            Console.ReadLine();

            python3.Dispose();
        }
    }
}
