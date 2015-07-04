using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    using System.Collections.Generic;

    public abstract class PassThroughUnitTestsBase : PythonVersionSpecificUnitTestBase
    {
        [TestMethod]
        public void FunctionInvoke_NullString_ReturnsSameValue()
        {
            TestPassThrough<string>(null);
        }

        [TestMethod]
        public void FunctionInvoke_EmptyString_ReturnsSameValue()
        {
            TestPassThrough(string.Empty);
        }

        [TestMethod]
        public void FunctionInvoke_TestString_ReturnsSameValue()
        {
            TestPassThrough("Test String");
        }

        [TestMethod]
        public void FunctionInvoke_TestUnicodeNullString_ReturnsSameValue()
        {
            TestPassThrough<PythonUnicodeString>(null);
        }

        [TestMethod]
        public void FunctionInvoke_TestUnicodeEmptyString_ReturnsSameValue()
        {
            TestPassThrough(PythonUnicodeString.Empty);
        }

        [TestMethod]
        public void FunctionInvoke_TestUnicodeString_ReturnsSameValue()
        {
            TestPassThrough(PythonUnicodeString.FromString("«κόσμε»"));
        }

        [TestMethod]
        public void FunctionInvoke_Boolean_ReturnsSameValue()
        {
            TestPassThrough(true);
            TestPassThrough(false);
        }

        [TestMethod]
        public void FunctionInvoke_Byte_ReturnsSameValue()
        {
            TestPassThrough((byte)0);
            TestPassThrough(byte.MinValue);
            TestPassThrough(byte.MaxValue);
        }

        [TestMethod]
        public void FunctionInvoke_Short_ReturnsSameValue()
        {
            TestPassThrough((short)0);
            TestPassThrough(short.MinValue);
            TestPassThrough(short.MaxValue);
        }

        [TestMethod]
        public void FunctionInvoke_UnsignedShort_ReturnsSameValue()
        {
            TestPassThrough((ushort)0);
            TestPassThrough(ushort.MinValue);
            TestPassThrough(ushort.MaxValue);
        }

        [TestMethod]
        public void FunctionInvoke_Int_ReturnsSameValue()
        {
            TestPassThrough((int)0);
            TestPassThrough(int.MinValue);
            TestPassThrough(int.MaxValue);
        }

        [TestMethod]
        public void FunctionInvoke_UnsignedInt_ReturnsSameValue()
        {
            TestPassThrough((uint)0);
            TestPassThrough(uint.MinValue);
            TestPassThrough(uint.MaxValue);
        }

        [TestMethod]
        public void FunctionInvoke_Long_ReturnsSameValue()
        {
            TestPassThrough((long)0);
            TestPassThrough(long.MinValue);
            TestPassThrough(long.MaxValue);
        }

        [TestMethod]
        public void FunctionInvoke_UnsignedLong_ReturnsSameValue()
        {
            TestPassThrough((ulong)0);
            TestPassThrough(ulong.MinValue);
            TestPassThrough(ulong.MaxValue);
        }

        [TestMethod]
        public void FunctionInvoke_Tuple_ReturnsSameValue()
        {
            TestPassThrough(new Tuple<int>(1));
            TestPassThrough(new Tuple<int, int>(1, 2));
            TestPassThrough(new Tuple<int, int, int>(1, 2, 3));
            TestPassThrough(new Tuple<int, int, int, int>(1, 2, 3, 4));
            TestPassThrough(new Tuple<int, int, int, int, int>(1, 2, 3, 4, 5));
            TestPassThrough(new Tuple<int, int, int, int, int, int>(1, 2, 3, 4, 5, 6));
            TestPassThrough(new Tuple<int, int, int, int, int, int, int>(1, 2, 3, 4, 5, 6, 7));
            TestPassThrough(new Tuple<int, int, int, int, int, int, int, Tuple<int>>(1, 2, 3, 4, 5, 6, 7, new Tuple<int>(8)));
        }

        [TestMethod]
        public void FunctionInvoke_Bytes_ReturnsSameValue()
        {
            var bytes = new byte[65535];

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)i;
            }

            TestPassThrough(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9});
        }

        private void TestPassThrough<T>(T value)
        {
            var result = Python.MainModule.Execute<T>("result = v", new Dictionary<string, object> { { "v", value } }, "result");

            if (value is ICollection && result is ICollection)
            {
                CollectionAssert.AreEqual((ICollection)value, (ICollection)result);
                return;
            }

            Assert.AreEqual(result, value);
        }
    }
}
