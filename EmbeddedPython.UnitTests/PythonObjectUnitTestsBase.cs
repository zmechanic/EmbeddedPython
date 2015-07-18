using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
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
    }
}
