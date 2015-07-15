using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    public abstract class PythonTupleUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        [TestMethod]
        public void Tuple_Create_StressTest()
        {
            for (var i = 0; i < 10000; i++)
            {
                var testTarget = Python.Factory.CreateTuple(8);
                testTarget.Dispose();
            }
        }

        [TestMethod]
        public void Indexer_Set_Succeeds()
        {
            using (var testTarget = Python.Factory.CreateTuple(8))
            {
                for (var i = 0; i < 8; i++)
                {
                    testTarget[i] = i;
                }
            }
        }

        [TestMethod]
        public void Indexer_Get_ReturnsCorrectValue()
        {
            using (var testTarget = Python.Factory.CreateTuple(8))
            {
                for (var i = 0; i < 8; i++)
                {
                    testTarget[i] = i;
                }

                for (var i = 0; i < 8; i++)
                {
                    Assert.AreEqual(i, testTarget[i]);
                    testTarget[i] = i;
                }
            }
        }

        [TestMethod]
        public void Set_GenericType_Succeeds()
        {
            using (var testTarget = Python.Factory.CreateTuple(8))
            {
                for (var i = 0; i < 8; i++)
                {
                    testTarget.Set(i, i);
                }
            }
        }

        [TestMethod]
        public void Get_GenericType_ReturnsCorrectValue()
        {
            using (var testTarget = Python.Factory.CreateTuple(8))
            {
                for (var i = 0; i < 8; i++)
                {
                    testTarget.Set(i, i);
                }

                for (var i = 0; i < 8; i++)
                {
                    var result = testTarget.Get<int>(i);
                    Assert.AreEqual(i, result);
                }
            }
        }

        [TestMethod]
        public void Get_ExplicitType_ReturnsCorrectValue()
        {
            using (var testTarget = Python.Factory.CreateTuple(8))
            {
                for (var i = 0; i < 8; i++)
                {
                    testTarget.Set(i, i);
                }

                for (var i = 0; i < 8; i++)
                {
                    var result = testTarget.Get(i, typeof(int));
                    Assert.AreEqual(i, result);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Get_GenericTypeInvalidKeyValue_ThrowsException()
        {
            using (var testTarget = Python.Factory.CreateTuple(1))
            {
                testTarget.Get<int>(1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PythonException))]
        public void Get_ExplicitTypeInvalidKeyValue_ThrowsException()
        {
            using (var testTarget = Python.Factory.CreateTuple(1))
            {
                testTarget.Get(1, typeof(int));
            }
        }
    }
}
