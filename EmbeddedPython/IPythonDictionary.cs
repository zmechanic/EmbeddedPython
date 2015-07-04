using System;

namespace EmbeddedPython
{
    /// <summary>
    /// Python dictionary.
    /// </summary>
    public interface IPythonDictionary : IDisposable
    {
        /// <summary>
        /// Adds item into dictionary.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="name">Name of dictionary item to be added.</param>
        /// <param name="value">Value of dictionary item to be added.</param>
        void Add<T>(string name, T value);

        /// <summary>
        /// Gets item value from dictionary.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="name">Name of dictionary item.</param>
        /// <returns>Value of dictionary item.</returns>
        T Get<T>(string name);
    }
}