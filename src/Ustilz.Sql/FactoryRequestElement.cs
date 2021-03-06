﻿namespace Ustilz.Sql
{
    #region Usings

    using JetBrains.Annotations;

    using Ustilz.Sql.RequestElement;
    using Ustilz.Sql.RequestElement.Implementations;

    #endregion

    /// <summary>The factory request element.</summary>
    [PublicAPI]
    public static class FactoryRequestElement
    {
        #region Méthodes publiques

        /// <summary>The create column.</summary>
        /// <param name="table">The nom.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>The <see cref="IColumn" />.</returns>
        [NotNull]
        public static IColumn CreateColumn(ITable table, string alias) => new Column(table, alias);

        /// <summary>The create table.</summary>
        /// <param name="nom">The nom.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>The <see cref="ITable" />.</returns>
        [NotNull]
        public static ITable CreateTable(string nom, string alias) => new Table(nom, alias);

        #endregion
    }
}
