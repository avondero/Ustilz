namespace Ustilz.Utils
{
    #region Usings

    using System;

    using JetBrains.Annotations;

    #endregion

    /// <summary>
    /// Classe d'aide pour la lévée d'exception.
    /// </summary>
    [PublicAPI]
    public class ThrowHelper
    {
        /// <summary>
        /// Lève une exeption <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="argument">Nom de l'argument.</param>
        public static void ThrowArgumentNullException(string argument) => throw new ArgumentNullException(argument);

        /// <summary>
        /// Lève une exeption <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="argument">Nom de l'argument.</param>
        public static void ThrowArgumentOutOfRangeException(string argument) => throw new ArgumentOutOfRangeException(argument);

        /// <summary>
        /// Lève une exeption <see cref="InvalidOperationException"/> avec un message "Plus d'un élément trouvé".
        /// </summary>
        public static void ThrowMoreThanOneElementException() => throw new InvalidOperationException(SR.MoreThanOneElement);

        /// <summary>
        /// Lève une exeption <see cref="InvalidOperationException"/> avec un message "Plus d'un élément correspond".
        /// </summary>
        public static void ThrowMoreThanOneMatchException() => throw new InvalidOperationException(SR.MoreThanOneMatch);

        /// <summary>
        /// Lève une exeption <see cref="InvalidOperationException"/> avec un message "Aucun élément trouvé".
        /// </summary>
        public static void ThrowNoElementsException() => throw new InvalidOperationException(SR.NoElements);

        /// <summary>
        /// Lève une exeption <see cref="InvalidOperationException"/> avec un message "Aucun élément ne correspond".
        /// </summary>
        public static void ThrowNoMatchException() => throw new InvalidOperationException(SR.NoMatch);

        /// <summary>
        /// Lève une exeption <see cref="NotSupportedException"/>.
        /// </summary>
        public static void ThrowNotSupportedException() => throw new NotSupportedException();
    }
}
