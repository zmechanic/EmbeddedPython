using System;

namespace EmbeddedPython
{
    /// <summary>
    /// Python dictionary.
    /// </summary>
    public interface IPythonDictionary : IDisposable
    {
        /// <summary>
        /// Gets or sets value of specified key.
        /// </summary>
        /// <param key="key">Key name.</param>
        /// <returns>Key value.</returns>
        object this[string key] { get; set; }

        /// <summary>
        /// Adds item into dictionary.
        /// </summary>
        /// <typeparam key="T">Type of value.</typeparam>
        /// <param key="key">Name of dictionary item to be added.</param>
        /// <param key="value">Value of dictionary item to be added.</param>
        void Set<T>(string key, T value);

        /// <summary>
        /// Gets item value from dictionary.
        /// </summary>
        /// <typeparam key="T">Type of value.</typeparam>
        /// <param key="key">Name of dictionary item.</param>
        /// <returns>Value of dictionary item.</returns>
        T Get<T>(string key);

        /// <summary>
        /// Gets item value from dictionary.
        /// </summary>
        /// <param key="key">Name of dictionary item.</param>
        /// <param key="t">Type of value.</param>
        /// <returns>Value of dictionary item.</returns>
        object Get(string key, Type t);
    }
}