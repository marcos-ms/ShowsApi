using Microsoft.EntityFrameworkCore;
using SolucionApi.Data;
using SolucionApi.Data.Repositories.Contracts;
using SolucionApi.Data.Repositories.Implementations;
using Moq;
using SolucionApi.Data.Entities;

namespace SolucionApi.Test.Repositories;

[TestClass]
public class UnitOfWorkTests
{
    private DataBaseApiDbContext _dbContext;
    private IShowRepository _showRepository;
    private INetworkRepository _networkRepository;
    private IWebChannelRepository _webChannelRepository;
    private UnitOfWork _unitOfWork;

    [TestInitialize]
    public void Setup()
    {
        // Configurar la base de datos en memoria
        var options = new DbContextOptionsBuilder<DataBaseApiDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Crear una instancia del DbContext con la base de datos en memoria
        _dbContext = new DataBaseApiDbContext(options);

        // Crear instancias reales de los repositorios
        _showRepository = new ShowRepository(_dbContext);
        _networkRepository = new NetworkRepository(_dbContext);
        _webChannelRepository = new WebChannelRepository(_dbContext);

        // Crear una instancia de UnitOfWork con los repositorios reales
        _unitOfWork = new UnitOfWork(
            _dbContext,
            _showRepository,
            _networkRepository,
            _webChannelRepository
        );
    }

    [TestCleanup]
    public void TearDown()
    {
        // Limpiar la base de datos después de cada prueba
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [TestMethod]
    public void ShowRepository_ReturnsInjectedInstance()
    {
        // Act
        var showRepo = _unitOfWork.ShowRepository;

        // Assert
        Assert.AreEqual(_showRepository, showRepo);
    }

    [TestMethod]
    public void NetworkRepository_ReturnsInjectedInstance()
    {
        // Act
        var networkRepo = _unitOfWork.NetworkRepository;

        // Assert
        Assert.AreEqual(_networkRepository, networkRepo);
    }

    [TestMethod]
    public void WebChannelRepository_ReturnsInjectedInstance()
    {
        // Act
        var webChannelRepo = _unitOfWork.WebChannelRepository;

        // Assert
        Assert.AreEqual(_webChannelRepository, webChannelRepo);
    }

    [TestMethod]
    public async Task SaveChangesAsyncTest()
    {
        // Act
        var result = await _unitOfWork.SaveChangesAsync();

        // Assert
        Assert.AreEqual(result, 0);
    }

    [TestMethod]
    public async Task SaveChangesAsyncTestSaving()
    {
        // Arrange
        var show1 = new ShowEntity() { Id = "1", Name = "Show Entity 1" };

        // Act
        _unitOfWork.ShowRepository.Insert(show1);

        // Assert
        var result = await _unitOfWork.SaveChangesAsync();
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public async Task InsertDuplicateShows_ShouldThrowArgumentException()
    {
        // Arrange
        var show1 = new ShowEntity() { Id = "1", Name = "Show Entity 1" };
        var show2 = new ShowEntity() { Id = "1", Name = "Show Entity 1" };

        // Act
        _unitOfWork.ShowRepository.Insert(show1);
        _unitOfWork.ShowRepository.Insert(show2);

        // Assert
        try
        {
            var result = await _unitOfWork.SaveChangesAsync();
            Assert.Fail("Se esperaba una excepción.");
        }
        catch (ArgumentException ex)
        {
            // Verifica que la excepción sea lanzada debido a la duplicidad de clave primaria
            Assert.IsInstanceOfType(ex, typeof(ArgumentException),
                "Se esperaba una excepción de tipo ArgumentException debido a la duplicidad de clave primaria.");
        }
        catch (Exception ex)
        {
            // Verifica que la excepción sea lanzada debido a la duplicidad de clave primaria
            Assert.Fail($"Excepción no esperada: {ex.GetType().Name}");

        }
    }

    [TestMethod]
    public async Task UpdateExistingEntity_ShouldModifyEntity()
    {
        // Arrange
        var show = new ShowEntity { Id = "1", Name = "Original Name" };
        _dbContext.Shows.Add(show);
        await _dbContext.SaveChangesAsync();

        // Act
        show.Name = "Updated Name";
        _unitOfWork.ShowRepository.Update(show);
        var result = await _unitOfWork.SaveChangesAsync();

        // Assert
        Assert.AreEqual(1, result);
        var updatedShow = await _dbContext.Shows.FindAsync("1");
        Assert.AreEqual("Updated Name", updatedShow.Name);
    }

    [TestMethod]
    public async Task DeleteExistingEntity_ShouldRemoveEntity()
    {
        // Arrange
        var show = new ShowEntity { Id = "1", Name = "Show to Delete" };
        _dbContext.Shows.Add(show);
        await _dbContext.SaveChangesAsync();

        // Act
        _unitOfWork.ShowRepository.Remove(show.Id);
        var result = await _unitOfWork.SaveChangesAsync();

        // Assert
        Assert.AreEqual(1, result);
        var deletedShow = await _dbContext.Shows.FindAsync("1");
        Assert.IsNull(deletedShow);
    }

    [TestMethod]
    public async Task GetEntityById_ShouldReturnEntity()
    {
        // Arrange
        var show = new ShowEntity { Id = "1", Name = "Show 1" };
        _dbContext.Shows.Add(show);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _unitOfWork.ShowRepository.GetById("1");

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(show.Name, result.Name);
    }

    [TestMethod]
    public async Task GetAllEntities_ShouldReturnAllEntities()
    {
        // Arrange
        var shows = new List<ShowEntity>
            {
                new ShowEntity { Id = "1", Name = "Show 1" },
                new ShowEntity { Id = "2", Name = "Show 2" }
            };
        _dbContext.Shows.AddRange(shows);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _unitOfWork.ShowRepository.GetAll().ToListAsync();

        // Assert
        Assert.AreEqual(2, result.Count);
    }

}

