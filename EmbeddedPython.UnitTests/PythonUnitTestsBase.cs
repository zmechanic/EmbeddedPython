using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    public abstract class PythonUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        [TestMethod]
        public void Instance_NoParameters_Activates()
        {
            var p = Python;
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void ImportModule_ByFileName_LoadsModule()
        {
            Python.ImportModule("Python/ObjectTest", "Main");
        }

        [TestMethod]
        public void ImportModule_ByFileName_StressTest()
        {
            for (var i = 0; i < 10000; i++)
            {
                var testTarget = Python.ImportModule("Python/ObjectTest", "Main");
                testTarget.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void ImportModule_ByIncorrectPath_ThrowsException()
        {
            Python.ImportModule("Python/NotAPath", "AModule");
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void ImportModule_ByIncorrectFileName_ThrowsException()
        {
            Python.ImportModule("Python/PassThrough", "AModule");
        }
    }
}
