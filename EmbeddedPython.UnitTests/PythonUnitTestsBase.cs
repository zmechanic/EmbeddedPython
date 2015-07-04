using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    public abstract class PythonUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        [TestMethod]
        public void Instance_NoParameters_Activates()
        {
            var p = Python;
            p.MainModule.Dictionary.Add("global_var", 1);
        }

        [TestMethod]
        public void ImportModule_ByFileName_LoadsModule()
        {
            Python.ImportModule("Python/PassThrough", "Main");
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void ImportModule_ByIncorrectFileName_ThrowsException()
        {
            Python.ImportModule("Python/PassThrough", "AModule");
        }
    }
}
