using Moq;
using FluentAssertions;
using RandomFactApp.Domain.Models;
using RandomFactApp.Domain.Clients;
using System.Linq.Expressions;

namespace RandomFactApp.ViewModels.Tests
{
    [TestClass]
    public class MainModelViewModelTests
    {
        [TestMethod]
        public async Task MainPageViewModel_DoesNotFetchRandomFact_WhenRandomFactIsAlreadyBeingFetchedAsync()
        {
            // Arrange
            var randomFactClient = new Mock<IRandomFactClient>();
            randomFactClient.Setup(x => x.GetRandomFactAsync())
                .ReturnsAsync(new RandomFact(), TimeSpan.FromMilliseconds(200));
            
            var sut = CreateMainPageViewModel(setupRandomFactClient: randomFactClient.Object);

            // Act
            await Task.WhenAll(sut.FetchRandomFact(), sut.FetchRandomFact());

            // Assert
            randomFactClient.Verify(x => x.GetRandomFactAsync(), Times.Once);
        }

        [TestMethod]
        public async Task MainPageViewModel_SetsRandomFact_IfGetLastKnownLocationFails()
        {
            // Arrange
            var random = CreateRandomRandomFact();
            var randomFactClient = new Mock<IRandomFactClient>();
            randomFactClient.Setup(x => x.GetRandomFactAsync())
                            .ReturnsAsync(random, TimeSpan.FromMilliseconds(200));

            var geolocation = new Mock<IGeolocation>();
            geolocation.Setup(x => x.GetLastKnownLocationAsync()).Throws<Exception>();

            var sut = CreateMainPageViewModel(setupRandomFactClient: randomFactClient.Object, setupGeolocation: geolocation.Object);

            // Act
            await sut.FetchRandomFact();

            // Assert
            sut.RandomFact.Should().Be(random.Text);
        }

        private static RandomFact CreateRandomRandomFact()
        {
            return new RandomFact { Text = Guid.NewGuid().ToString(), Location = new GpsLocation(0, 0, "City") };
        }

        private static MainPageViewModel CreateMainPageViewModel(IRandomFactClient? setupRandomFactClient = null, IGeolocation? setupGeolocation = null, IWebSocketClient? setupWebsocketClient = null)
        {
            var randomFactClient = setupRandomFactClient ?? new Mock<IRandomFactClient>().Object;
            var geolocation = setupGeolocation ?? new Mock<IGeolocation>().Object;
            var websocketClient = setupWebsocketClient ?? new Mock<IWebSocketClient>().Object;

            return new MainPageViewModel(randomFactClient, websocketClient, geolocation);
        }
    }
}
