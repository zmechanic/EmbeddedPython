using System;
using System.Collections.Generic;

namespace EmbeddedPython
{
    /// <summary>
    /// Python module.
    /// </summary>
    public interface IPythonModule : IDisposable
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
        /// Gets function to directly invoke Python function with single argument without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T">Type of invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>Function.</returns>
        Func<T, TResult> GetFunction<T, TResult>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with two arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>Function.</returns>
        Func<T1, T2, TResult> GetFunction<T1, T2, TResult>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with three arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>Function.</returns>
        Func<T1, T2, T3, TResult> GetFunction<T1, T2, T3, TResult>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with four arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>Function.</returns>
        Func<T1, T2, T3, T4, TResult> GetFunction<T1, T2, T3, T4, TResult>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with five arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>Function.</returns>
        Func<T1, T2, T3, T4, T5, TResult> GetFunction<T1, T2, T3, T4, T5, TResult>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with six arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>Function.</returns>
        Func<T1, T2, T3, T4, T5, T6, TResult> GetFunction<T1, T2, T3, T4, T5, T6, TResult>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with seven arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="T7">Type of seventh invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>Function.</returns>
        Func<T1, T2, T3, T4, T5, T6, T7, TResult> GetFunction<T1, T2, T3, T4, T5, T6, T7, TResult>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with eigth arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="T7">Type of seventh invocation argument.</typeparam>
        /// <typeparam name="T8">Type of eighth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>Function.</returns>
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> GetFunction<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(string functionName);

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