using System;

namespace EmbeddedPython
{
    /// <summary>
    /// Python function.
    /// </summary>
    public interface IPythonFunction : IDisposable
    {
        /// <summary>
        /// Invokes function with no arguments and returns result of function invocation converted to type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <returns>Result of function invoke.</returns>
        TResult Invoke<TResult>();

        /// <summary>
        /// Invokes function with single argument and returns result of function invocation converted to type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T">Type of invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="arg">Function argument.</param>
        /// <returns>Result of function invoke.</returns>
        TResult Invoke<T, TResult>(T arg);

        /// <summary>
        /// Invokes function with two arguments and returns result of function invocation converted to type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <returns>Result of function invoke.</returns>
        TResult Invoke<T1, T2, TResult>(T1 arg1, T2 arg2);

        /// <summary>
        /// Invokes function with three arguments and returns result of function invocation converted to type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <returns>Result of function invoke.</returns>
        TResult Invoke<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        /// Invokes function with four arguments and returns result of function invocation converted to type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <returns>Result of function invoke.</returns>
        TResult Invoke<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        /// Invokes function with five arguments and returns result of function invocation converted to type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        /// <returns>Result of function invoke.</returns>
        TResult Invoke<T1, T2, T3, T4, T5, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        /// <summary>
        /// Invokes function with six arguments and returns result of function invocation converted to type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        /// <param name="arg6">Sixth argument.</param>
        /// <returns>Result of function invoke.</returns>
        TResult Invoke<T1, T2, T3, T4, T5, T6, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

        /// <summary>
        /// Invokes function with seven arguments and returns result of function invocation converted to type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="T7">Type of seventh invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        /// <param name="arg6">Sixth argument.</param>
        /// <param name="arg7">Seventh argument.</param>
        /// <returns>Result of function invoke.</returns>
        TResult Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

        /// <summary>
        /// Invokes function with eight arguments and returns result of function invocation converted to type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="T7">Type of seventh invocation argument.</typeparam>
        /// <typeparam name="T8">Type of eighth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        /// <param name="arg6">Sixth argument.</param>
        /// <param name="arg7">Seventh argument.</param>
        /// <param name="arg8">Eighth argument.</param>
        /// <returns>Result of function invoke.</returns>
        TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
    }
}