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
        public void Invoke_IncorrectNumberOfArgs_ThrowsException()
        {
            using (var testTarget = _module.GetFunction("func1"))
            {
                testTarget.Invoke<string, string, string>("a", "b");
            }
        }

        [TestMethod]
        public void Invoke_0Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetFunction("func0"))
            {
                var result = testTarget.Invoke<string>();

                Assert.AreEqual("TaDa", result);
            }
        }

        [TestMethod]
        public void Invoke_1Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetFunction("func1"))
            {
                var result = testTarget.Invoke<string, string>("a");

                Assert.AreEqual("a", result);
            }
        }

        [TestMethod]
        public void Invoke_2Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetFunction("func2"))
            {
                var result = testTarget.Invoke<string, string, string>("a", "b");

                Assert.AreEqual("ab", result);
            }
        }

        [TestMethod]
        public void Invoke_3Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetFunction("func3"))
            {
                var result = testTarget.Invoke<string, string, string, string>("a", "b", "c");

                Assert.AreEqual("abc", result);
            }
        }

        [TestMethod]
        public void Invoke_4Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetFunction("func4"))
            {
                var result = testTarget.Invoke<string, string, string, string, string>("a", "b", "c", "d");

                Assert.AreEqual("abcd", result);
            }
        }

        [TestMethod]
        public void Invoke_5Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetFunction("func5"))
            {
                var result = testTarget.Invoke<string, string, string, string, string, string>("a", "b", "c", "d", "e");

                Assert.AreEqual("abcde", result);
            }
        }

        [TestMethod]
        public void Invoke_6Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetFunction("func6"))
            {
                var result = testTarget.Invoke<string, string, string, string, string, string, string>("a", "b", "c", "d", "e", "f");

                Assert.AreEqual("abcdef", result);
            }
        }

        [TestMethod]
        public void Invoke_7Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetFunction("func7"))
            {
                var result = testTarget.Invoke<string, string, string, string, string, string, string, string>("a", "b", "c", "d", "e", "f", "g");

                Assert.AreEqual("abcdefg", result);
            }
        }

        [TestMethod]
        public void Invoke_8Arg_ReturnsCorrectName()
        {
            using (var testTarget = _module.GetFunction("func8"))
            {
                var result = testTarget.Invoke<string, string, string, string, string, string, string, string, string>("a", "b", "c", "d", "e", "f", "g", "h");

                Assert.AreEqual("abcdefgh", result);
            }
        }
    }
}