using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SolucionApi.ApiExternas.TvMazeApi;
using SolucionApi.ApiExternas.TvMazeApi.Models;

namespace SolucionApi.Test.ApiExternas.TvMazeApi
{
    [TestClass]
    public class ShowRepositoryTests
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private ShowRepository _showRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            var httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://api.tvmaze.com")
            };

            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _showRepository = new ShowRepository(_mockHttpClientFactory.Object);
        }

        [TestMethod]
        public async Task GetShowsAsync_ReturnsListOfShows()
        {
            // Arrange
            var expectedShows = new List<Show>
            {
                new Show { Id = 1, Name = "Show 1" },
                new Show { Id = 2, Name = "Show 2" }
            };

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(expectedShows))
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _showRepository.GetShowsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedShows.Count, result.Count);
            Assert.AreEqual(expectedShows[0].Name, result[0].Name);
        }

        [TestMethod]
        public async Task GetShowAsync_ReturnsShow()
        {
            // Arrange
            var expectedShow = new Show { Id = 1, Name = "Show 1" };

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(expectedShow))
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _showRepository.GetShowAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedShow.Name, result.Name);
        }

        [TestMethod]
        public async Task GetShowAsync_ThrowsExceptionOnHttpError()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await _showRepository.GetShowAsync(1));
        }
    }
}
