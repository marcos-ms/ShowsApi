using Microsoft.EntityFrameworkCore;
using SolucionApi.Data.Entities;
using SolucionApi.Data.Repositories.Implementations;

namespace SolucionApi.Test.Repositories;

[TestClass()]
public class WebChannelRepositoryTest : BasePruebas
{

    [TestMethod()]
    public async Task InsertTest()
    {
        // Preparacion
        var webChannelEntities = new List<WebChannelEntity>()
        {
            new WebChannelEntity { Id = "1", Name = "WebChannel 1" },
            new WebChannelEntity { Id = "2", Name = "WebChannel 2" }
        };
        var newWebChannel = new WebChannelEntity { Id = "3", Name = "WebChannel 3" };

        var nombreDb = Guid.NewGuid().ToString();
        var dbContext = ConstruirContext(nombreDb);

        await dbContext.WebChannels.AddRangeAsync(webChannelEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new WebChannelRepository(dbContext);
        var entity = await repository.Insert(newWebChannel);
        await dbContext.SaveChangesAsync();

        var entities = await repository.GetAll().ToListAsync();

        // Verificacion
        Assert.AreEqual(webChannelEntities.Count + 1, entities.Count());
        Assert.AreEqual(newWebChannel.Id, entity.Id);
        Assert.AreEqual(newWebChannel.Name, entity.Name);
    }

    [TestMethod()]
    public async Task UpdateTest()
    {
        // Preparacion
        var id = "2";
        var webChannelEntities = new List<WebChannelEntity>()
        {
            new WebChannelEntity { Id = "1", Name = "WebChannel 1" },
            new WebChannelEntity { Id = "2", Name = "WebChannel 2" }
        };

        var nombreDb = Guid.NewGuid().ToString();
        var dbContext = ConstruirContext(nombreDb);

        await dbContext.WebChannels.AddRangeAsync(webChannelEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new WebChannelRepository(dbContext);
        var entity = await repository.GetById(id);
        var oldEntity = new WebChannelEntity()
        {
            Id = entity.Id,
            Name = entity.Name
        };
        entity.Name = "WebChannel 2 Updated";
        repository.Update(entity);
        await dbContext.SaveChangesAsync();

        var newEntity = await repository.GetById(id);

        // Verificacion
        Assert.IsNotNull(entity);
        Assert.AreEqual(oldEntity.Id, newEntity.Id);
        Assert.AreNotEqual(oldEntity.Name, newEntity.Name);

    }

    [TestMethod()]
    public async Task GetAllWebChannelsTest()
    {
        // Preparacion
        var webChannelEntities = new List<WebChannelEntity>()
        {
            new WebChannelEntity { Id = "1", Name = "WebChannel 1" },
            new WebChannelEntity { Id = "2", Name = "WebChannel 2" }
        };

        var nombreDb = Guid.NewGuid().ToString();
        var dbContext = ConstruirContext(nombreDb);

        await dbContext.WebChannels.AddRangeAsync(webChannelEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new WebChannelRepository(dbContext);
        var entities = repository.GetAll().ToList();

        // Verificacion
        Assert.AreEqual(webChannelEntities.Count, entities.Count);
        for (int i = 0; i < webChannelEntities.Count; i++)
        {
            Assert.AreEqual(webChannelEntities[i].Id, entities[i].Id);
            Assert.AreEqual(webChannelEntities[i].Name, entities[i].Name);
        }

    }

    [TestMethod()]
    public async Task GetWebChannelByIdTest()
    {
        // Preparacion
        var id = "1";
        var webChannelEntities = new List<WebChannelEntity>()
        {
            new() { Id = "1", Name = "WebChannel 1" },
            new() { Id = "2", Name = "WebChannel 2" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.WebChannels.AddRangeAsync(webChannelEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new WebChannelRepository(dbContext);
        var entity = await repository.GetById(id);

        // Verificacion
        Assert.AreEqual(webChannelEntities[0].Id, entity?.Id);
        Assert.AreEqual(webChannelEntities[0].Name, entity?.Name);
    }

    [TestMethod()]
    public async Task RemoveTest()
    {
        // Preparacion
        var id = "2";
        var webChannelEntities = new List<WebChannelEntity>()
        {
            new() { Id = "1", Name = "WebChannel 1" },
            new() { Id = "2", Name = "WebChannel 2" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.WebChannels.AddRangeAsync(webChannelEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new WebChannelRepository(dbContext);
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
        var webChannelEntities = new List<WebChannelEntity>()
        {
            new() { Id = "1", Name = "WebChannel 1" },
            new() { Id = "2", Name = "WebChannel 2" },
            new() { Id = "3", Name = "WebChannel 3" }
        };

        var deleteEntities = new List<WebChannelEntity>()
        {
            webChannelEntities[0],
            webChannelEntities[2]
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.WebChannels.AddRangeAsync(webChannelEntities);
        await dbContext.SaveChangesAsync();

        deleteEntities.ForEach(x => dbContext.Entry(x).State = EntityState.Detached);

        // Prueba
        var repository = new WebChannelRepository(dbContext);
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
        var webChannelEntities = new List<WebChannelEntity>()
        {
            new() { Id = "1", Name = "WebChannel 1" },
            new() { Id = "2", Name = "WebChannel 2" },
            new() { Id = "3", Name = "WebChannel 3" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.WebChannels.AddRangeAsync(webChannelEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var count = dbContext.WebChannels.Count();

        // Verificacion
        Assert.AreEqual(webChannelEntities.Count, count);
    }

    [TestMethod()]
    public async Task ExistTest()
    {
        // Preparacion
        var id = "1";
        var webChannelEntities = new List<WebChannelEntity>()
        {
            new() { Id = "1", Name = "WebChannel 1" },
            new() { Id = "2", Name = "WebChannel 2" },
            new() { Id = "3", Name = "WebChannel 3" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.WebChannels.AddRangeAsync(webChannelEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var exist = await dbContext.WebChannels.AnyAsync(x => x.Id.Equals(id));

        // Verificacion
        Assert.IsTrue(exist);
    }
}