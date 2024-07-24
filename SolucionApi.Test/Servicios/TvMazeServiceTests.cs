using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using global::SolucionApi.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolucionApi.ApiExternas.TvMazeApi;
using SolucionApi.ApiExternas.TvMazeApi.Models;


namespace SolucionApi.Test.Servicios;


[TestClass]
public class TvMazeServiceTests
{
    private Mock<IShowRepository> _mockShowRepository;
    private TvMazeService _tvMazeService;

    [TestInitialize]
    public void Setup()
    {
        _mockShowRepository = new Mock<IShowRepository>();
        _tvMazeService = new TvMazeService(_mockShowRepository.Object);
    }

    [TestMethod]
    public async Task GetShowsAsync_ReturnsListOfShowDtos()
    {
        // Arrange
        var shows = new List<Show>
        {
            new Show { Id = 1, Name = "Show 1" },
            new Show { Id = 2, Name = "Show 2" }
        };

        _mockShowRepository.Setup(repo => repo.GetShowsAsync()).ReturnsAsync(shows);

        // Act
        var result = await _tvMazeService.GetShowsAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(shows.Count, result.Count);
        Assert.AreEqual(shows[0].Name, result[0].Name);
    }

    [TestMethod]
    public async Task GetShowAsync_ReturnsShowDto()
    {
        // Arrange
        var show = new Show { Id = 1, Name = "Show 1" };

        _mockShowRepository.Setup(repo => repo.GetShowAsync(It.IsAny<int>())).ReturnsAsync(show);

        // Act
        var result = await _tvMazeService.GetShowAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(show.Name, result.Name);
    }

    [TestMethod]
    public async Task GetShowAsync_ReturnsNullWhenShowNotFound()
    {
        // Arrange
        _mockShowRepository.Setup(repo => repo.GetShowAsync(It.IsAny<int>())).ReturnsAsync((Show)null);

        // Act
        var result = await _tvMazeService.GetShowAsync(1);

        // Assert
        Assert.IsNull(result);
    }
}