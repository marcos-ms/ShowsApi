using Microsoft.EntityFrameworkCore;
using SolucionApi.Data.Entities;
using SolucionApi.Data.Repositories.Implementations;

namespace SolucionApi.Test.Repositories;

[TestClass()]
public class ShowRepositoryTest : BasePruebas
{

    [TestMethod()]
    public async Task InsertTest()
    {
        // Preparacion
        var showEntities = new List<ShowEntity>()
        {
            new ShowEntity { Id = "1", Name = "Show 1" },
            new ShowEntity { Id = "2", Name = "Show 2" }
        };
        var newShow = new ShowEntity { Id = "3", Name = "Show 3" };

        var nombreDb = Guid.NewGuid().ToString();
        var dbContext = ConstruirContext(nombreDb);

        await dbContext.Shows.AddRangeAsync(showEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new ShowRepository(dbContext);
        var entity = await repository.Insert(newShow);
        await dbContext.SaveChangesAsync();

        var entities = await repository.GetAll().ToListAsync();

        // Verificacion
        Assert.AreEqual(showEntities.Count + 1, entities.Count());
        Assert.AreEqual(newShow.Id, entity.Id);
        Assert.AreEqual(newShow.Name, entity.Name);
    }

    [TestMethod()]
    public async Task UpdateTest()
    {
        // Preparacion
        var id = "2";
        var showEntities = new List<ShowEntity>()
        {
            new ShowEntity { Id = "1", Name = "Show 1" },
            new ShowEntity { Id = "2", Name = "Show 2" }
        };

        var nombreDb = Guid.NewGuid().ToString();
        var dbContext = ConstruirContext(nombreDb);

        await dbContext.Shows.AddRangeAsync(showEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new ShowRepository(dbContext);
        var entity = await repository.GetById(id);
        var oldEntity = new ShowEntity()
        {
            Id = entity.Id,
            Name = entity.Name
        };
        entity.Name = "Show 2 Updated";
        repository.Update(entity);
        await dbContext.SaveChangesAsync();

        var newEntity = await repository.GetById(id);

        // Verificacion
        Assert.IsNotNull(entity);
        Assert.AreEqual(oldEntity.Id, newEntity.Id);
        Assert.AreNotEqual(oldEntity.Name, newEntity.Name);

    }

    [TestMethod()]
    public async Task GetAllShowsTest()
    {
        // Preparacion
        var showEntities = new List<ShowEntity>()
        {
            new ShowEntity { Id = "1", Name = "Show 1" },
            new ShowEntity { Id = "2", Name = "Show 2" }
        };

        var nombreDb = Guid.NewGuid().ToString();
        var dbContext = ConstruirContext(nombreDb);

        await dbContext.Shows.AddRangeAsync(showEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new ShowRepository(dbContext);
        var entities = repository.GetAll().ToList();

        // Verificacion
        Assert.AreEqual(showEntities.Count, entities.Count);
        for (int i = 0; i < showEntities.Count; i++)
        {
            Assert.AreEqual(showEntities[i].Id, entities[i].Id);
            Assert.AreEqual(showEntities[i].Name, entities[i].Name);
        }

    }

    [TestMethod()]
    public async Task GetShowByIdTest()
    {
        // Preparacion
        var id = "1";
        var showEntities = new List<ShowEntity>()
        {
            new() { Id = "1", Name = "Show 1" },
            new() { Id = "2", Name = "Show 2" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.Shows.AddRangeAsync(showEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new ShowRepository(dbContext);
        var entity = await repository.GetById(id);

        // Verificacion
        Assert.AreEqual(showEntities[0].Id, entity?.Id);
        Assert.AreEqual(showEntities[0].Name, entity?.Name);
    }

    [TestMethod()]
    public async Task RemoveTest()
    {
        // Preparacion
        var id = "2";
        var showEntities = new List<ShowEntity>()
        {
            new() { Id = "1", Name = "Show 1" },
            new() { Id = "2", Name = "Show 2" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.Shows.AddRangeAsync(showEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new ShowRepository(dbContext);
        var success = await repository.Remove(id);
        await dbContext.SaveChangesAsync();

        var entity = await repository.GetById(id);


        // Verificacion
        Assert.IsTrue(success);
        Assert.IsNull(entity);
    }

    [TestMethod()]
    public async Task RemoveRangeTest()
    {
        // Preparacion
        var showEntities = new List<ShowEntity>()
        {
            new() { Id = "1", Name = "Show 1" },
            new() { Id = "2", Name = "Show 2" },
            new() { Id = "3", Name = "Show 3" }
        };

        var deleteEntities = new List<ShowEntity>()
        {
            showEntities[0],
            showEntities[2]
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.Shows.AddRangeAsync(showEntities);
        await dbContext.SaveChangesAsync();

        deleteEntities.ForEach(x => dbContext.Entry(x).State = EntityState.Detached);

        // Prueba
        var repository = new ShowRepository(dbContext);
        repository.RemoveRange(deleteEntities.ToArray());
        await dbContext.SaveChangesAsync();

        var entity1 = await repository.GetById("1");
        var entity3 = await repository.GetById("3");
        var entity2 = await repository.GetById("2");

        // Verificacion
        Assert.IsNull(entity1);
        Assert.IsNull(entity3);
        Assert.IsNotNull(entity2);
    }

    [TestMethod()]
    public async Task CountTest()
    {
        // Preparacion
        var showEntities = new List<ShowEntity>()
        {
            new() { Id = "1", Name = "Show 1" },
            new() { Id = "2", Name = "Show 2" },
            new() { Id = "3", Name = "Show 3" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.Shows.AddRangeAsync(showEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var count = dbContext.Shows.Count();

        // Verificacion
        Assert.AreEqual(showEntities.Count, count);
    }

    [TestMethod()]
    public async Task ExistTest()
    {
        // Preparacion
        var id = "1";
        var showEntities = new List<ShowEntity>()
        {
            new() { Id = "1", Name = "Show 1" },
            new() { Id = "2", Name = "Show 2" },
            new() { Id = "3", Name = "Show 3" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.Shows.AddRangeAsync(showEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var exist = await dbContext.Shows.AnyAsync(x => x.Id.Equals(id));

        // Verificacion
        Assert.IsTrue(exist);
    }
}