using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    [TestClass]
    [DeploymentItem(@"Python\PassThrough\Main.py")]
    public class V2PassThroughUnitTests : PassThroughUnitTestsBase
    {
    }
}
