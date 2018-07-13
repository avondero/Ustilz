﻿namespace Ustilz.Test.Extensions
{
    #region Usings

    using System;
    using Ustilz.Extensions;
    using Xunit;

    #endregion

    /// <summary>The extensions int 32.</summary>
    public partial class ExtensionsInt32Test
    {
        #region Méthodes publiques

        /// <summary>The ticks test.</summary>
        [Fact]
        public void TicksTest() => Assert.Equal(TimeSpan.FromTicks(5), 5.Ticks());

        /// <summary>The milliseconds.</summary>
        [Fact]
        public void MillisecondsTest() => Assert.Equal(TimeSpan.FromMilliseconds(5), 5.Milliseconds());

        /// <summary>The seconds.</summary>
        [Fact]
        public void SecondsTest() => Assert.Equal(TimeSpan.FromSeconds(5), 5.Seconds());

        /// <summary>The minutes.</summary>
        [Fact]
        public void MinutesTest() => Assert.Equal(TimeSpan.FromMinutes(5), 5.Minutes());

        /// <summary>The hours.</summary>
        [Fact]
        public void HoursTest() => Assert.Equal(TimeSpan.FromHours(5), 5.Hours());

        /// <summary>The days.</summary>
        [Fact]
        public void DaysTest() => Assert.Equal(TimeSpan.FromDays(5), 5.Days());

        #endregion
    }
}