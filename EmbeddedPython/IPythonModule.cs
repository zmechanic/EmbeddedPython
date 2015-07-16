using System;
using System.Collections.Generic;

namespace EmbeddedPython
{
    /// <summary>
    /// Python module.
    /// </summary>
    public interface IPythonModule : IPythonObject
    {
        /// <summary>
        /// Gets full name of Python module.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets dictionary of module scoped variables.
        /// </summary>
        IPythonDictionary Dictionary { get; }

        /// <summary>
        /// Executes Python code.
        /// This method can execute multiline code, and is different to evaluation functionality.
        /// </summary>
        /// <param name="code">Python code to execute.</param>
        void Execute(string code);

        /// <summary>
        /// Executes Python code and returns result from specified <paramref name="resultVariable"/> variable.
        /// This method can execute multiline code, and is different to evaluation functionality.
        /// </summary>
        /// <typeparam name="T">Type of result to return.</typeparam>
        /// <param name="code">Python code to execute.</param>
        /// <param name="resultVariable">Name of result variable.</param>
        /// <returns>Result value retrieved from <paramref name="resultVariable"/> variable.</returns>
        T Execute<T>(string code, string resultVariable);

        /// <summary>
        /// Executes Python code with local variables initialization.
        /// This method can execute multiline code, and is different to evaluation functionality.
        /// </summary>
        /// <param name="code">Python code to execute.</param>
        /// <param name="variables">Collection of supplied variables.</param>
        void Execute(string code, IDictionary<string, object> variables);

        /// <summary>
        /// Executes Python code with local variables initialization and returns result from specified <paramref name="resultVariable"/> variable.
        /// This method can execute multiline code, and is different to evaluation functionality.
        /// </summary>
        /// <typeparam name="T">Type of result to return.</typeparam>
        /// <param name="code">Python code to execute.</param>
        /// <param name="variables">Collection of supplied variables.</param>
        /// <param name="resultVariable">Name of result variable.</param>
        /// <returns>Result value retrieved from <paramref name="resultVariable"/> variable.</returns>
        T Execute<T>(string code, IDictionary<string, object> variables, string resultVariable);

        /// <summary>
        /// Executes Python code with local variables initialization and returns all variables specified in collection of<paramref name="resultVariables"/>.
        /// This method can execute multiline code, and is different to evaluation functionality.
        /// </summary>
        /// <param name="code">Python code to execute.</param>
        /// <param name="variables">Collection of supplied variables.</param>
        /// <param name="resultVariables">Collection of names and types of result variables.</param>
        /// <returns>Collection of result values retrieved from <paramref name="resultVariables"/> variables in exactly the same order as they were specified.</returns>
        object[] Execute(string code, IDictionary<string, object> variables, IDictionary<string, Type> resultVariables);

        /// <summary>
        /// Executes Python code and returns all variables specified in collection of<paramref name="resultVariables"/>.
        /// This method can execute multiline code, and is different to evaluation functionality.
        /// </summary>
        /// <param name="code">Python code to execute.</param>
        /// <param name="resultVariables">Collection of names and types of result variables.</param>
        /// <returns>Collection of result values retrieved from <paramref name="resultVariables"/> variables in exactly the same order as they were specified.</returns>
        object[] Execute(string code, IDictionary<string, Type> resultVariables);
    }
}