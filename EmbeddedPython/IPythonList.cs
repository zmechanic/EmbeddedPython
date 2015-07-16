namespace EmbeddedPython
{
    using System;

    /// <summary>
    /// Python list.
    /// </summary>
    public interface IPythonList : IPythonObject
    {
        /// <summary>
        /// Gets or sets value of specified item.
        /// </summary>
        /// <param name="index">Position of item in the list.</param>
        /// <returns>Item value.</returns>
        object this[int index] { get; set; }

        /// <summary>
        /// Gets size of the list.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Sets item in the list.
        /// </summary>
        /// <typeparam key="T">Type of value.</typeparam>
        /// <param name="index">Position of item in the list.</param>
        /// <param key="value">Value of list item to be set.</param>
        void Set<T>(int index, T value);

        /// <summary>
        /// Gets item value from the list.
        /// </summary>
        /// <typeparam key="T">Type of value.</typeparam>
        /// <param name="index">Position of item in the list.</param>
        /// <returns>Value of list item.</returns>
        T Get<T>(int index);

        /// <summary>
        /// Gets item value from the list.
        /// </summary>
        /// <param name="index">Position of item in the list.</param>
        /// <param key="t">Type of value.</param>
        /// <returns>Value of list item.</returns>
        object Get(int index, Type t);

        /// <summary>
        /// Inserts element into the Python list.
        /// </summary>
        /// <typeparam key="T">Type of value.</typeparam>
        /// <param name="index">Position of item in the list.</param>
        /// <param key="value">Value of list item to be inserted.</param>
        void Insert<T>(int index, T value);

        /// <summary>
        /// Appends element into the Python list.
        /// </summary>
        /// <typeparam key="T">Type of value.</typeparam>
        /// <param key="value">Value of list item to be added.</param>
        void Add<T>(T value);

        /// <summary>
        /// Sorts Python list.
        /// </summary>
        void Sort();

        /// <summary>
        /// Reverses Python list.
        /// </summary>
        void Reverse();

        /// <summary>
        /// Returns Python list content as Python tuple.
        /// </summary>
        /// <returns>Python tuple from list.</returns>
        IPythonTuple ToTuple();
    }
}