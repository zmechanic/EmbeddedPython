using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    public abstract class PythonDictionaryUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        [TestMethod]
        public void Dictionary_Create_StressTest()
        {
            for (var i = 0; i < 10000; i++)
            {
                var testTarget = Python.Factory.CreateDictionary();
                testTarget.Dispose();
            }
        }

        [TestMethod]
        public void Indexer_Set_Succeeds()
        {
            using (var testTarget = Python.Factory.CreateDictionary())
            {
                testTarget["testKey"] = 1;
            }
        }

        [TestMethod]
        public void Indexer_Get_ReturnsCorrectValue()
        {
            const int value = 1;

            using (var testTarget = Python.Factory.CreateDictionary())
            {
                testTarget["testKey"] = value;

                var result = testTarget["testKey"];

                Assert.AreEqual(value, result);
            }
        }

        [TestMethod]
        public void Set_GenericType_Succeeds()
        {
            using (var testTarget = Python.Factory.CreateDictionary())
            {
                testTarget.Set("testKey", 1);
            }
        }

        [TestMethod]
        public void Get_GenericType_ReturnsCorrectValue()
        {
            const int value = 1;

            using (var testTarget = Python.Factory.CreateDictionary())
            {
                testTarget.Set("testKey", value);

                var result = testTarget.Get<int>("testKey");

                Assert.AreEqual(value, result);
            }
        }

        [TestMethod]
        public void Get_ExplicitType_ReturnsCorrectValue()
        {
            const int value = 1;

            using (var testTarget = Python.Factory.CreateDictionary())
            {
                testTarget.Set("testKey", value);

                var result = testTarget.Get("testKey", typeof (int));

                Assert.AreEqual(value, result);
            }
        }

        [TestMethod]
        public void Clear_NoParameters_ClearsDictionary()
        {
            using (var testTarget = Python.Factory.CreateDictionary())
            {
                testTarget.Set("testKey", 1);
                testTarget.Clear();

                Assert.IsFalse(testTarget.HasKey("testKey"));
            }
        }

        [TestMethod]
        public void HasKey_Property_ReturnsCorrectValue()
        {
            using (var testTarget = Python.Factory.CreateDictionary())
            {
                testTarget.Set("testKey", 1);

                Assert.IsTrue(testTarget.HasKey("testKey"));
                Assert.IsFalse(testTarget.HasKey("missingKey"));
            }
        }

        [TestMethod]
        public void Keys_Property_ReturnsCorrectList()
        {
            using (var testTarget = Python.Factory.CreateDictionary())
            {
                testTarget.Set("testKey", 1);
                testTarget.Set("someOtherKey", 1);

                var result = testTarget.Keys.ToList();

                CollectionAssert.Contains(result, "testKey");
                CollectionAssert.Contains(result, "someOtherKey");
            }
        }

        [TestMethod]
        public void Delete_ByKey_RemovesItem()
        {
            using (var testTarget = Python.Factory.CreateDictionary())
            {
                testTarget.Set("testKey", 1);
                testTarget.Set("someOtherKey", 1);

                testTarget.Delete("testKey");

                Assert.IsFalse(testTarget.HasKey("testKey"));
                Assert.IsTrue(testTarget.HasKey("someOtherKey"));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Get_GenericTypeInvalidKeyValue_ThrowsException()
        {
            using (var testTarget = Python.Factory.CreateDictionary())
            {
                testTarget.Get<int>("noKey");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Get_ExplicitTypeInvalidKeyValue_ThrowsException()
        {
            using (var testTarget = Python.Factory.CreateDictionary())
            {
                testTarget.Get("noKey", typeof (int));
            }
        }
    }
}
