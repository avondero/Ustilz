namespace Ustilz.Utils
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    using JetBrains.Annotations;

    #endregion

    /// <summary>The check.</summary>
    [DebuggerStepThrough]
    [PublicAPI]
    public static class Check
    {
        #region M�thodes publiques

        /// <summary>The has no nulls.</summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <typeparam name="T">Type de la valeur � tester</typeparam>
        /// <returns>The IReadOnlyList.</returns>
        [NotNull]
        public static IReadOnlyList<T> HasNoNulls<T>([NotNull] IReadOnlyList<T> value, [InvokerParameterName] [NotNull] string parameterName)
            where T : class
        {
            NotNull(value, parameterName);

            if (value.All(e => e != null))
            {
                return value;
            }

            NotEmpty(parameterName, nameof(parameterName));

            throw new ArgumentException(parameterName);
        }

        /// <summary>The not empty.</summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <typeparam name="T">Type de la valeur � tester</typeparam>
        /// <returns>The IReadOnlyList.</returns>
        [NotNull]
        [ContractAnnotation("value:null => halt")]
        public static IReadOnlyList<T> NotEmpty<T>(IReadOnlyList<T> value, [InvokerParameterName] [NotNull] string parameterName)
        {
            NotNull(value, parameterName);

            if (value.Count != 0)
            {
                return value;
            }

            NotEmpty(parameterName, nameof(parameterName));

            throw new ArgumentException(Strings.CollectionArgumentIsEmpty(parameterName));
        }

        /// <summary>The not empty.</summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <returns>The <see cref="string" />.</returns>
        [NotNull]
        [ContractAnnotation("value:null => halt")]
        public static string NotEmpty(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            Exception e = null;
            if (ReferenceEquals(value, null))
            {
                e = new ArgumentNullException(parameterName);
            }
            else if (value.Trim().Length == 0)
            {
                e = new ArgumentException(Strings.ArgumentIsEmpty(parameterName));
            }

            if (e == null)
            {
                return value;
            }

            NotEmpty(parameterName, nameof(parameterName));

            throw e;
        }

        /// <summary>The not null.</summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <typeparam name="T">Type de la valeur � tester</typeparam>
        /// <returns>The <see cref="T" />.</returns>
        [NotNull]
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>([NoEnumeration] T value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (!ReferenceEquals(value, null))
            {
                return value;
            }

            NotEmpty(parameterName, nameof(parameterName));

            throw new ArgumentNullException(parameterName);
        }

        /// <summary>The not null.</summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="propertyName">The property name.</param>
        /// <typeparam name="T">Type de la valeur � tester</typeparam>
        /// <returns>The <see cref="T" />.</returns>
        [NotNull]
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>([NoEnumeration] T value, [InvokerParameterName] [NotNull] string parameterName, [NotNull] string propertyName)
        {
            if (!ReferenceEquals(value, null))
            {
                return value;
            }

            NotEmpty(parameterName, nameof(parameterName));
            NotEmpty(propertyName, nameof(propertyName));

            throw new ArgumentException(Strings.ArgumentPropertyNull(propertyName, parameterName));
        }

        /// <summary>The null but not empty.</summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <returns>The <see cref="string" />.</returns>
        [CanBeNull]
        public static string NullButNotEmpty([CanBeNull] string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (ReferenceEquals(value, null) || value.Length != 0)
            {
                return value;
            }

            NotEmpty(parameterName, nameof(parameterName));

            throw new ArgumentException(Strings.ArgumentIsEmpty(parameterName));
        }

        /// <summary>The valid entity type.</summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <returns>The <see cref="Type" />.</returns>
        public static Type ValidEntityType(Type value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (value.GetTypeInfo().IsClass)
            {
                return value;
            }

            NotEmpty(parameterName, nameof(parameterName));

            throw new ArgumentException(Strings.InvalidEntityType(value, parameterName));
        }

        #endregion
    }
}