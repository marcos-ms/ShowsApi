using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolucionApi.Data.Entities;
using SolucionApi.Data.Repositories.Contracts;
using SolucionApi.Dtos;
using SolucionApi.Services;
using SolucionApi.Services.Implementations;
using SolucionApi.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolucionApi.Test.Servicios
{
    [TestClass]
    public class DataBaseServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IShowRepository> _mockShowRepository;
        private Mock<INetworkRepository> _mockNetworkRepository;
        private Mock<IWebChannelRepository> _mockWebChannelRepository;
        private DataBaseService _dataBaseService;

        [TestInitialize]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockShowRepository = new Mock<IShowRepository>();
            _mockNetworkRepository = new Mock<INetworkRepository>();
            _mockWebChannelRepository = new Mock<IWebChannelRepository>();

            _mockUnitOfWork.Setup(uow => uow.ShowRepository).Returns(_mockShowRepository.Object);
            _mockUnitOfWork.Setup(uow => uow.NetworkRepository).Returns(_mockNetworkRepository.Object);
            _mockUnitOfWork.Setup(uow => uow.WebChannelRepository).Returns(_mockWebChannelRepository.Object);

            _dataBaseService = new DataBaseService(_mockUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetShowsAsync_ReturnsListOfShowDtos()
        {
            // Arrange
            var showEntities = new List<ShowEntity>
            {
                new ShowEntity { Id = "1", Name = "Show 1" },
                new ShowEntity { Id = "2", Name = "Show 2" }
            };

            var mockDbSet = showEntities.CreateDbSetMock();
            _mockShowRepository.Setup(repo => repo.GetAll()).Returns(mockDbSet.Object);

            // Act
            var result = await _dataBaseService.GetShowsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(showEntities.Count, result.Count);
            Assert.AreEqual(showEntities[0].Name, result[0].Name);
        }

        [TestMethod]
        public async Task GetShowAsync_ReturnsShowDto()
        {
            // Arrange
            var showEntity = new ShowEntity { Id = "1", Name = "Show 1" };

            _mockShowRepository.Setup(repo => repo.GetById(It.IsAny<string>()))
                .ReturnsAsync(showEntity);

            // Act
            var result = await _dataBaseService.GetShowAsync("1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(showEntity.Name, result.Name);
        }

        [TestMethod]
        public async Task GetShowAsync_ReturnsNullWhenShowNotFound()
        {
            // Arrange
            _mockShowRepository.Setup(repo => repo.GetById(It.IsAny<string>()))
                .ReturnsAsync((ShowEntity)null);

            // Act
            var result = await _dataBaseService.GetShowAsync("1");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task SaveShowAsync_InsertsNewShow()
        {
            // Arrange
            var showDto = new ShowDto { Id = "1", Name = "Show 1" };
            var showEntity = showDto.ToEntity();

            _mockShowRepository.Setup(repo => repo.Exist(It.IsAny<string>()))
                .ReturnsAsync(false);
            _mockShowRepository.Setup(repo => repo.Insert(It.IsAny<ShowEntity>()))
                .ReturnsAsync(showEntity);

            // Act
            var result = await _dataBaseService.SaveShowAsync(showDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(ServicesConst.ShowSaved, result.Message);
            _mockShowRepository.Verify(repo => repo.Insert(It.IsAny<ShowEntity>()), Times.Once);
        }

        [TestMethod]
        public async Task SaveShowAsync_UpdatesExistingShow()
        {
            // Arrange
            var showDto = new ShowDto { Id = "1", Name = "Show 1" };
            var showEntity = showDto.ToEntity();

            _mockShowRepository.Setup(repo => repo.Exist(It.IsAny<string>()))
                .ReturnsAsync(true);
            _mockShowRepository.Setup(repo => repo.Update(It.IsAny<ShowEntity>()))
                .Returns(showEntity);

            // Act
            var result = await _dataBaseService.SaveShowAsync(showDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(ServicesConst.ShowUpdated, result.Message);
            _mockShowRepository.Verify(repo => repo.Update(It.IsAny<ShowEntity>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteShowAsync_RemovesShow()
        {
            // Arrange
            var showId = "1";

            _mockShowRepository.Setup(repo => repo.Exist(It.IsAny<string>()))
                .ReturnsAsync(true);
            _mockShowRepository.Setup(repo => repo.Remove(It.IsAny<string>()));

            // Act
            var result = await _dataBaseService.DeleteShowAsync(showId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(ServicesConst.ShowDeleted, result.Message);
            _mockShowRepository.Verify(repo => repo.Remove(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteShowAsync_ReturnsErrorWhenShowNotFound()
        {
            // Arrange
            var showId = "1";

            _mockShowRepository.Setup(repo => repo.Exist(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _dataBaseService.DeleteShowAsync(showId);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ServicesConst.ErrorNotFound, result.Message);
        }
    }
}
