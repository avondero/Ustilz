namespace Ustilz.TestExe
{
    #region Usings

    using System;

    using Ustilz.Sql;

    #endregion

    internal class Program
    {
        #region Méthodes privées

        private static void Main(string[] args)
        {
            FactoryRequest.GetSelectRequest("Table1", "t").WhereClause
        }

        #endregion
    }
}
