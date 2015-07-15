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
        /// <returns>Newly created Python tuple.</returns>
        IPythonTuple CreateTuple();

        /// <summary>
        /// Creates Python list.
        /// </summary>
        /// <returns>Newly created Python list.</returns>
        IPythonList CreateList();
    }
}