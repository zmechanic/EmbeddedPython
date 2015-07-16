using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    [TestClass]
    [DeploymentItem(@"Python\MethodInvokeTest\Main.py")]
    public class V3PythonMethodUnitTests : PythonMethodUnitTestsBase
    {
    }
}
