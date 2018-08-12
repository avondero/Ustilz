namespace Ustilz.Sql.RequestElement.Implementations
{
    #region Usings

    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using JetBrains.Annotations;

    #endregion

    /// <summary>The where clause. </summary>
    [PublicAPI]
    internal sealed class WhereClause : IWhereClause
    {
        public WhereClause() => this.Conditions = new List<IWhereCondition>();

        #region Propriétés et indexeurs

        /// <summary>Gets the first condition.</summary>
        /// <value>The first condition.</value>
        public List<IWhereCondition> Conditions { get; private set; }

        #endregion

        #region Méthodes publiques

        /// <summary>The add condition.</summary>
        /// <param name="condition">The condition.</param>
        public void AddCondition(ICondition condition) => this.Conditions.Add(new WhereCondition(condition));

        /// <summary>Retourne un <see cref="T:System.String" /> qui représente le <see cref="T:System.Object" /> actuel. </summary>
        /// <returns><see cref="T:System.String" /> qui représente le <see cref="T:System.Object" /> actuel.</returns>
        public override string ToString()
        {
            var whereBuilder = new StringBuilder(Constantes.Space);
            whereBuilder.Append(Constantes.SQL.Keyword.Conditions.Where);
            whereBuilder.Append(this.Conditions);
            return whereBuilder.ToString();
        }

        #endregion
    }
}
