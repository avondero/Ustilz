namespace Ustilz.Sql.RequestElement
{
    #region Usings

    using JetBrains.Annotations;

    using Ustilz.Sql.Enums;

    #endregion

    /// <summary>The Join interface.</summary>
    [PublicAPI]
    public interface IJoin
    {
        #region Propri�t�s et indexeurs

        /// <summary>Gets the join table. </summary>
        /// <value>The join table.</value>
        [NotNull]
        ITable JoinTable { get; }

        /// <summary>Gets the right column. </summary>
        /// <value>The other column.</value>
        [NotNull]
        IColumn OtherColumn { get; }

        /// <summary>Gets the left column. </summary>
        /// <value>The root column.</value>
        [NotNull]
        IColumn RootColumn { get; }

        /// <summary>Gets the type. </summary>
        /// <value>The type.</value>
        TypeJoin Type { get; }

        #endregion

        #region M�thodes publiques

        /// <summary>Retourne un <see cref="T:System.String" /> qui repr�sente le <see cref="T:System.Object" /> actuel. </summary>
        /// <returns><see cref="T:System.String" /> qui repr�sente le <see cref="T:System.Object" /> actuel.</returns>
        string ToString();

        #endregion
    }
}
