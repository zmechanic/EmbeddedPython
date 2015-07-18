using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedPython.UnitTests
{
    [TestClass]
    [DeploymentItem(@"Python\ObjectTest\Main.py")]
    public class V3PythonModuleUnitTests : PythonModuleUnitTestsBase
    {
    }
}
