using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    using System.Linq;

    public abstract class PythonObjectUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        private const string ModulePath = "Python/ObjectTest";
        private const string ModuleMain = "Main";

        [TestMethod]
        public void Hash_Property_Succeeds()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            var testTarget = module.Execute<IPythonObject>("o = MyClass()", "o");

            var hash = testTarget.Hash;

            Assert.AreNotEqual(-1, hash);
        }

        [TestMethod]
        public void ToString_NoParameters_Succeeds()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            var testTarget = module.Execute<IPythonObject>("o = MyClass()", "o");

            var str = testTarget.ToString();

            Assert.IsFalse(string.IsNullOrEmpty(str));
        }

        [TestMethod]
        public void Dir_Property_Succeeds()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            var testTarget = module.Execute<IPythonObject>("o = MyClass()", "o");

            var result = testTarget.Dir;

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Size_Property_Succeeds()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            var testTarget = module.Execute<IPythonObject>("o = 'test'", "o");

            var result = testTarget.Size;

            Assert.AreEqual(4, result);
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Size_PropertyNoSizeImplemented_ThrowsException()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            var testTarget = module.Execute<IPythonObject>("o = MyClass()", "o");

            // ReSharper disable once UnusedVariable
            var result = testTarget.Size;
        }

        [TestMethod]
        public void HasAttr_WithCorrectName_ReturnsTrue()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            var testTarget = module.Execute<IPythonObject>("o = MyClass()", "o");

            var result = testTarget.HasAttr("__class__");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasAttr_WithIncorrectName_ReturnsFalse()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            var testTarget = module.Execute<IPythonObject>("o = MyClass()", "o");

            var result = testTarget.HasAttr("__not_an_attribute__");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAttr_WithCorrectName_ReturnsExpectedValue()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            var testTarget = module.Execute<IPythonObject>("o = MyClass()", "o");

            var result = testTarget.GetAttr("__class__");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void GetAttr_WithIncorrectName_ThrowsException()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            var testTarget = module.Execute<IPythonObject>("o = MyClass()", "o");

            var result = testTarget.GetAttr("__not_an_attribute__");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SetAttr_WithCorrectName_SetsAttribute()
        {
            const string attributeValue = "test";
            const string attributeName = "new_attribute";

            var module = Python.ImportModule(ModulePath, ModuleMain);
            var testTarget = module.Execute<IPythonObject>("o = MyClass()", "o");

            testTarget.SetAttr(attributeName, attributeValue);

            var result = testTarget.GetAttr<string>(attributeName);

            Assert.AreEqual(attributeValue, result);
        }
    }
}
