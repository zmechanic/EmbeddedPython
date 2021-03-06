﻿using System.Collections.Generic;

namespace EmbeddedPython
{
    using System;

    public interface IPythonObject : IDisposable
    {
        /// <summary>
        /// Gets corresponding CLR type for the current Python object.
        /// </summary>
        Type ClrType { get; }

        /// <summary>
        /// Gets hash of Python object.
        /// </summary>
        long Hash { get; }

        /// <summary>
        /// Gets size of Python object.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Gets list of descriptors for Python object.
        /// </summary>
        IEnumerable<string> Dir { get; }

        /// <summary>
        /// Gets Python object's attribute value.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="attributeName">Name of attribute.</param>
        /// <returns>Value of attribute.</returns>
        T GetAttr<T>(string attributeName);

        /// <summary>
        /// Gets flag indicating whether Python object has attribute.
        /// </summary>
        /// <param name="attributeName">Name of attribute.</param>
        /// <returns>True if attribute present</returns>
        bool HasAttr(string attributeName);

        /// <summary>
        /// Gets Python object's attribute value.
        /// </summary>
        /// <param name="attributeName">Name of attribute.</param>
        /// <returns>Value of attribute.</returns>
        object GetAttr(string attributeName);

        /// <summary>
        /// Sets Python object's attribute value.
        /// </summary>
        /// <param name="attributeName">Name of attribute.</param>
        /// <param name="value">Value of attribute.</param>
        void SetAttr(string attributeName, object value);

        /// <summary>
        /// Deletes Python object's attribute.
        /// </summary>
        /// <param name="attributeName">Name of attribute.</param>
        void DelAttr(string attributeName);

