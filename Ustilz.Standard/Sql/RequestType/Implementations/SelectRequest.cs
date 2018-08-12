namespace Ustilz.Sql.RequestType.Implementations
{
    #region Usings

    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using JetBrains.Annotations;

    using Ustilz.Sql.Enums;
    using Ustilz.Sql.RequestElement;
    using Ustilz.Sql.RequestElement.Implementations;

    #endregion

    /// <summary>The select request. </summary>
    [PublicAPI]
    internal sealed class SelectRequest : ISelectRequest
    {
        #region Champs

        /// <summary>Gets the join. </summary>
        [NotNull] private readonly Joins joins;

        /// <summary>The liste columns. </summary>
        [NotNull] private readonly List<string> listeColumns;

        /// <summary>The liste columns. </summary>
        [NotNull] private readonly IColumns selectColumns;

        /// <summary>The where clause. </summary>
        [CanBeNull] private WhereClause whereClause;

        #endregion

        #region Constructeurs et destructeurs

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectRequest" /> class. Initialise une nouvelle instance de la
        ///     classe <see cref="SelectRequest" />.
        /// </summary>
        /// <param name="nomTable">The nomTable.</param>
        internal SelectRequest(string nomTable)
            : this()
        {
            this.listeColumns = new List<string>();
            this.PrincipalTable = new Table(nomTable);
            this.selectColumns = new Columns(this.PrincipalTable);
            this.joins = new Joins();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectRequest" /> class. Initialise une nouvelle instance de la
        ///     classe <see cref="SelectRequest" />.
        /// </summary>
        /// <param name="nomTable">The nom table.</param>
        /// <param name="alias">The alias.</param>
        internal SelectRequest(string nomTable, string alias)
            : this(nomTable) =>
            this.PrincipalTable = new Table(nomTable, alias);

        /// <summary>Prevents a default instance of the <see cref="SelectRequest" /> class from being created.</summary>
        private SelectRequest()
        {
        }

        #endregion

        #region Propriétés et indexeurs

        /// <summary>Gets the principal table.</summary>
        /// <value>The principal table.</value>
        [NotNull]
        public ITable PrincipalTable { get; private set; }

        /// <summary>Gets or sets a value indicating whether upper sql. </summary>
        /// TODO Mots clé en majuscule
        /// <value>The upper sql.</value>
        public bool UpperSQL { get; set; }

        /// <summary>Gets the where clause.</summary>
        /// <value>The where clause.</value>
        public IWhereClause WhereClause => this.whereClause;

        #endregion

        #region Méthodes publiques

        /// <summary>The add join.</summary>
        /// <param name="typeJoin">The type join.</param>
        /// <param name="otherColumn">The other column.</param>
        /// <param name="tableJoin">The table join.</param>
        /// <param name="rootColumn">The root column.</param>
        public void AddJoin(TypeJoin typeJoin, string otherColumn, ITable tableJoin, string rootColumn)
        {
            var jointureT1 = new Join(typeJoin, tableJoin, new Column(this.PrincipalTable, otherColumn), new Column(tableJoin, rootColumn));
            this.joins.Add(jointureT1);
        }

        /// <summary>The add columns.</summary>
        /// <param name="col">The col.</param>
        public void AddSelectColumn(string col) => this.selectColumns.Add(col);

        /// <summary>The add select column.</summary>
        /// <param name="col">The col.</param>
        /// <param name="alias">The alias.</param>
        public void AddSelectColumn(string col, string alias) => this.selectColumns.Add(col, alias);

        /// <summary>The add first where clause.</summary>
        /// <param name="condition">The condition.</param>
        public void AddWhereClause(ICondition condition)
        {
            if (this.whereClause == null)
            {
                this.whereClause = new WhereClause();
            }

            this.whereClause.AddCondition(condition);
        }

        /// <summary>The to sql. </summary>
        /// <returns>The <see cref="string" />. </returns>
        [Pure]
        public string ToSql()
        {
            var sql = new StringBuilder();
            sql.Append(Constantes.SQL.Keyword.Select);
            sql.Append(Constantes.Space);
            if (!this.HasColumnsSpecified())
            {
                sql.Append(Constantes.SQL.AllColumns);
                sql.Append(Constantes.Space);
            }
            else
            {
                sql.Append(string.Join(Constantes.VirguleSpace, this.listeColumns.ToArray()));
                sql.Append(string.Join(Constantes.VirguleSpace, this.selectColumns.ToStringArray()));
            }

            sql.Append(Constantes.Space);
            sql.Append(Constantes.SQL.Keyword.From);

            sql.Append(Constantes.Space);
            sql.Append(this.PrincipalTable);

            if (this.joins.Any())
            {
                sql.Append(this.joins);
            }

            if (this.whereClause != null)
            {
                sql.Append(this.whereClause);
            }

            return sql.ToString();
        }

        /// <summary>The to string. </summary>
        /// <returns>The <see cref="string" />. </returns>
        public override string ToString() => this.ToSql();

        #endregion

        #region Méthodes privées

        /// <summary>The has columns specified. </summary>
        /// <returns>The <see cref="bool" />. </returns>
        private bool HasColumnsSpecified() => this.selectColumns.Any();

        #endregion
    }
}
