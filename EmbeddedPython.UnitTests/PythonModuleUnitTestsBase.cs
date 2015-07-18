using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    public abstract class PythonModuleUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        private const string ModulePath = "Python/ObjectTest";
        private const string ModuleMain = "Main";

        [TestMethod]
        public void FullName_Get_ReturnsCorrectName()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            Assert.AreEqual(ModulePath + "/" + ModuleMain, module.FullName);
        }

        [TestMethod]
        public void GetFunction_ByName_ReturnsFunction()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            var func = module.GetFunction<int, int>("pass_value_through");
            Assert.IsNotNull(func);
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void GetFunction_ByIncorrectName_ThrowsException()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            module.GetFunction<int, int>("not_a_function");
        }

        [TestMethod]
        public void GetFunction_ByName_ReturnsSameFunction()
        {
            var module = Python.ImportModule(ModulePath, ModuleMain);
            var func1 = module.GetFunction<int, int>("pass_value_through");
            var func2 = module.GetFunction<int, int>("pass_value_through");
            Assert.AreSame(func1.Method, func2.Method);
        }

        [TestMethod]
        public void Execute_FromStringWithVoidReturn_Succeeds()
        {
            var expectedResult1 = "Test: " + Guid.NewGuid();
            var expectedResult2 = "Test: " + Guid.NewGuid();

            Python.MainModule.Execute("import sys\nresult = '" + expectedResult1 + "'");
            Python.MainModule.Execute("import sys\nresult = '" + expectedResult2 + "'");
        }

        [TestMethod]
        public void Execute_FromStringWithSingleReturnsParameter_ReturnsExpectedValue()
        {
            var expectedResult1 = "Test: " + Guid.NewGuid();
            var expectedResult2 = "Test: " + Guid.NewGuid();

            var result1 = Python.MainModule.Execute<string>("import sys\nresult = '" + expectedResult1 + "'", "result");
            var result2 = Python.MainModule.Execute<string>("import sys\nresult = '" + expectedResult2 + "'", "result");

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        [TestMethod]
        public void Execute_FromStringWithVariablesWithVoidReturn_Succeeds()
        {
            const int v1 = 10;
            const int v2 = 20;

            var pd = new Dictionary<string, object> { { "v1", v1 }, { "v2", v2 } };

            Python.MainModule.Execute("result = v1 + v2", pd);
            Python.MainModule.Execute("result = v1 * v2", pd);
        }

        [TestMethod]
        public void Execute_FromStringWithVariablesWithSingleReturnsParameter_ReturnsExpectedValue()
        {
            const int v1 = 10;
            const int v2 = 20;

            var pd = new Dictionary<string, object> { { "v1", v1 }, { "v2", v2 } };

            var result1 = Python.MainModule.Execute<int>("result = v1 + v2", pd, "result");
            var result2 = Python.MainModule.Execute<int>("result = v1 * v2", pd, "result");

            Assert.AreEqual(v1 + v2, result1);
            Assert.AreEqual(v1 * v2, result2);
        }

        [TestMethod]
        public void Execute_FromStringWithVariablesWithMultipleReturnsParameters_ReturnsExpectedValue()
        {
            const int v1 = 10;
            const int v2 = 20;

            var pd = new Dictionary<string, object> { { "v1", v1 }, { "v2", v2 } };
            var pr = new Dictionary<string, Type> { { "result1", typeof(int) }, { "result2", typeof(int) } };

            var results = Python.MainModule.Execute("result1 = v1 + v2\nresult2 = v1 * v2", pd, pr);

            Assert.AreEqual(v1 + v2, (int)results[0]);
            Assert.AreEqual(v1 * v2, (int)results[1]);
        }

        [TestMethod]
        public void Execute_FromStringWithNoVariablesWithMultipleReturnsParameters_ReturnsExpectedValue()
        {
            var pr = new Dictionary<string, Type> { { "result1", typeof(int) }, { "result2", typeof(int) } };

            var results = Python.MainModule.Execute("result1 = 10 + 20\nresult2 = 10 * 20", pr);

            Assert.AreEqual(30, (int)results[0]);
            Assert.AreEqual(200, (int)results[1]);
        }

        [TestMethod]
        public void Dictionary_PropertyGet_ReturnsExpectedValue()
        {
            var results = Python.MainModule.Dictionary;

            Assert.IsNotNull(results);
        }
    }
}
