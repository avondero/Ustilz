namespace Ustilz.Sql.RequestType
{
    #region Usings

    using Ustilz.Sql.Enums;
    using Ustilz.Sql.RequestElement;

    #endregion

    /// <summary>The SelectRequest interface. </summary>
    public interface ISelectRequest : IRequest
    {
        #region Propriétés et indexeurs

        /// <summary>Gets the table. </summary>
        /// <value>The principal table.</value>
        ITable PrincipalTable { get; }

        /// <summary>Gets the where clause.</summary>
        /// <value>The where clause.</value>
        IWhereClause WhereClause { get; }

        #endregion

        #region Méthodes publiques

        /// <summary>The add first where clause.</summary>
        /// <param name="condition">The condition.</param>
        void AddWhereClause(ICondition condition);

        /// <summary>The add join.</summary>
        /// <param name="typeJoin">The type join.</param>
        /// <param name="otherColumn">The other column.</param>
        /// <param name="tableJoin">The table join.</param>
        /// <param name="rootColumn">The root column.</param>
        void AddJoin(TypeJoin typeJoin, string otherColumn, ITable tableJoin, string rootColumn);

        /// <summary>The add select column.</summary>
        /// <param name="col">The col.</param>
        void AddSelectColumn(string col);

        /// <summary>The add select column.</summary>
        /// <param name="col">The col.</param>
        /// <param name="alias">The alias.</param>
        void AddSelectColumn(string col, string alias);

        #endregion
    }
}
