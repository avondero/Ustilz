﻿namespace Ustilz.UI.Test
{
    #region Usings

    using Xunit;

    #endregion

    /// <summary>The color utils test.</summary>
    public sealed class ColorUtilsTest
    {
        #region Méthodes publiques

        /// <summary>The generer couleurs test.</summary>
        [Fact]
        public void GenererCouleursTest()
        {
            var couleur = ColorUtils.GenerateColor();
            Assert.Equal(6, couleur.Length);
        }

        /// <summary>The get color from nom test nominal.</summary>
        /// <param name="nom">The nom.</param>
        [Theory]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("FFDGZZ")]
        [InlineData("dfDFe")]
        public void GetColorFromNomNominalTest(string nom)
        {
            var colorFromNom = ColorUtils.GetColorFromNom(nom);
            Assert.Equal(7, colorFromNom.Length);
        }

        [Fact]
        public void GetColorFromNomTest()
        {
            var colorFromNom = ColorUtils.GetColorFromNom("Test");
            Assert.Equal("#6573C8", colorFromNom);
        }

        [Fact]
        public void GetColorFromNomClairTest()
        {
            var colorFromNom = ColorUtils.GetColorFromNom("Test", true);
            Assert.Equal("#E5F3E8", colorFromNom);
        }

        #endregion
    }
}
