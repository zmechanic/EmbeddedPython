using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    [TestClass]
    [DeploymentItem(@"Python\FunctionsInvokeTest\Main.py")]
    public class V3PythonFunctionUnitTests : PythonFunctionUnitTestsBase
    {
    }
}
