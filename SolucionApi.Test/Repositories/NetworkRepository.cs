using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using SolucionApi.Data.Entities;
using SolucionApi.Data.Repositories.Implementations;

namespace SolucionApi.Test.Repositories;

[TestClass()]
public class NetworkRepositoryTest : BasePruebas
{
    public NetworkRepositoryTest()
    {
        
    }

    [TestMethod()]
    public async Task InsertTest()
    {
        // Preparacion
        var networkEntities = new List<NetworkEntity>()
        {
            new NetworkEntity { Id = "1", Name = "Network 1" },
            new NetworkEntity { Id = "2", Name = "Network 2" }
        };
        var newNetwork = new NetworkEntity { Id = "3", Name = "Network 3" };

        var nombreDb = Guid.NewGuid().ToString();
        var dbContext = ConstruirContext(nombreDb);

        await dbContext.Networks.AddRangeAsync(networkEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new NetworkRepository(dbContext);
        var entity = await repository.Insert(newNetwork);
        await dbContext.SaveChangesAsync();

        var entities = await repository.GetAll().ToListAsync();

        // Verificacion
        Assert.AreEqual(networkEntities.Count + 1, entities.Count());
        Assert.AreEqual(newNetwork.Id, entity.Id);
        Assert.AreEqual(newNetwork.Name, entity.Name);
    }

    [TestMethod()]
    public async Task UpdateTest()
    {
        // Preparacion
        var id = "2";
        var networkEntities = new List<NetworkEntity>()
        {
            new NetworkEntity { Id = "1", Name = "Network 1" },
            new NetworkEntity { Id = "2", Name = "Network 2" }
        };

        var nombreDb = Guid.NewGuid().ToString();
        var dbContext = ConstruirContext(nombreDb);

        await dbContext.Networks.AddRangeAsync(networkEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new NetworkRepository(dbContext);
        var entity = await repository.GetById(id);
        var oldEntity = new NetworkEntity()
        {
            Id = entity.Id,
            Name = entity.Name
        };
        entity.Name = "Network 2 Updated";
        repository.Update(entity);
        await dbContext.SaveChangesAsync();

        var newEntity = await repository.GetById(id);

        // Verificacion
        Assert.IsNotNull(entity);
        Assert.AreEqual(oldEntity.Id, newEntity.Id);
        Assert.AreNotEqual(oldEntity.Name, newEntity.Name);

    }

    [TestMethod()]
    public async Task GetAllNetworksTest()
    {
        // Preparacion
        var networkEntities = new List<NetworkEntity>()
        {
            new NetworkEntity { Id = "1", Name = "Network 1" },
            new NetworkEntity { Id = "2", Name = "Network 2" }
        };

        var nombreDb = Guid.NewGuid().ToString();
        var dbContext = ConstruirContext(nombreDb);

        await dbContext.Networks.AddRangeAsync(networkEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new NetworkRepository(dbContext);
        var entities = repository.GetAll().ToList();

        // Verificacion
        Assert.AreEqual(networkEntities.Count, entities.Count);
        for (int i = 0; i < networkEntities.Count; i++)
        {
            Assert.AreEqual(networkEntities[i].Id, entities[i].Id);
            Assert.AreEqual(networkEntities[i].Name, entities[i].Name);
        }

    }

    [TestMethod()]
    public async Task GetNetworkByIdTest()
    {
        // Preparacion
        var id = "1";
        var networkEntities = new List<NetworkEntity>()
        {
            new() { Id = "1", Name = "Network 1" },
            new() { Id = "2", Name = "Network 2" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.Networks.AddRangeAsync(networkEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new NetworkRepository(dbContext);
        var entity = await repository.GetById(id);

        // Verificacion
        Assert.AreEqual(networkEntities[0].Id, entity?.Id);
        Assert.AreEqual(networkEntities[0].Name, entity?.Name);
    }

    [TestMethod()]
    public async Task RemoveTest()
    {
        // Preparacion
        var id = "2";
        var networkEntities = new List<NetworkEntity>()
        {
            new() { Id = "1", Name = "Network 1" },
            new() { Id = "2", Name = "Network 2" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.Networks.AddRangeAsync(networkEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var repository = new NetworkRepository(dbContext);
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
        var networkEntities = new List<NetworkEntity>()
        {
            new() { Id = "1", Name = "Network 1" },
            new() { Id = "2", Name = "Network 2" },
            new() { Id = "3", Name = "Network 3" }
        };

        var deleteEntities = new List<NetworkEntity>()
        {
            networkEntities[0],
            networkEntities[2] 
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.Networks.AddRangeAsync(networkEntities);
        await dbContext.SaveChangesAsync();

        deleteEntities.ForEach(x => dbContext.Entry(x).State = EntityState.Detached);

        // Prueba
        var repository = new NetworkRepository(dbContext);
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
        var networkEntities = new List<NetworkEntity>()
        {
            new() { Id = "1", Name = "Network 1" },
            new() { Id = "2", Name = "Network 2" },
            new() { Id = "3", Name = "Network 3" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.Networks.AddRangeAsync(networkEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var count = dbContext.Networks.Count();

        // Verificacion
        Assert.AreEqual(networkEntities.Count, count);
    }

    [TestMethod()]
    public async Task ExistTest()
    {
        // Preparacion
        var id = "1";
        var networkEntities = new List<NetworkEntity>()
        {
            new() { Id = "1", Name = "Network 1" },
            new() { Id = "2", Name = "Network 2" },
            new() { Id = "3", Name = "Network 3" }
        };

        var dbContext = ConstruirContext(Guid.NewGuid().ToString());

        await dbContext.Networks.AddRangeAsync(networkEntities);
        await dbContext.SaveChangesAsync();

        // Prueba
        var exist = await dbContext.Networks.AnyAsync(x => x.Id.Equals(id));

        // Verificacion
        Assert.IsTrue(exist);
    }


}