        /// <summary>
        /// Gets Python function wrapper.
        /// </summary>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>Python function wrapper.</returns>
        IPythonFunction GetPythonFunction(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function without arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET action.</returns>
        Action GetVoidFunction(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with single argument without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T">Type of invocation argument.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET action.</returns>
        Action<T> GetVoidFunction<T>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with two arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET action.</returns>
        Action<T1, T2> GetVoidFunction<T1, T2>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with three arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET action.</returns>
        Action<T1, T2, T3> GetVoidFunction<T1, T2, T3>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with four arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET action.</returns>
        Action<T1, T2, T3, T4> GetVoidFunction<T1, T2, T3, T4>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with five arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET action.</returns>
        Action<T1, T2, T3, T4, T5> GetVoidFunction<T1, T2, T3, T4, T5>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with six arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET action.</returns>
        Action<T1, T2, T3, T4, T5, T6> GetVoidFunction<T1, T2, T3, T4, T5, T6>(string functionName);

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
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET action.</returns>
        Action<T1, T2, T3, T4, T5, T6, T7> GetVoidFunction<T1, T2, T3, T4, T5, T6, T7>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with eighth arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="T7">Type of seventh invocation argument.</typeparam>
        /// <typeparam name="T8">Type of eighth invocation argument.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET action.</returns>
        Action<T1, T2, T3, T4, T5, T6, T7, T8> GetVoidFunction<T1, T2, T3, T4, T5, T6, T7, T8>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function without arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET function.</returns>
        Func<TResult> GetFunction<TResult>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with single argument without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T">Type of invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET function.</returns>
        Func<T, TResult> GetFunction<T, TResult>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with two arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET function.</returns>
        Func<T1, T2, TResult> GetFunction<T1, T2, TResult>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with three arguments without obtaining <see cref="IPythonFunction"/> first.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of function invocation result.</typeparam>
        /// <param name="functionName">Name of the Python function to get CLR invoke function for.</param>
        /// <returns>.NET function.</returns>
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
        /// <returns>.NET function.</returns>
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
        /// <returns>.NET function.</returns>
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
        /// <returns>.NET function.</returns>
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
        /// <returns>.NET function.</returns>
        Func<T1, T2, T3, T4, T5, T6, T7, TResult> GetFunction<T1, T2, T3, T4, T5, T6, T7, TResult>(string functionName);

        /// <summary>
        /// Gets function to directly invoke Python function with eighth arguments without obtaining <see cref="IPythonFunction"/> first.
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
        /// <returns>.NET function.</returns>
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> GetFunction<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(string functionName);

        /// <summary>
        /// Invokes Python method without arguments.
        /// </summary>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        void CallMethod(string methodName);

        /// <summary>
        /// Invokes Python method without arguments.
        /// </summary>
        /// <typeparam name="T">Type of invocation argument.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg">The argument.</param>
        void CallMethod<T>(string methodName, T arg);

        /// <summary>
        /// Invokes Python method with two arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        void CallMethod<T1, T2>(string methodName, T1 arg1, T2 arg2);

        /// <summary>
        /// Invokes Python method with three arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        void CallMethod<T1, T2, T3>(string methodName, T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        /// Invokes Python method with four arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        void CallMethod<T1, T2, T3, T4>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        /// Invokes Python method with five arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        void CallMethod<T1, T2, T3, T4, T5>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        /// <summary>
        /// Invokes Python method with six arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        /// <param name="arg6">Sixth argument.</param>
        void CallMethod<T1, T2, T3, T4, T5, T6>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

        /// <summary>
        /// Invokes Python method with seven arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="T7">Type of seventh invocation argument.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        /// <param name="arg6">Sixth argument.</param>
        /// <param name="arg7">Seventh argument.</param>
        void CallMethod<T1, T2, T3, T4, T5, T6, T7>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

        /// <summary>
        /// Invokes Python method with eighth arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="T7">Type of seventh invocation argument.</typeparam>
        /// <typeparam name="T8">Type of eighth invocation argument.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        /// <param name="arg6">Sixth argument.</param>
        /// <param name="arg7">Seventh argument.</param>
        /// <param name="arg8">Eighth argument.</param>
        void CallMethod<T1, T2, T3, T4, T5, T6, T7, T8>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);

        /// <summary>
        /// Invokes Python method without arguments.
        /// </summary>
        /// <typeparam name="TResult">Type of method invocation result.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <returns>Result of method invocation.</returns>
        TResult CallMethod<TResult>(string methodName);

        /// <summary>
        /// Invokes Python method without arguments.
        /// </summary>
        /// <typeparam name="T">Type of invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of method invocation result.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg">The argument.</param>
        /// <returns>Result of method invocation.</returns>
        TResult CallMethod<T, TResult>(string methodName, T arg);

        /// <summary>
        /// Invokes Python method with two arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of method invocation result.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <returns>Result of method invocation.</returns>
        TResult CallMethod<T1, T2, TResult>(string methodName, T1 arg1, T2 arg2);

        /// <summary>
        /// Invokes Python method with three arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of method invocation result.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <returns>Result of method invocation.</returns>
        TResult CallMethod<T1, T2, T3, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        /// Invokes Python method with four arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of method invocation result.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <returns>Result of method invocation.</returns>
        TResult CallMethod<T1, T2, T3, T4, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        /// Invokes Python method with five arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of method invocation result.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        /// <returns>Result of method invocation.</returns>
        TResult CallMethod<T1, T2, T3, T4, T5, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        /// <summary>
        /// Invokes Python method with six arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of method invocation result.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        /// <param name="arg6">Sixth argument.</param>
        /// <returns>Result of method invocation.</returns>
        TResult CallMethod<T1, T2, T3, T4, T5, T6, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

        /// <summary>
        /// Invokes Python method with seven arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="T7">Type of seventh invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of method invocation result.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        /// <param name="arg6">Sixth argument.</param>
        /// <param name="arg7">Seventh argument.</param>
        /// <returns>Result of method invocation.</returns>
        TResult CallMethod<T1, T2, T3, T4, T5, T6, T7, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

        /// <summary>
        /// Invokes Python method with eighth arguments.
        /// </summary>
        /// <typeparam name="T1">Type of first invocation argument.</typeparam>
        /// <typeparam name="T2">Type of second invocation argument.</typeparam>
        /// <typeparam name="T3">Type of third invocation argument.</typeparam>
        /// <typeparam name="T4">Type of forth invocation argument.</typeparam>
        /// <typeparam name="T5">Type of fifth invocation argument.</typeparam>
        /// <typeparam name="T6">Type of sixth invocation argument.</typeparam>
        /// <typeparam name="T7">Type of seventh invocation argument.</typeparam>
        /// <typeparam name="T8">Type of eighth invocation argument.</typeparam>
        /// <typeparam name="TResult">Type of method invocation result.</typeparam>
        /// <param name="methodName">Name of the Python method to get CLR invoke method for.</param>
        /// <param name="arg1">First argument.</param>
        /// <param name="arg2">Second argument.</param>
        /// <param name="arg3">Third argument.</param>
        /// <param name="arg4">Forth argument.</param>
        /// <param name="arg5">Fifth argument.</param>
        /// <param name="arg6">Sixth argument.</param>
        /// <param name="arg7">Seventh argument.</param>
        /// <param name="arg8">Eighth argument.</param>
        /// <returns>Result of method invocation.</returns>
        TResult CallMethod<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
    }
}