﻿namespace Ustilz.Extensions.Enumerables
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using JetBrains.Annotations;

    #endregion

    /// <summary>The extensions i enumerable string.</summary>
    public static class ExtensionsIEnumerableString
    {
        #region Méthodes publiques

        /// <summary>Returns a path combined out of the items in the given IEnumerable.</summary>
        /// <exception cref="ArgumentNullException">The enumerable can not be null.</exception>
        /// <param name="enumerable">The IEnumerable to act on.</param>
        /// <returns>The combined path.</returns>
        [PublicAPI]
        [Pure]
        [NotNull]
        public static string PathCombine([NotNull] [ItemCanBeNull] this IEnumerable<string> enumerable)
        {
            enumerable.ThrowIfNull(nameof(enumerable));

            return Path.Combine(enumerable.ToArray());
        }

        #endregion
    }
}