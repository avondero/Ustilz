namespace Ustilz.Extensions.Models
{
    #region Usings

    using System;

    #endregion

    /// <summary>Le résultat de l'exécution.</summary>
    /// <typeparam name="T">Type du résultat.</typeparam>
    internal sealed class ExecutionResult<T> : IExecutionResult<T>
    {
        #region Propriétés et indexeurs

        /// <summary>Obtient ou définit l'exception.</summary>
        /// <value>The exception.</value>
        public Exception Exception { get; set; }

        /// <summary>Obtient ou définit le résultat.</summary>
        /// <value>The result.</value>
        public T Result { get; set; }

        #endregion
    }
}
