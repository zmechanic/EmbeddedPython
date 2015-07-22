using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    using System.Linq;

    public abstract class PythonObjectUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        private const string ModulePath = "Python/ObjectTest";
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
        public void Hash_Property_Succeeds()
        {
            using (var testTarget = _module.Execute<IPythonObject>("o = MyClass()", "o"))
            {
                var hash = testTarget.Hash;

                Assert.AreNotEqual(-1, hash);
            }
        }

        [TestMethod]
        public void ToString_NoParameters_Succeeds()
        {
            using (var testTarget = _module.Execute<IPythonObject>("o = MyClass()", "o"))
            {
                var str = testTarget.ToString();

                Assert.IsFalse(string.IsNullOrEmpty(str));
            }
        }

        [TestMethod]
        public void Dir_Property_Succeeds()
        {
            using (var testTarget = _module.Execute<IPythonObject>("o = MyClass()", "o"))
            {
                var result = testTarget.Dir;

                Assert.IsTrue(result.Any());
            }
        }

        [TestMethod]
        public void Size_Property_Succeeds()
        {
            using (var testTarget = _module.Execute<IPythonObject>("o = 'test'", "o"))
            {
                var result = testTarget.Size;

                Assert.AreEqual(4, result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Size_PropertyNoSizeImplemented_ThrowsException()
        {
            using (var testTarget = _module.Execute<IPythonObject>("o = MyClass()", "o"))
            {
                // ReSharper disable once UnusedVariable
                var result = testTarget.Size;
            }
        }

        [TestMethod]
        public void HasAttr_WithCorrectName_ReturnsTrue()
        {
            using (var testTarget = _module.Execute<IPythonObject>("o = MyClass()", "o"))
            {
                var result = testTarget.HasAttr("__class__");

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void HasAttr_WithIncorrectName_ReturnsFalse()
        {
            using (var testTarget = _module.Execute<IPythonObject>("o = MyClass()", "o"))
            {
                var result = testTarget.HasAttr("__not_an_attribute__");

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void GetAttr_WithCorrectName_ReturnsExpectedValue()
        {
            using (var testTarget = _module.Execute<IPythonObject>("o = MyClass()", "o"))
            {
                var result = testTarget.GetAttr("__class__");

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void GetAttr_WithIncorrectName_ThrowsException()
        {
            using (var testTarget = _module.Execute<IPythonObject>("o = MyClass()", "o"))
            {
                var result = testTarget.GetAttr("__not_an_attribute__");

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void SetAttr_WithCorrectName_SetsAttribute()
        {
            const string attributeValue = "test";
            const string attributeName = "new_attribute";

            using (var testTarget = _module.Execute<IPythonObject>("o = MyClass()", "o"))
            {
                testTarget.SetAttr(attributeName, attributeValue);

                var result = testTarget.GetAttr<string>(attributeName);

                Assert.AreEqual(attributeValue, result);
            }
        }

        [TestMethod]
        public void GetPythonFunction_WithCorrectName_ReturnsFunction()
        {
            using (var result = _module.GetPythonFunction("func0"))
            {
                Assert.IsInstanceOfType(result, typeof(IPythonFunction));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void GetPythonFunction_WithIncorrectName_ThrowsException()
        {
            // ReSharper disable once UnusedVariable
            var result = _module.GetPythonFunction("not_a_function");
        }

        [TestMethod]
        public void Invoke_0ArgVoid_Succeeds()
        {
            var testTarget = _module.GetVoidFunction("func0");
            testTarget();
        }

        [TestMethod]
        public void Invoke_1ArgVoid_Succeeds()
        {
            var testTarget = _module.GetVoidFunction<string>("func1");
            testTarget("a");
        }

        [TestMethod]
        public void Invoke_2ArgVoid_Succeeds()
        {
            var testTarget = _module.GetVoidFunction<string, string>("func2");
            testTarget("a", "b");
        }

        [TestMethod]
        public void Invoke_3ArgVoid_Succeeds()
        {
            var testTarget = _module.GetVoidFunction<string, string, string>("func3");
            testTarget("a", "b", "c");
        }

        [TestMethod]
        public void Invoke_4ArgVoid_Succeeds()
        {
            var testTarget = _module.GetVoidFunction<string, string, string, string>("func4");
            testTarget("a", "b", "c", "d");
        }

        [TestMethod]
        public void Invoke_5ArgVoid_Succeeds()
        {
            var testTarget = _module.GetVoidFunction<string, string, string, string, string>("func5");
            testTarget("a", "b", "c", "d", "e");
        }

        [TestMethod]
        public void Invoke_6ArgVoid_Succeeds()
        {
            var testTarget = _module.GetVoidFunction<string, string, string, string, string, string>("func6");
            testTarget("a", "b", "c", "d", "e", "f");
        }

        [TestMethod]
        public void Invoke_7ArgVoid_Succeeds()
        {
            var testTarget = _module.GetVoidFunction<string, string, string, string, string, string, string>("func7");
            testTarget("a", "b", "c", "d", "e", "f", "g");
        }

        [TestMethod]
        public void Invoke_8ArgVoid_Succeeds()
        {
            var testTarget = _module.GetVoidFunction<string, string, string, string, string, string, string, string>("func8");
            testTarget("a", "b", "c", "d", "e", "f", "g", "h");
        }

        [TestMethod]
        public void Invoke_0Arg_ReturnsCorrectName()
        {
            var testTarget = _module.GetFunction<string>("func0");
            var result = testTarget();

            Assert.AreEqual("TaDa", result);
        }

        [TestMethod]
        public void Invoke_1Arg_ReturnsCorrectName()
        {
            var testTarget = _module.GetFunction<string, string>("func1");
            var result = testTarget("a");

            Assert.AreEqual("a", result);
        }

        [TestMethod]
        public void Invoke_2Arg_ReturnsCorrectName()
        {
            var testTarget = _module.GetFunction<string, string, string>("func2");
            var result = testTarget("a", "b");

            Assert.AreEqual("ab", result);
        }

        [TestMethod]
        public void Invoke_3Arg_ReturnsCorrectName()
        {
            var testTarget = _module.GetFunction<string, string, string, string>("func3");
            var result = testTarget("a", "b", "c");

            Assert.AreEqual("abc", result);
        }

        [TestMethod]
        public void Invoke_4Arg_ReturnsCorrectName()
        {
            var testTarget = _module.GetFunction<string, string, string, string, string>("func4");
            var result = testTarget("a", "b", "c", "d");

            Assert.AreEqual("abcd", result);
        }

        [TestMethod]
        public void Invoke_5Arg_ReturnsCorrectName()
        {
            var testTarget = _module.GetFunction<string, string, string, string, string, string>("func5");
            var result = testTarget("a", "b", "c", "d", "e");

            Assert.AreEqual("abcde", result);
        }

        [TestMethod]
        public void Invoke_6Arg_ReturnsCorrectName()
        {
            var testTarget = _module.GetFunction<string, string, string, string, string, string, string>("func6");
            var result = testTarget("a", "b", "c", "d", "e", "f");

            Assert.AreEqual("abcdef", result);
        }

        [TestMethod]
        public void Invoke_7Arg_ReturnsCorrectName()
        {
            var testTarget = _module.GetFunction<string, string, string, string, string, string, string, string>("func7");
            var result = testTarget("a", "b", "c", "d", "e", "f", "g");

            Assert.AreEqual("abcdefg", result);
        }

        [TestMethod]
        public void Invoke_8Arg_ReturnsCorrectName()
        {
            var testTarget = _module.GetFunction<string, string, string, string, string, string, string, string, string>("func8");
            var result = testTarget("a", "b", "c", "d", "e", "f", "g", "h");

            Assert.AreEqual("abcdefgh", result);
        }
    }
}
