namespace Ustilz.Extensions
{
    #region Usings

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using JetBrains.Annotations;

    #endregion

    /// <summary>The extensions t. </summary>
    [PublicAPI]
    public static class ExtensionsT
    {
        #region Méthodes publiques

        /// <summary>
        ///     Méthode de "transformation" d'objet en boolean.
        /// </summary>
        /// <typeparam name="T">Type à "tranformer".</typeparam>
        /// <param name="obj">Objet à "tranformer".</param>
        /// <returns>Retourne l'interprétation de l'objet passé en paramètre en booléen.</returns>
        public static bool AsBool<T>(this T obj)
        {
            switch (obj)
            {
                case int n:
                    return n > 0;
                case string s:
                {
                    if (bool.TryParse(s, out var b))
                    {
                        return b;
                    }

                    if (int.TryParse(s, out var i))
                    {
                        return i.AsBool();
                    }

                    return s != string.Empty;
                }

                case object o when o is string s:
                {
                    return s.AsBool();
                }

                case object o when o is int i:
                {
                    return i.AsBool();
                }

                case IConvertible c:
                {
                    return c.ToBoolean(null);
                }

                case object o:
                {
                    return Convert.ToBoolean(o);
                }

                default:
                    return false;
            }
        }

        /// <summary>The between. </summary>
        /// <param name="value">The value. </param>
        /// <param name="from">The from. </param>
        /// <param name="to">The to. </param>
        /// <typeparam name="T">Type à comparer </typeparam>
        /// <returns>The <see cref="bool" />. </returns>
        public static bool Between<T>([NotNull] this T value, T from, T to)
            where T : IComparable<T>
            => value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;

        /// <summary>Executes the action specified, which the given object as parameter.</summary>
        /// <remarks>Use this method to chain method calls on the same object.</remarks>
        /// <exception cref="ArgumentNullException">The action can not be null.</exception>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to act on.</param>
        /// <param name="action">The action.</param>
        /// <returns>Returns the given object.</returns>
        [PublicAPI]
        [Pure]
        public static T Chain<T>([CanBeNull] this T obj, [NotNull] Action<T> action)
        {
            action.ThrowIfNull(nameof(action));

            action(obj);
            return obj;
        }

        /// <summary>The dump. </summary>
        /// <param name="o">The o. </param>
        /// <typeparam name="T">The Type </typeparam>
        /// <returns>The <see cref="T" />. </returns>
        public static T Dump<T>(this T o)
        {
            if (o is IEnumerable list)
            {
                var enumerable = list as object[] ?? list.Cast<object>().ToArray();
                Console.WriteLine("[{0}]", string.Join(", ", enumerable.Select(t => t.Dump()).ToArray()));
                return o;
            }

            Console.WriteLine(o);
            return o;
        }

        /// <summary>The if null.</summary>
        /// <param name="source">The source.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <typeparam name="T">Type de l'objet</typeparam>
        /// <returns>The <see cref="T" />.</returns>
        public static T IfNull<T>([CanBeNull] this T source, T defaultValue)
            where T : class
            => source ?? defaultValue;

        /// <summary>Checks if the value is present in the given array.</summary>
        /// <exception cref="ArgumentNullException">The values can not be null.</exception>
        /// <param name="value">The value to search for.</param>
        /// <param name="values">A IEnumerable containing the values.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>Returns true if the value is present in the array.</returns>
        [Pure]
        [PublicAPI]
        public static bool IsIn<T>([CanBeNull] this T value, [NotNull] params T[] values)
        {
            values.ThrowIfNull(nameof(values));

            return values.Contains(value);
        }

        /// <summary>Checks if the value is present in the given IEnumerable.</summary>
        /// <exception cref="ArgumentNullException">The values can not be null.</exception>
        /// <param name="value">The value to search for.</param>
        /// <param name="values">A IEnumerable containing the values.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>Returns true if the value is present in the IEnumerable.</returns>
        [Pure]
        [PublicAPI]
        public static bool IsIn<T>([CanBeNull] this T value, [NotNull] IEnumerable<T> values)
        {
            values.ThrowIfNull(nameof(values));

            return values.Contains(value);
        }

