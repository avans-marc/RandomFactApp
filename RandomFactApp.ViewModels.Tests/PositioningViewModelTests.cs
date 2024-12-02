using Moq;
using FluentAssertions;
using RandomFactApp.Domain.Models;
using RandomFactApp.Domain.Clients;
using System.Linq.Expressions;
using Microsoft.Maui.Maps;

namespace RandomFactApp.ViewModels.Tests
{
    [TestClass]
    public class PositioningViewModelTests
    {
        [TestMethod]
        public async Task PositioningViewModel_SetsCurrentMappedRandomFact_IfGetLastKnownLocationFails()
        {
            // Arrange
            var random = TestHelper.CreateRandomRandomFact();
            var randomFactClient = new Mock<IRandomFactClient>();
            randomFactClient.Setup(x => x.GetRandomFactAsync())
                            .ReturnsAsync(random, TimeSpan.FromMilliseconds(200));

            var geolocation = new Mock<IGeolocation>();
            geolocation.Setup(x => x.GetLastKnownLocationAsync()).Throws<Exception>();

            var sut = CreatePositioningViewModel(setupRandomFactClient: randomFactClient.Object, setupGeolocation: geolocation.Object);

            // Act
            await sut.FetchRandomFact();

            // Assert
            sut.CurrentMappedRandomFact.Should().NotBeNull();
        }

        [TestMethod]
        public async Task PositioningViewModel_UpdatesCurrentMapSpan_WhenLocationIsChanged()
        {
            // Arrange
            var location = new Location(51.00, 41.00);
            var geolocation = new Mock<IGeolocation>();
            var sut = CreatePositioningViewModel(setupGeolocation: geolocation.Object);

            // Act
            geolocation.Raise(x => x.LocationChanged += null, new GeolocationLocationChangedEventArgs(location));

            // Assert
            sut.CurrentMapSpan.Should().Match<MapSpan>(x => x.Center == location);
        }


        private static PositioningViewModel CreatePositioningViewModel(IRandomFactClient? setupRandomFactClient = null, IGeolocation? setupGeolocation = null)
        {
            var randomFactClient = setupRandomFactClient ?? new Mock<IRandomFactClient>().Object;
            var geolocation = setupGeolocation ?? new Mock<IGeolocation>().Object;

            return new PositioningViewModel(randomFactClient, geolocation);
        }
    }
}
