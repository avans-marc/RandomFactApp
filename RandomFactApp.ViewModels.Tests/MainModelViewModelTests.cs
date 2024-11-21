using Moq;
using FluentAssertions;
using RandomFactApp.Domain.Models;
using RandomFactApp.Domain.Clients;

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


            var sut = new MainPageViewModel(randomFactClient.Object);

            // Act
            await Task.WhenAll(sut.FetchRandomFact(), sut.FetchRandomFact());

            // Assert
            randomFactClient.Verify(x => x.GetRandomFactAsync(), Times.Once);
         }

     }
}
