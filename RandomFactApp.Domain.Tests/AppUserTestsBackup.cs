//using FluentAssertions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using RandomFactApp.Domain.Models;

//namespace RandomFactApp.Domain.Tests
//{
//    [TestClass]
//    public sealed class AppUserTests
//    {

//        [TestMethod]
//        public void AppUser_ShouldBeAllowedExplicitRandomFacts_WhenOldEnoughAccordingToCountry()
//        {
//            // Arrange
//            var birthDate = new DateTime(2000, 1, 1);
//            var currentDate = new DateTime(2018, 1, 1);
//            var user = new AppUser
//            {
//                DateOfBirth = birthDate,
//                Country = new Country
//                {
//                    IsoCode = "TEST",
//                    MinimumAgeForExplicitContent = 18
//                }
//            };


//            // Act
//            var isAllowedExplicitRandomFacts = user.IsAllowedExplicitRandomFacts(currentDate);

//            // Assert
//            isAllowedExplicitRandomFacts.Should().BeTrue();
//        }


//    }
//}
