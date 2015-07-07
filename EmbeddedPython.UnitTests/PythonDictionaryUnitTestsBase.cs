using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    public abstract class PythonDictionaryUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        [TestMethod]
        public void Indexer_Set_Succeeds()
        {
            var testTarget = Python.Factory.CreateDictionary();
            testTarget["testValue"] = 1;
        }

        [TestMethod]
        public void Indexer_Get_Succeeds()
        {
            const int value = 1;

            var testTarget = Python.Factory.CreateDictionary();
            testTarget["testValue"] = value;

            var result = testTarget["testValue"];

            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void Set_GenericType_Succeeds()
        {
            var testTarget = Python.Factory.CreateDictionary();
            testTarget.Set("testValue", 1);
        }

        [TestMethod]
        public void Get_GenericType_ReturnsCorrectValue()
        {
            const int value = 1;

            var testTarget = Python.Factory.CreateDictionary();
            testTarget.Set("testValue", value);

            var result = testTarget.Get<int>("testValue");

            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void Get_ExplicitType_ReturnsCorrectValue()
        {
            const int value = 1;

            var testTarget = Python.Factory.CreateDictionary();
            testTarget.Set("testValue", value);

            var result = testTarget.Get("testValue", typeof(int));

            Assert.AreEqual(value, result);
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Get_GenericTypeInvalidKeyValue_ThrowsException()
        {
            var testTarget = Python.Factory.CreateDictionary();
            testTarget.Get<int>("noKey");
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Get_ExplicitTypeInvalidKeyValue_ThrowsException()
        {
            var testTarget = Python.Factory.CreateDictionary();
            testTarget.Get("noKey", typeof(int));
        }
    }
}
