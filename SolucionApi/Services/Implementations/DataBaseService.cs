using Microsoft.EntityFrameworkCore;
using SolucionApi.Data.Entities;
using SolucionApi.Data.Repositories.Contracts;
using SolucionApi.Dtos;
using SolucionApi.Shared;

namespace SolucionApi.Services.Implementations;

public class DataBaseService : IDataBaseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly List<string> statusList = ["Ended", "Running", "To Be Determined"];

    public DataBaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<ShowDto>> GetShowsAsync()
    {
        var entities = await _unitOfWork.ShowRepository.GetAll()
            .AsNoTracking()
            .Include(x => x.Network)
            .Include(x => x.WebChannel)
            .OrderBy(x => x.Id)
            .ToListAsync();
        return entities.Select(x => x.ToDto()).ToList();
    }

    public async Task<ShowDto?> GetShowAsync(string showId)
    {
        var entity = await _unitOfWork.ShowRepository.GetById(showId);
        return entity?.ToDto();
    }

    public async Task<List<ShowDto>> GetShowsAsync(ShowFilter filter)
    {
        var query = _unitOfWork.ShowRepository.GetAll()
            .AsNoTracking()
            .Include(x => x.Network)
            .Include(x => x.WebChannel)
            .AsQueryable();

        query = MakeQuery(query, filter);
        query = query.OrderBy(x => x.Id);

        return await query.Select(x => x.ToDto()).ToListAsync();
    }
    public List<ShowDto> GetShows(ShowFilter filter)
    {
        var query = _unitOfWork.ShowRepository.GetAll()
            .AsNoTracking()
            .Include(x => x.Network)
            .Include(x => x.WebChannel)
            .AsQueryable();

        query = MakeQuery(query, filter);
        query = query.OrderBy(x => x.Id);

        return query.Select(x => x.ToDto()).ToList();
    }

    public async Task<ResultValue> SaveShowImportsAsync(List<ShowDto> shows)
    {
        try
        {
            await SaveNetworksAsync(shows);

            await SaveWebChannelsAsync(shows);

            await SaveShowsToDataDaseAsync(shows);

            await _unitOfWork.SaveChangesAsync();
            return new ResultValue(true, ServicesConst.AllShowsSaved);

        }
        catch (Exception Ex)
        {
            return new ResultValue(false, ServicesConst.ErrorSavingAllShows);
        }
    }

    private async Task SaveShowsToDataDaseAsync(List<ShowDto> shows)
    {
        var showEntities = shows.Select(x =>
        {
            var showEntity = x.ToEntity();
            showEntity.Network = null;
            showEntity.WebChannel = null;
            return showEntity;
        }).ToList();

        foreach (var entity in showEntities)
        {
            var existingShow = await _unitOfWork.ShowRepository.Exist(entity.Id);
            if (existingShow)
            {
                _unitOfWork.ShowRepository.Update(entity);
            }
            else
            {
                await _unitOfWork.ShowRepository.Insert(entity);
            }
        }
    }

    private async Task SaveWebChannelsAsync(List<ShowDto> shows)
    {
        List<WebChannelDto?> distinctWebChannels = shows
            .Where(show => show.WebChannel != null)
            .Select(show => show.WebChannel)
            .DistinctBy(webChannel => webChannel.Id)
            .ToList();

        foreach (var webChannel in distinctWebChannels)
        {
            var existingWebChannel = await _unitOfWork.WebChannelRepository.Exist(webChannel.Id);
            if (existingWebChannel)
            {
                _unitOfWork.WebChannelRepository.Update(webChannel.ToEntity());
            }
            else
            {
                await _unitOfWork.WebChannelRepository.Insert(webChannel.ToEntity());
            }
        }
    }

    private async Task SaveNetworksAsync(List<ShowDto> shows)
    {
        List<NetworkDto?> distinctNetworks = shows
            .Where(show => show.Network != null)
            .Select(show => show.Network)
            .DistinctBy(network => network.Id)
            .ToList();

        foreach (var network in distinctNetworks)
        {
            var existingNetwork = await _unitOfWork.NetworkRepository.Exist(network.Id);
            if (existingNetwork)
            {
                _unitOfWork.NetworkRepository.Update(network.ToEntity());
            }
            else
            {
                await _unitOfWork.NetworkRepository.Insert(network.ToEntity());
            }
        }
    }

    public async Task<ResultValue> SaveShowAsync(ShowDto show)
    {
        try
        {
            ResultValue result;
            var exist = await _unitOfWork.ShowRepository.Exist(show.Id);
            if (exist)
            {
                _unitOfWork.ShowRepository.Update(show.ToEntity());
                result = new ResultValue(true, ServicesConst.ShowUpdated);
            }
            else
            {
                await _unitOfWork.ShowRepository.Insert(show.ToEntity());
                result = new ResultValue(true, ServicesConst.ShowSaved);
            }

            await _unitOfWork.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {
            return new ResultValue(false, ServicesConst.ErrorSavingShow);
        }
    }

    public async Task<ResultValue<ShowDto>> SaveShowsAsync(ShowDto show)
    {
            ShowEntity result;
            if (string.IsNullOrWhiteSpace(show.Id))
            {
                show.Id = Guid.NewGuid().ToString();
                result = await _unitOfWork.ShowRepository.Insert(show.ToEntity());
            }
            else
                result = _unitOfWork.ShowRepository.Update(show.ToEntity());

            await _unitOfWork.SaveChangesAsync();
            return new ResultValue<ShowDto>(true, ServicesConst.ShowSaved, result.ToDto());
    }

    public async Task<ResultValue> UpdateShowsAsync(ShowDto show)
    {
            var showExist = await _unitOfWork.ShowRepository.GetById(show.Id);
            if(showExist is null)
            {
                return new ResultValue(false, ServicesConst.ErrorNotFound);
            }

            if(!string.IsNullOrWhiteSpace(show.Name))
                showExist.Name = show.Name;
            if(show.Genres.Any())
                showExist.Genres = new List<string>(show.Genres);
            if(!string.IsNullOrWhiteSpace(show.Status))
                showExist.Status = show.Status;
            if(show.Rating.Average.HasValue )
                showExist.Rating.Average = show.Rating.Average.Value;

            _unitOfWork.ShowRepository.Update(showExist);
            await _unitOfWork.SaveChangesAsync();

            return new ResultValue(true, ServicesConst.ShowUpdated);
    }

    public async Task<ResultValue> DeleteShowAsync(string id)
    {
        var exist = await _unitOfWork.ShowRepository.Exist(id);
        if (!exist)
        {
            return new ResultValue(false, ServicesConst.ErrorNotFound);
        }

        await _unitOfWork.ShowRepository.Remove(id);
        await _unitOfWork.SaveChangesAsync();

        return new ResultValue(true, ServicesConst.ShowDeleted);
    }

    public async Task ClearDataBase()
    {
        var shows = await _unitOfWork.ShowRepository.GetAll().ToListAsync();
        var webChannels = await _unitOfWork.WebChannelRepository.GetAll().ToListAsync();
        var networks = await _unitOfWork.NetworkRepository.GetAll().ToListAsync();

        _unitOfWork.ShowRepository.RemoveRange(shows.ToArray());
        _unitOfWork.WebChannelRepository.RemoveRange(webChannels.ToArray());
        _unitOfWork.NetworkRepository.RemoveRange(networks.ToArray());

        await _unitOfWork.SaveChangesAsync();

    }

    private IQueryable<ShowEntity> MakeQuery(IQueryable<ShowEntity> query, ShowFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.Id))
        {
            query = query.Where(x => x.Id == filter.Id);
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(x => x.Name.Contains(filter.Name));
        }

        if (!string.IsNullOrEmpty(filter.Genres))
        {
            query = query.Where(show => show.Genres.Any(genre => genre.Contains(filter.Genres)));
        }

        if (filter.Status.HasValue)
        {
            if (filter.Status == 1)
                query = query.Where(x => x.Status == statusList[0]);
            else if (filter.Status == 2)
                query = query.Where(x => x.Status == statusList[1]);
            else if (filter.Status == 3)
                query = query.Where(x => x.Status == statusList[2]);
        }

        if (filter.MinAverage.HasValue && filter.MaxAverage.HasValue && filter.MinAverage == filter.MaxAverage)
        {
            double average = filter.MinAverage.Value;
            query = query.Where(x => x.Rating.Average >= average && x.Rating.Average < ((average >= 10.0) ? 10.0 : average + 0.1));
        }
        else
        {
            if (filter.MinAverage.HasValue)
            {
                // Añade un margen muy pequeño para incluir el valor exacto de MinAverage
                double minAverage = filter.MinAverage.Value;
                query = query.Where(x => x.Rating.Average >= minAverage);
            }

            if (filter.MaxAverage.HasValue)
            {
                // Añade un margen muy pequeño para evitar excluir el valor exacto de MaxAverage
                double maxAverage = filter.MaxAverage.Value;
                query = query.Where(x => x.Rating.Average < ((maxAverage >= 10.0) ? 10.0 : maxAverage + 0.1));
            }
        }

        return query;
    }
}