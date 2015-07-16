using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    public abstract class PythonListUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        [TestMethod]
        public void List_Create_StressTest()
        {
            for (var i = 0; i < 10000; i++)
            {
                var testTarget = Python.Factory.CreateList(8);
                testTarget.Dispose();
            }
        }

        [TestMethod]
        public void Size_Property_ReturnsCorrectValue()
        {
            using (var testTarget = Python.Factory.CreateList(8))
            {
                Assert.AreEqual(8, testTarget.Size);
            }
        }

        [TestMethod]
        public void Indexer_Set_Succeeds()
        {
            const int size = 1000;

            using (var testTarget = Python.Factory.CreateList(size))
            {
                for (var i = 0; i < size; i++)
                {
                    testTarget[i] = i;
                }
            }
        }

        [TestMethod]
        public void Indexer_Get_ReturnsCorrectValue()
        {
            const int size = 1000;

            using (var testTarget = Python.Factory.CreateList(size))
            {
                for (var i = 0; i < size; i++)
                {
                    testTarget[i] = i;
                }

                for (var i = 0; i < size; i++)
                {
                    Assert.AreEqual(i, testTarget[i]);
                    testTarget[i] = i;
                }
            }
        }

        [TestMethod]
        public void Set_GenericType_Succeeds()
        {
            const int size = 1000;

            using (var testTarget = Python.Factory.CreateList(size))
            {
                for (var i = 0; i < size; i++)
                {
                    testTarget.Set(i, i);
                }
            }
        }

        [TestMethod]
        public void Get_GenericType_ReturnsCorrectValue()
        {
            const int size = 1000;

            using (var testTarget = Python.Factory.CreateList(size))
            {
                for (var i = 0; i < size; i++)
                {
                    testTarget.Set(i, i);
                }

                for (var i = 0; i < size; i++)
                {
                    var result = testTarget.Get<int>(i);
                    Assert.AreEqual(i, result);
                }
            }
        }

        [TestMethod]
        public void Get_ExplicitType_ReturnsCorrectValue()
        {
            const int size = 1000;

            using (var testTarget = Python.Factory.CreateList(size))
            {
                for (var i = 0; i < size; i++)
                {
                    testTarget.Set(i, i);
                }

                for (var i = 0; i < size; i++)
                {
                    var result = testTarget.Get(i, typeof(int));
                    Assert.AreEqual(i, result);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Get_GenericTypeInvalidIndex_ThrowsException()
        {
            using (var testTarget = Python.Factory.CreateList(1))
            {
                testTarget.Get<int>(1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Get_ExplicitTypeInvalidIndex_ThrowsException()
        {
            using (var testTarget = Python.Factory.CreateList(1))
            {
                testTarget.Get(1, typeof(int));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Set_GenericTypeInvalidIndex_ThrowsException()
        {
            using (var testTarget = Python.Factory.CreateList(1))
            {
                testTarget.Set(1, 1);
            }
        }
    }
}
