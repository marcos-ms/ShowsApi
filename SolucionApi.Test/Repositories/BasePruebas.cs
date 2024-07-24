using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SolucionApi.Data;
using SolucionApi.Data.Entities;
using SolucionApi.Data.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace SolucionApi.Test.Repositories;

public abstract class BasePruebas
{
    protected DataBaseApiDbContext ConstruirContext(string nombreDb)
    {
        var options = new DbContextOptionsBuilder<DataBaseApiDbContext>()
            .UseInMemoryDatabase(nombreDb)
            .Options;
        var dbContext = new DataBaseApiDbContext(options);
        return dbContext;
    }

    public class MockShowRepository : IShowRepository
    {
        public Task<ShowEntity> Insert(ShowEntity entity)
        {
            return Task.FromResult(new ShowEntity() { Id = "1", Name = "ShowEntity 1" });
        }

        public Task<ShowEntity?> GetById(string id)
        {
            return Task.FromResult(new ShowEntity() { Id = "1", Name = "ShowEntity 1" });
        }

        public IQueryable<ShowEntity> GetAll()
        {
            var webChannels = new List<ShowEntity>
            {
                new ShowEntity { Id = "1", Name = "Channel 1" },
                new ShowEntity { Id = "2", Name = "Channel 2" }
            };
            return webChannels.AsQueryable();
        }

        public ShowEntity Update(ShowEntity entity)
        {
            return new ShowEntity() { Id = "1", Name = "ShowEntity 1" };
        }

        public Task<bool> Remove(string id)
        {
            return Task.FromResult(false);
        }

        public void RemoveRange(params ShowEntity[] entities)
        {
        }

        public int Count()
        {
            return 0;
        }

        public Task<bool> Exist(string id)
        {
            return Task.FromResult(false);
        }
    }

    public class MockNetworkRepository : INetworkRepository
    {
        public Task<NetworkEntity> Insert(NetworkEntity entity)
        {
            return Task.FromResult(new NetworkEntity() { Id = "1", Name = "NetworkEntity 1" });
        }

        public Task<NetworkEntity?> GetById(string id)
        {
            return Task.FromResult(new NetworkEntity() { Id = "1", Name = "NetworkEntity 1" });
        }

        public IQueryable<NetworkEntity> GetAll()
        {
            var webChannels = new List<NetworkEntity>
            {
                new NetworkEntity { Id = "1", Name = "Channel 1" },
                new NetworkEntity { Id = "2", Name = "Channel 2" }
            };
            return webChannels.AsQueryable();
        }

        public NetworkEntity Update(NetworkEntity entity)
        {
            return new NetworkEntity() { Id = "1", Name = "NetworkEntity 1" };
        }

        public Task<bool> Remove(string id)
        {
            return Task.FromResult(false);
        }

        public void RemoveRange(params NetworkEntity[] entities)
        {
        }

        public int Count()
        {
            return 0;
        }

        public Task<bool> Exist(string id)
        {
            return Task.FromResult(false);
        }
    }

    public class MockWebChannelRepository : IWebChannelRepository
    {
        // Implementar métodos de IWebChannelRepository según sea necesario para las pruebas
        public Task<WebChannelEntity> Insert(WebChannelEntity entity)
        {
            return Task.FromResult(new WebChannelEntity() { Id = "1", Name = "WebChannelEntity 1" });
        }

        public Task<WebChannelEntity?> GetById(string id)
        {
            return Task.FromResult(new WebChannelEntity() { Id = "1", Name = "WebChannelEntity 1" });
        }

        public IQueryable<WebChannelEntity> GetAll()
        {
            var webChannels = new List<WebChannelEntity>
                {
                    new WebChannelEntity { Id = "1", Name = "Channel 1" },
                    new WebChannelEntity { Id = "2", Name = "Channel 2" }
                };
            return webChannels.AsQueryable();
        }

        public WebChannelEntity Update(WebChannelEntity entity)
        {
            return new WebChannelEntity() { Id = "1", Name = "WebChannelEntity 1" };
        }

        public Task<bool> Remove(string id)
        {
            return Task.FromResult(false);
        }

        public void RemoveRange(params WebChannelEntity[] entities)
        {
        }

        public int Count()
        {
            return 0;
        }

        public Task<bool> Exist(string id)
        {
            return Task.FromResult(false);
        }
    }
}
