using System;

namespace EmbeddedPython
{
    /// <summary>
    /// Python tuple.
    /// </summary>
    public interface IPythonTuple : IPythonObject
    {
        /// <summary>
        /// Gets or sets value of specified item.
        /// </summary>
        /// <param name="position">Position of item in the tuple.</param>
        /// <returns>Item value.</returns>
        object this[int position] { get; set; }

        /// <summary>
        /// Gets size of the tuple.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Sets item in the tuple.
        /// </summary>
        /// <typeparam key="T">Type of value.</typeparam>
        /// <param name="position">Position of item in the tuple.</param>
        /// <param key="value">Value of tuple item to be added.</param>
        void Set<T>(int position, T value);

        /// <summary>
        /// Gets item value from the tuple.
        /// </summary>
        /// <typeparam key="T">Type of value.</typeparam>
        /// <param name="position">Position of item in the tuple.</param>
        /// <returns>Value of tuple item.</returns>
        T Get<T>(int position);

        /// <summary>
        /// Gets item value from the tuple.
        /// </summary>
        /// <param name="position">Position of item in the tuple.</param>
        /// <param key="t">Type of value.</param>
        /// <returns>Value of tuple item.</returns>
        object Get(int position, Type t);
    }
}