using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomFactApp.Domain.Models;
using System.Linq.Expressions;

namespace RandomFactApp.Domain.Tests
{
    [TestClass]
    public sealed class AppUserTests
    {
        //[TestMethod]
        //public void AppUser_UnderTheAgeOf5_IsAToddler()
        //{
        //    // Arrange
        //    var today = new DateTime(2024, 1, 1);
        //    var birthDate = new DateTime(2022, 1, 1);
        //    var user = new AppUser
        //    {
        //        Country = new Country { IsoCode = "NL" },
        //        DateOfBirth = birthDate,
        //        Name = "Kleuter"
        //    };

        //    // Act
        //    var isToddler = user.IsToddler(today);

        //    // Assert
        //    isToddler.Should().BeTrue();
        //}

        [TestMethod]
        public void AppUser_ShouldBeAllowedExplicitRandomFacts_WhenOldEnoughAccordingToCountry()
        {
            // Arrange
            var birthDate = new DateTime(2000, 1, 1);
            var currentDate = new DateTime(2018, 1, 1);
            var user = new AppUser
            {
                DateOfBirth = birthDate,
                Country = new Country // 👈 We're not testing the minimum age in The Netherlands is 18, so we inject fake data and check if the user adheres to it
                {
                    IsoCode = "TEST",
                    MinimumAgeForExplicitContent = 18
                }
            };

            // Act
            var isAllowedExplicitRandomFacts = user.IsAllowedExplicitRandomFacts(currentDate);

            // Assert
            isAllowedExplicitRandomFacts.Should().BeTrue();
        }
    }
}