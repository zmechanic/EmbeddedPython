using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    public class PythonFunctionUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        private const string ModulePath = "Python/FunctionInvokeTest";
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
        public void Invoke_IncorrectNumberOfArgsVoid_ThrowsException()
        {
            using (var testTarget = _module.GetPythonFunction("func1"))
            {
                testTarget.Invoke("a", "b");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Invoke_IncorrectNumberOfArgs_ThrowsException()
        {
            using (var testTarget = _module.GetPythonFunction("func1"))
            {
                testTarget.Invoke<string, string, string>("a", "b");
            }
        }

        [TestMethod]
        public void Invoke_0ArgVoid_Succeeds()
        {
            using (var testTarget = _module.GetPythonFunction("func0"))
            {
                testTarget.Invoke();
            }
        }

        [TestMethod]
        public void Invoke_1ArgVoid_Succeeds()
        {
            using (var testTarget = _module.GetPythonFunction("func1"))
            {
                testTarget.Invoke("a");
            }
        }

        [TestMethod]
        public void Invoke_2ArgVoid_Succeeds()
        {
            using (var testTarget = _module.GetPythonFunction("func2"))
            {
                testTarget.Invoke("a", "b");
            }
        }

        [TestMethod]
        public void Invoke_3ArgVoid_Succeeds()
        {
            using (var testTarget = _module.GetPythonFunction("func3"))
            {
                testTarget.Invoke("a", "b", "c");
            }
        }

        [TestMethod]
        public void Invoke_4ArgVoid_Succeeds()
        {
            using (var testTarget = _module.GetPythonFunction("func4"))
            {
                testTarget.Invoke("a", "b", "c", "d");
            }
        }

        [TestMethod]
        public void Invoke_5ArgVoid_Succeeds()
        {
            using (var testTarget = _module.GetPythonFunction("func5"))
            {
                testTarget.Invoke("a", "b", "c", "d", "e");
            }
        }

        [TestMethod]
        public void Invoke_6ArgVoid_Succeeds()
        {
            using (var testTarget = _module.GetPythonFunction("func6"))
            {
                testTarget.Invoke("a", "b", "c", "d", "e", "f");
            }
        }

        [TestMethod]
        public void Invoke_7ArgVoid_Succeeds()
        {
            using (var testTarget = _module.GetPythonFunction("func7"))
            {
                testTarget.Invoke("a", "b", "c", "d", "e", "f", "g");
            }
        }

        [TestMethod]
        public void Invoke_8ArgVoid_Succeeds()
        {
            using (var testTarget = _module.GetPythonFunction("func8"))
            {
                testTarget.Invoke("a", "b", "c", "d", "e", "f", "g", "h");
            }
        }

        [TestMethod]
        public void Invoke_0Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetPythonFunction("func0"))
            {
                var result = testTarget.Invoke<string>();

                Assert.AreEqual("TaDa", result);
            }
        }

        [TestMethod]
        public void Invoke_1Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetPythonFunction("func1"))
            {
                var result = testTarget.Invoke<string, string>("a");

                Assert.AreEqual("a", result);
            }
        }

        [TestMethod]
        public void Invoke_2Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetPythonFunction("func2"))
            {
                var result = testTarget.Invoke<string, string, string>("a", "b");

                Assert.AreEqual("ab", result);
            }
        }

        [TestMethod]
        public void Invoke_3Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetPythonFunction("func3"))
            {
                var result = testTarget.Invoke<string, string, string, string>("a", "b", "c");

                Assert.AreEqual("abc", result);
            }
        }

        [TestMethod]
        public void Invoke_4Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetPythonFunction("func4"))
            {
                var result = testTarget.Invoke<string, string, string, string, string>("a", "b", "c", "d");

                Assert.AreEqual("abcd", result);
            }
        }

        [TestMethod]
        public void Invoke_5Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetPythonFunction("func5"))
            {
                var result = testTarget.Invoke<string, string, string, string, string, string>("a", "b", "c", "d", "e");

                Assert.AreEqual("abcde", result);
            }
        }

        [TestMethod]
        public void Invoke_6Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetPythonFunction("func6"))
            {
                var result = testTarget.Invoke<string, string, string, string, string, string, string>("a", "b", "c", "d", "e", "f");

                Assert.AreEqual("abcdef", result);
            }
        }

        [TestMethod]
        public void Invoke_7Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetPythonFunction("func7"))
            {
                var result = testTarget.Invoke<string, string, string, string, string, string, string, string>("a", "b", "c", "d", "e", "f", "g");

                Assert.AreEqual("abcdefg", result);
            }
        }

        [TestMethod]
        public void Invoke_8Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetPythonFunction("func8"))
            {
                var result = testTarget.Invoke<string, string, string, string, string, string, string, string, string>("a", "b", "c", "d", "e", "f", "g", "h");

                Assert.AreEqual("abcdefgh", result);
            }
        }

        [TestMethod]
        public void Invoke_WithDispose_StressTest()
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

                using (var testTarget = _module.GetPythonFunction("func8"))
                {
                    var result =
                        testTarget.Invoke<string, string, string, string, string, string, string, string, string>(
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
        public void Invoke_WithoutDispose_StressTest()
        {
            var random = new Random(555);

            using (var testTarget = _module.GetPythonFunction("func8"))
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
                        testTarget.Invoke<string, string, string, string, string, string, string, string, string>(
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