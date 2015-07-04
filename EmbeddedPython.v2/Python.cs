using System;
using EmbeddedPython.Internal;

namespace EmbeddedPython.v2
{
    public class Python : PythonBase
    {
        private static bool _isInitialized;

        private readonly static IPython _instance = new Python();

        private Python()
        {
        }

        public static IPython Instance
        {
            get
            {
                lock (_instance)
                {
                    if (!_isInitialized)
                    {
                        PythonConcrete.Initialize();
                        _isInitialized = true;
                    }
                }

                return _instance;
            }
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (_instance)
                {
                    PythonConcrete.Deinitialize();
                    _isInitialized = false;
                }
            }
        }
    }
}