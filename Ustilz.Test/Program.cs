﻿namespace Ustilz.Test
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Ustilz.Arguments;
    using Ustilz.Extensions;
    using Ustilz.Programs;
    using Ustilz.Sql;
    using Ustilz.Sql.Enums;
    using Ustilz.Sql.RequestType;
    

    #endregion

    /// <summary>The program.</summary>
    internal class Program
    {
        public int T { get; set; } = 4;

        /// <summary>The main.</summary>
        /// <param name="args">The args.</param>
        private static void Main(string[] args)
        {

            var arguments = ArgumentsManager.Init<TestArgument>(args);

                //Programme.WithChoice(true, TestDump, TestSqlMaker, TestMethod);

            var checkArguments = arguments.CheckArguments();
        }

        public double Distance => Math.Sqrt(this.X * this.X + this.Y * this.Y);

        public int Y { get; set; }

        public int X { get; set; }

        /// <summary>The test sql maker.</summary>
        private static void TestSqlMaker()
        {
            var requetesList = new List<IRequest>();

            var sr = FactoryRequest.GetSelectRequest("TABLE_TEST", "tt");

            // TODO Retourner les mots clés en majuscule
            //// selectRequest.UpperSQL = true;
            sr.AddSelectColumn("colonne1");
            sr.AddSelectColumn("colonne2", "toto");
            sr.AddSelectColumn("colonne3");
            sr.AddSelectColumn("colonne4");

            var t1 = FactoryRequestElement.CreateTable("Table_1", "T1");
            var t2 = FactoryRequestElement.CreateTable("Table_2", "T2");
            var c = FactoryRequestElement.CreateColumn(t1, "ff");
            var c2 = FactoryRequestElement.CreateColumn(sr.PrincipalTable, "ff");
            /*
            Condition cond = new Condition(c, c2);
            sr.AddFirstWhereClause(cond);


            // sr.WhereClause.FirstCondition;
            
            Table t2 = new Table("Table_2", "T2");*/
            
            bool b = c == c2;

            sr.AddJoin(TypeJoin.InnerJoin, "T1colonne2", t1, "colonne2");
            sr.AddJoin(TypeJoin.InnerJoin, "T2colonne9", t2, "colonne2");

            requetesList.Add(sr);

            foreach (var request in requetesList)
            {
                request.ToSql().DumpConsole();
            }

            var button = new Button
            {
                Text = "Say hello"
            };
            button.Click += (sender, e) => MessageBox.Show("Hello world!");

        }

        /// <summary>The test dump.</summary>
        private static void TestDump()
        {
            string[] ss = { "1", "3", "TT" };
            ss.DumpConsole();

            var vv = new List<Version> { new Version("8.8.9.8"), new Version("9.0.7.6") };
            vv.DumpConsole();
        }

        private static void TestMethod()
        {
            GetValue(null, 10, "");
        }

        private static void GetValue(object toto, int i, string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            var listeElement = new List<string>(9);

            ArgumentsManager.Check(nameof(s)).NotNullOrEmpty(s);
            ArgumentsManager.Check(nameof(i)).LessThan(i, listeElement.Count);
            ArgumentsManager.Check(nameof(toto)).NotNull(toto);

        }
    }
}