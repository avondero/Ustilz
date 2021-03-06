﻿namespace Ustilz.Extensions.Actions
{
    #region Usings

    using System;
    using System.Linq;

    using JetBrains.Annotations;

    #endregion

    /// <summary>Class containing some extension methods for <see cref="Action" />.</summary>
    public static partial class ExtensionsAction
    {
        #region Méthodes publiques

        /// <summary>Executes the given action inside of a try catch block and catches all exception expect the specified type.</summary>
        /// <exception cref="ArgumentNullException">Action can not be null.</exception>
        /// <typeparam name="TException">The type of the exception to throw.</typeparam>
        /// <param name="action">The action to execute.</param>
        /// <returns>Returns true if the action was executed without an exception, otherwise false.</returns>
        [PublicAPI]
        public static bool SafeExecuteExcept<TException>(this Action action)
            where TException : Exception
            => action.SafeExecuteExcept(typeof(TException));

        /// <summary>Executes the given action inside of a try catch block and catches all exception expect the specified types.</summary>
        /// <exception cref="ArgumentNullException">Action can not be null.</exception>
        /// <typeparam name="TException1">The first exception type to throw.</typeparam>
        /// <typeparam name="TException2">The second exception type to throw.</typeparam>
        /// <param name="action">The action to execute.</param>
        /// <returns>Returns true if the action was executed without an exception, otherwise false.</returns>
        public static bool SafeExecuteExcept<TException1, TException2>(this Action action)
            where TException1 : Exception
            where TException2 : Exception
            => action.SafeExecuteExcept(typeof(TException1), typeof(TException2));

        /// <summary>Executes the given action inside of a try catch block and catches all exception expect the specified types.</summary>
        /// <exception cref="ArgumentNullException">Action can not be null.</exception>
        /// <typeparam name="TException1">The first exception type to throw.</typeparam>
        /// <typeparam name="TException2">The second exception type to throw.</typeparam>
        /// <typeparam name="TException3">The third exception type to throw.</typeparam>
        /// <param name="action">The action to execute.</param>
        /// <returns>Returns true if the action was executed without an exception, otherwise false.</returns>
        public static bool SafeExecuteExcept<TException1, TException2, TException3>(this Action action)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
            => action.SafeExecuteExcept(typeof(TException1), typeof(TException2), typeof(TException3));

        /// <summary>Executes the given action inside of a try catch block and catches all exception expect the specified types.</summary>
        /// <exception cref="ArgumentNullException">Action can not be null.</exception>
        /// <typeparam name="TException1">The first exception type to throw.</typeparam>
        /// <typeparam name="TException2">The second exception type to throw.</typeparam>
        /// <typeparam name="TException3">The third exception type to throw.</typeparam>
        /// <typeparam name="TException4">The fourth exception type to throw.</typeparam>
        /// <param name="action">The action to execute.</param>
        /// <returns>Returns true if the action was executed without an exception, otherwise false.</returns>
        public static bool SafeExecuteExcept<TException1, TException2, TException3, TException4>(this Action action)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
            where TException4 : Exception
            => action.SafeExecuteExcept(typeof(TException1), typeof(TException2), typeof(TException3), typeof(TException4));

        /// <summary>Executes the given action inside of a try catch block and catches all exception expect the given ones.</summary>
        /// <exception cref="ArgumentNullException">Action can not be null.</exception>
        /// <exception cref="ArgumentNullException">ExceptionsToThrow can not be null.</exception>
        /// <param name="action">The action to execute.</param>
        /// <param name="exceptionsToThrow">The exceptions to throw.</param>
        /// <returns>Returns true if the action was executed without an exception, otherwise false.</returns>
        public static bool SafeExecuteExcept(this Action action, params Type[] exceptionsToThrow)
        {
            action.ThrowIfNull(nameof(action));
            exceptionsToThrow.ThrowIfNull(nameof(exceptionsToThrow));

            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                if (exceptionsToThrow.Any(x => x == ex.GetType()))
                {
                    throw;
                }

                return false;
            }
        }

        #endregion
    }
}
