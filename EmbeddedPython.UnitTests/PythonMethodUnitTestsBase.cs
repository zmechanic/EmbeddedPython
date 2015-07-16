using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    public class PythonMethodUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        private const string ModulePath = "Python/MethodInvokeTest";
        private const string ModuleMain = "Main";

        private IPythonModule _module;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            _module = Python.ImportModule(ModulePath, ModuleMain);
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            _module.Dispose();

            base.TestCleanup();
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void CallMethod_IncorrectNumberOfArgs_ThrowsException()
        {
            using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
            {
                testTarget.CallMethod<string, string, string>("method1", "a", "b");
            }
        }

        [TestMethod]
        public void CallMethod_0Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
            {
                var result = testTarget.CallMethod<string>("method0");

                Assert.AreEqual("TaDa", result);
            }
        }

        [TestMethod]
        public void CallMethod_1Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
            {
                var result = testTarget.CallMethod<string, string>("method1", "a");

                Assert.AreEqual("a", result);
            }
        }

        [TestMethod]
        public void CallMethod_2Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
            {
                var result = testTarget.CallMethod<string, string, string>("method2", "a", "b");

                Assert.AreEqual("ab", result);
            }
        }

        [TestMethod]
        public void CallMethod_3Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
            {
                var result = testTarget.CallMethod<string, string, string, string>("method3", "a", "b", "c");

                Assert.AreEqual("abc", result);
            }
        }

        [TestMethod]
        public void CallMethod_4Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
            {
                var result = testTarget.CallMethod<string, string, string, string, string>("method4", "a", "b", "c", "d");

                Assert.AreEqual("abcd", result);
            }
        }

        [TestMethod]
        public void CallMethod_5Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
            {
                var result = testTarget.CallMethod<string, string, string, string, string, string>("method5", "a", "b", "c", "d", "e");

                Assert.AreEqual("abcde", result);
            }
        }

        [TestMethod]
        public void CallMethod_6Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
            {
                var result = testTarget.CallMethod<string, string, string, string, string, string, string>("method6", "a", "b", "c", "d", "e", "f");

                Assert.AreEqual("abcdef", result);
            }
        }

        [TestMethod]
        public void CallMethod_7Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
            {
                var result = testTarget.CallMethod<string, string, string, string, string, string, string, string>("method7", "a", "b", "c", "d", "e", "f", "g");

                Assert.AreEqual("abcdefg", result);
            }
        }

        [TestMethod]
        public void CallMethod_8Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
            {
                var result = testTarget.CallMethod<string, string, string, string, string, string, string, string, string>("method8", "a", "b", "c", "d", "e", "f", "g", "h");

                Assert.AreEqual("abcdefgh", result);
            }
        }

        [TestMethod]
        public void CallMethod_WithDispose_StressTest()
        {
            var random = new Random(555);

            for (var i = 0; i < 10000; i++)
            {
                var v1 = ((char)random.Next(32, 127)).ToString();
                var v2 = ((char)random.Next(32, 127)).ToString();
                var v3 = ((char)random.Next(32, 127)).ToString();
                var v4 = ((char)random.Next(32, 127)).ToString();
                var v5 = ((char)random.Next(32, 127)).ToString();
                var v6 = ((char)random.Next(32, 127)).ToString();
                var v7 = ((char)random.Next(32, 127)).ToString();
                var v8 = ((char)random.Next(32, 127)).ToString();

                using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
                {
                    var result =
                        testTarget.CallMethod<string, string, string, string, string, string, string, string, string>(
                            "method8",
                            v1,
                            v2,
                            v3,
                            v4,
                            v5,
                            v6,
                            v7,
                            v8);

                    Assert.AreEqual(v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8, result);
                }
            }
        }

        [TestMethod]
        public void CallMethod_WithoutDispose_StressTest()
        {
            var random = new Random(555);

            using (var testTarget = _module.Execute<IPythonObject>("c=MyClass()", "c"))
            {
                for (var i = 0; i < 10000; i++)
                {
                    var v1 = ((char)random.Next(32, 127)).ToString();
                    var v2 = ((char)random.Next(32, 127)).ToString();
                    var v3 = ((char)random.Next(32, 127)).ToString();
                    var v4 = ((char)random.Next(32, 127)).ToString();
                    var v5 = ((char)random.Next(32, 127)).ToString();
                    var v6 = ((char)random.Next(32, 127)).ToString();
                    var v7 = ((char)random.Next(32, 127)).ToString();
                    var v8 = ((char)random.Next(32, 127)).ToString();

                    var result =
                        testTarget.CallMethod<string, string, string, string, string, string, string, string, string>(
                            "method8",
                            v1,
                            v2,
                            v3,
                            v4,
                            v5,
                            v6,
                            v7,
                            v8);

                    Assert.AreEqual(v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8, result);
                }
            }
        }
    }
}