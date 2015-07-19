using System;
using System.Collections.Generic;

namespace EmbeddedPython
{
    /// <summary>
    /// Python dictionary.
    /// </summary>
    public interface IPythonDictionary : IPythonObject
    {
        /// <summary>
        /// Gets or sets value of specified key.
        /// </summary>
        /// <param key="key">Key name.</param>
        /// <returns>Item value.</returns>
        object this[string key] { get; set; }

        /// <summary>
        /// Gets list of keys in the dictionary.
        /// </summary>
        IEnumerable<string> Keys { get; }

        /// <summary>
        /// Adds item into the dictionary.
        /// </summary>
        /// <typeparam key="T">Type of value.</typeparam>
        /// <param key="key">Name of dictionary item to be added.</param>
        /// <param key="value">Value of dictionary item to be added.</param>
        void Set<T>(string key, T value);

        /// <summary>
        /// Gets item value from the dictionary.
        /// </summary>
        /// <typeparam key="T">Type of value.</typeparam>
        /// <param key="key">Name of dictionary item.</param>
        /// <returns>Value of dictionary item.</returns>
        T Get<T>(string key);

        /// <summary>
        /// Gets item value from the dictionary.
        /// </summary>
        /// <param key="key">Name of dictionary item.</param>
        /// <param key="t">Type of value.</param>
        /// <returns>Value of dictionary item.</returns>
        object Get(string key, Type t);

        /// <summary>
        /// Checks if key is present in the dictionary.
        /// </summary>
        /// <param name="key">Name of dictionary item.</param>
        /// <returns>True if key is present in the dictionary.</returns>
        bool HasKey(string key);

        /// <summary>
        /// Deletes key from the dictionary.
        /// </summary>
        /// <param name="key">Name of dictionary item.</param>
        void Delete(string key);

        /// <summary>
        /// Clears the dictionary.
        /// </summary>
        void Clear();
    }
}