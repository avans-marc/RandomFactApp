using Moq;
using RandomFactApp.Domain.Models;
using RandomFactApp.Domain.Services;

namespace RandomFactApp.ViewModels.Tests
{
    [TestClass]
    public class MainModelViewModelTests
    {
        [TestMethod]
        public async Task MainPageViewModel_CannotFetchRandomFact_WhenRandomFactIsAlreadyBeingFetchedAsync()
        {
            // Arrange
            var randomFactClient = new Moq.Mock<IRandomFactClient>();

            randomFactClient.Setup(x => x.GetRandomFactAsync())
                            .ReturnsAsync(new RandomFact(), TimeSpan.FromMilliseconds(200));


            var sut = new MainPageViewModel(randomFactClient.Object);

            // Act
            sut.FetchRandomFact();

            // Assert
            Assert.IsFalse(sut.CanFetchRandomFact());
        }

        [TestMethod]
        public async Task MainPageViewModel_DoesNotFetchRandomFact_WhenRandomFactIsAlreadyBeingFetchedAsync()
        {
            // Arrange
            var randomFactClient = new Moq.Mock<IRandomFactClient>();

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
