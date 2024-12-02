using Moq;
using FluentAssertions;
using RandomFactApp.Domain.Models;
using RandomFactApp.Domain.Clients;
using System.Linq.Expressions;

namespace RandomFactApp.ViewModels.Tests
{
    [TestClass]
    public class ApiSocketsViewModelTests
    {
        [TestMethod]
        public async Task ApiSocketsViewModel_DoesNotFetchRandomFact_WhenRandomFactIsAlreadyBeingFetchedAsync()
        {
            // Arrange
            var randomFactClient = new Mock<IRandomFactClient>();
            randomFactClient.Setup(x => x.GetRandomFactAsync())
                .ReturnsAsync(new RandomFact(), TimeSpan.FromMilliseconds(200));
            
            var sut = CreateApiSocketsViewModel(setupRandomFactClient: randomFactClient.Object);

            // Act
            await Task.WhenAll(sut.FetchRandomFact(), sut.FetchRandomFact());

            // Assert
            randomFactClient.Verify(x => x.GetRandomFactAsync(), Times.Once);
        }

        public static ApiSocketsViewModel CreateApiSocketsViewModel(IRandomFactClient? setupRandomFactClient = null, IWebSocketClient? setupWebsocketClient = null)
        {
            var randomFactClient = setupRandomFactClient ?? new Mock<IRandomFactClient>().Object;
            var websocketClient = setupWebsocketClient ?? new Mock<IWebSocketClient>().Object;

            return new ApiSocketsViewModel(randomFactClient, websocketClient);
        }

    }
}
