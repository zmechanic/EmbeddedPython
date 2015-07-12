using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    [TestClass]
    [DeploymentItem(@"Python\PassThrough\Main.py")]
    public class V3PassThroughUnitTests : PassThroughUnitTestsBase
    {
        [TestMethod]
        public void PassThrough_Bytes_ReturnsSameValue()
        {
            var bytes = new byte[65535];

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)i;
            }

            TestPassThrough(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }
    }
}
