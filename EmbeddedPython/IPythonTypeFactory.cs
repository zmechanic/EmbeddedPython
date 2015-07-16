namespace EmbeddedPython
{
    /// <summary>
    /// Factory to create CLR wrappers for native Python types.
    /// </summary>
    public interface IPythonTypeFactory
    {
        /// <summary>
        /// Creates Python dictionary.
        /// </summary>
        /// <returns>Newly created Python dictionary.</returns>
        IPythonDictionary CreateDictionary();

        /// <summary>
        /// Creates Python tuple.
        /// </summary>
        /// <param name="size">Number of items in the tuple.</param>
        /// <returns>Newly created Python tuple.</returns>
        IPythonTuple CreateTuple(int size);

        /// <summary>
        /// Creates Python list.
        /// </summary>
        /// <param name="size">Number of elements in the list.</param>
        /// <returns>Newly created Python list.</returns>
        IPythonList CreateList(int size);
    }
}