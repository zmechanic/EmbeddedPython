namespace EmbeddedPython.UnitTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public abstract class PythonVersionSpecificUnitTestBase
    {
        private IPython _python;

        public IPython Python
        {
            get
            {
                return _python;
            }
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            string className = GetType().Name.ToLower();

            if (className.StartsWith("v2"))
            {
                _python = v2.Python.Instance;
            }
            else if (className.StartsWith("v3"))
            {
                _python = v3.Python.Instance;
            }
            else
            {
                throw new Exception("Incorrect name of test class.");
            }
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            Python.Dispose();
        }
    }
}