        /// <summary>Checks if the value is not present in the given array.</summary>
        /// <exception cref="ArgumentNullException">Values can not be null.</exception>
        /// <param name="value">The value to search for.</param>
        /// <param name="values">A IEnumerable containing the values.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>&gt;Returns true if the value is not present in the array.</returns>
        [Pure]
        [PublicAPI]
        public static bool IsNotIn<T>([CanBeNull] this T value, [NotNull] params T[] values)
            => !IsIn(value, values);

        /// <summary>Checks if the value is not present in the given IEnumerable.</summary>
        /// <exception cref="ArgumentNullException">Values can not be null.</exception>
        /// <param name="value">The value to search for.</param>
        /// <param name="values">A IEnumerable containing the values.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>&gt;Returns true if the value is not present in the IEnumerable.</returns>
        [Pure]
        [PublicAPI]
        public static bool IsNotIn<T>([CanBeNull] this T value, [NotNull] IEnumerable<T> values)
            => !IsIn(value, values);

        /// <summary>Determines whether [is not null].</summary>
        /// <typeparam name="T">Type de l'objet</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="bool" />.</returns>
        [DebuggerStepThrough]
        public static bool IsNotNull<T>([CanBeNull] this T source)
            where T : class
            => source != null;

        /// <summary>The is null. </summary>
        /// <param name="source">The source. </param>
        /// <typeparam name="T">Le type à tester </typeparam>
        /// <returns>The <see cref="bool" />. </returns>
        [DebuggerStepThrough]
        public static bool IsNull<T>([CanBeNull] this T source)
            where T : class
            => source == null;

        /// <summary>Swaps the given values.</summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="obj">An object to cal the extension method on, can be null.</param>
        /// <param name="value0">The first value.</param>
        /// <param name="value1">The second value.</param>
        [PublicAPI]
        public static void Swap<T>([CanBeNull] this object obj, ref T value0, ref T value1)
        {
            var temp = value0;
            value0 = value1;
            value1 = temp;
        }

        /// <summary>Throws a <see cref="ArgumentNullException" /> exception if <paramref name="obj" /> is null.</summary>
        /// <remarks>
        ///     If <paramref name="errorMessage" /> is null, this method will use the following default message:
        ///     "{object name} can not be null."
        /// </remarks>
        /// <typeparam name="TObject">The type <paramref name="obj" />.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <param name="parameterName">The name of <paramref name="obj" />.</param>
        /// <param name="errorMessage">The text used as exception message if <paramref name="obj" /> is null.</param>
        [PublicAPI]
        [DebuggerStepThrough]
        public static void ThrowIfNull<TObject>([NoEnumeration] [CanBeNull] this TObject obj, [NotNull] string parameterName, [CanBeNull] string errorMessage = null)
        {
            if (obj != null)
            {
                return;
            }

            throw new ArgumentNullException(parameterName, errorMessage ?? $"{parameterName} can not be null.");
        }

        /// <summary>The to.</summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">Type to convert</typeparam>
        /// <returns>The <see cref="T" />.</returns>
        public static T To<T>([CanBeNull] this IConvertible value)
        {
            try
            {
                var t = typeof(T);
                var u = Nullable.GetUnderlyingType(t);

                if (u != null)
                {
                    if (value == null || value.Equals(string.Empty))
                    {
                        return default;
                    }

                    return (T)Convert.ChangeType(value, u);
                }

                if (value == null || value.Equals(string.Empty))
                {
                    return default;
                }

                return (T)Convert.ChangeType(value, t);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>The to.</summary>
        /// <param name="value">The value.</param>
        /// <param name="ifError">The if error.</param>
        /// <typeparam name="T">Type to convert</typeparam>
        /// <returns>The <see cref="T" />.</returns>
        public static T To<T>([CanBeNull] this IConvertible value, IConvertible ifError)
        {
            try
            {
                var t = typeof(T);
                var u = Nullable.GetUnderlyingType(t);

                if (u != null)
                {
                    if (value == null || value.Equals(string.Empty))
                    {
                        return (T)ifError;
                    }

                    return (T)Convert.ChangeType(value, u);
                }

                if (value == null || value.Equals(string.Empty))
                {
                    return ifError.To<T>();
                }

                return (T)Convert.ChangeType(value, t);
            }
            catch
            {
                return (T)ifError;
            }
        }

        #endregion
    }
}
