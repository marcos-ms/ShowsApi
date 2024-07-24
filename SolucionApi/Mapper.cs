using SolucionApi.ApiExternas.TvMazeApi.Models;
using SolucionApi.Data.Entities;
using SolucionApi.Dtos;

namespace SolucionApi;

public static class Mapper
{
    public static ShowDto ToDto(this Show show)
    {
        if (show == null)
            throw new ArgumentNullException(nameof(show));

        var showDto = new ShowDto
        {
            Id = show.Id.ToString(),
            Url = show.Url,
            Name = show.Name,
            Type = show.Type,
            Language = show.Language,
            Genres = (show.Genres is null) ? [] : [.. show.Genres],
            Status = show.Status,
            Runtime = show.Runtime,
            AverageRuntime = show.AverageRuntime,
            Premiered = show.Premiered,
            Ended = show.Ended,
            OfficialSite = show.OfficialSite,
            Schedule = show.Schedule?.ToDto(),
            Rating = show.Rating?.ToDto(),
            Weight = show.Weight,
            Network = show.Network?.ToDto(),
            WebChannel = show.WebChannel?.ToDto(),
            DvdCountry = show.DvdCountry?.ToDto(),
            Externals = show.Externals?.ToDto(),
            Image = show.Image?.ToDto(),
            Summary = show.Summary,
            Updated = show.Updated,
            Links = show.Links?.ToDto()
        };

        return showDto;
    }

    public static ExternalsDto ToDto(this Externals externals)
    {
        if (externals == null)
            throw new ArgumentNullException(nameof(externals));


        return new ExternalsDto
        {
            Tvrage = externals.Tvrage,
            Thetvdb = externals.Thetvdb,
            Imdb = externals.Imdb
        };
    }

    public static ImageDto ToDto(this Image image)
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));


        return new ImageDto
        {
            Medium = image.Medium,
            Original = image.Original
        };
    }

    public static LinksDto ToDto(this Links links)
    {
        if (links == null)
            throw new ArgumentNullException(nameof(links));

        return new LinksDto
        {
            Self = links.Self?.ToDto(),
            Previousepisode = links.Previousepisode?.ToDto(),
            Nextepisode = links.Nextepisode?.ToDto()
        };
    }

    public static SelfDto ToDto(this Self self)
    {
        if (self == null)
            throw new ArgumentNullException(nameof(self));

        return new SelfDto
        {
            Href = self.Href
        };
    }

    public static EpisodeDto ToDto(this Episode episode)
    {
        if (episode == null)
            throw new ArgumentNullException(nameof(episode));

        return new EpisodeDto
        {
            Href = episode.Href,
            Name = episode.Name ?? string.Empty
        };
    }

    public static NetworkDto ToDto(this Network network)
    {
        if (network == null)
            throw new ArgumentNullException(nameof(network));

        return new NetworkDto
        {
            Id = network.Id.ToString(),
            Name = network.Name,
            Country = network.Country?.ToDto(),
            OfficialSite = network.OfficialSite
        };
    }

    public static WebChannelDto ToDto(this WebChannel webChannel)
    {
        if (webChannel == null)
            throw new ArgumentNullException(nameof(webChannel));

        return new WebChannelDto
        {
            Id = webChannel.Id.ToString(),
            Name = webChannel.Name,
            Country = webChannel.Country?.ToDto(),
            OfficialSite = webChannel.OfficialSite
        };
    }

    public static CountryDto ToDto(this Country country)
    {
        if (country == null)
            throw new ArgumentNullException(nameof(country));

        return new CountryDto
        {
            Name = country.Name,
            Code = country.Code,
            Timezone = country.Timezone
        };
    }

    public static RatingDto ToDto(this Rating rating)
    {
        if (rating == null)
            throw new ArgumentNullException(nameof(rating));

        return new RatingDto
        {
            Average = rating.Average
        };
    }

    public static ScheduleDto ToDto(this Schedule schedule)
    {
        if (schedule == null)
            throw new ArgumentNullException(nameof(schedule));

        return new ScheduleDto
        {
            Time = schedule.Time,
            Days = (schedule.Days is null) ? [] : [.. schedule.Days]
        };
    }

    public static ShowEntity ToEntity(this ShowDto show)
    {
        var showEntity = new ShowEntity
        {
            Id = show.Id,
            Url = show.Url,
            Name = show.Name,
            Type = show.Type,
            Language = show.Language,
            Genres = (show.Genres is null) ? [] : [..show.Genres],
            Status = show.Status,
            Runtime = show.Runtime,
            AverageRuntime = show.AverageRuntime,
            Premiered = show.Premiered,
            Ended = show.Ended,
            OfficialSite = show.OfficialSite,
            Schedule = show.Schedule?.ToEntity(),
            Rating = show.Rating?.ToEntity() ?? new RatingEntity(),
            Weight = show.Weight,
            NetworkId = show.Network?.Id,
            Network = show.Network?.ToEntity(),
            WebChannelId = show.WebChannel?.Id,
            WebChannel = show.WebChannel?.ToEntity(),
            DvdCountry = show.DvdCountry?.ToEntity() ?? new CountryEntity(),
            Externals = show.Externals?.ToEntity() ?? new ExternalsEntity(),
            Image = show.Image?.ToEntity() ?? new ImageEntity(),
            Summary = show.Summary,
            Updated = show.Updated,
            Links = show.Links?.ToEntity() ?? new LinksEntity()
        };

        return showEntity;
    }

    public static ExternalsEntity ToEntity(this ExternalsDto externals)
    {
        return new ExternalsEntity
        {
            Tvrage = externals.Tvrage,
            Thetvdb = externals.Thetvdb,
            Imdb = externals.Imdb
        };
    }

    public static ImageEntity ToEntity(this ImageDto image)
    {
        return new ImageEntity
        {
            Medium = image.Medium,
            Original = image.Original
        };
    }

    public static LinksEntity ToEntity(this LinksDto links)
    {
        return new LinksEntity
        {
            Self = links.Self?.ToEntity(),
            Previousepisode = links.Previousepisode?.ToEntity(),
            Nextepisode = links.Nextepisode?.ToEntity()
        };
    }

    public static SelfEntity ToEntity(this SelfDto self)
    {
        return new SelfEntity
        {
            Href = self.Href
        };
    }

    public static EpisodeEntity ToEntity(this EpisodeDto previousepisode)
    {
        return new EpisodeEntity
        {
            Href = previousepisode.Href,
            Name = previousepisode.Name
        };
    }

    public static NetworkEntity ToEntity(this NetworkDto network)
    {
        return new NetworkEntity
        {
            Id = network.Id,
            Name = network.Name,
            Country = network.Country?.ToEntity(),
            OfficialSite = network.OfficialSite
        };
    }

    public static WebChannelEntity ToEntity(this WebChannelDto webChannel)
    {
        return new WebChannelEntity
        {
            Id = webChannel.Id,
            Name = webChannel.Name,
            Country = webChannel.Country?.ToEntity(),
            OfficialSite = webChannel.OfficialSite
        };
    }

    public static CountryEntity ToEntity(this CountryDto country)
    {
        return new CountryEntity
        {
            Name = country.Name,
            Code = country.Code,
            Timezone = country.Timezone
        };
    }

    public static RatingEntity ToEntity(this RatingDto rating)
    {
        return new RatingEntity
        {
            Average = rating.Average
        };
    }

    public static ScheduleEntity ToEntity(this ScheduleDto schedule)
    {
        return new ScheduleEntity
        {
            Time = schedule.Time,
            Days = (schedule.Days is null) ? [] : [.. schedule.Days]
        };
    }

    public static ShowDto ToDto(this ShowEntity show)
    {
        var showDto = new ShowDto
        {
            Id = show.Id,
            Url = show.Url,
            Name = show.Name,
            Type = show.Type,
            Language = show.Language,
            Genres = (show.Genres is null) ? [] : [.. show.Genres],
            Status = show.Status,
            Runtime = show.Runtime,
            AverageRuntime = show.AverageRuntime,
            Premiered = show.Premiered,
            Ended = show.Ended,
            OfficialSite = show.OfficialSite,
            Schedule = show.Schedule?.ToDto(),
            Rating = show.Rating?.ToDto(),
            Weight = show.Weight,
            Network = show.Network?.ToDto(),
            WebChannel = show.WebChannel?.ToDto(),
            DvdCountry = show.DvdCountry?.ToDto(),
            Externals = show.Externals?.ToDto(),
            Image = show.Image?.ToDto(),
            Summary = show.Summary,
            Updated = show.Updated,
            Links = show.Links?.ToDto()
        };

        return showDto;
    }

    public static ExternalsDto ToDto(this ExternalsEntity externals)
    {
        return new ExternalsDto
        {
            Tvrage = externals.Tvrage,
            Thetvdb = externals.Thetvdb,
            Imdb = externals.Imdb
        };
    }

    public static ImageDto ToDto(this ImageEntity image)
    {
        return new ImageDto
        {
            Medium = image.Medium,
            Original = image.Original
        };
    }

    public static LinksDto ToDto(this LinksEntity links)
    {
        return new LinksDto
        {
            Self = links.Self?.ToDto(),
            Previousepisode = links.Previousepisode?.ToDto(),
            Nextepisode = links.Nextepisode?.ToDto()
        };
    }

    public static SelfDto ToDto(this SelfEntity self)
    {
        return new SelfDto
        {
            Href = self.Href
        };
    }

    public static EpisodeDto ToDto(this EpisodeEntity episode)
    {
        return new EpisodeDto
        {
            Href = episode.Href,
            Name = episode.Name
        };
    }

    public static NetworkDto ToDto(this NetworkEntity network)
    {
        return new NetworkDto
        {
            Id = network.Id,
            Name = network.Name,
            Country = network.Country?.ToDto(),
            OfficialSite = network.OfficialSite
        };
    }

    public static WebChannelDto ToDto(this WebChannelEntity webChannel)
    {
        return new WebChannelDto
        {
            Id = webChannel.Id,
            Name = webChannel.Name,
            Country = webChannel.Country?.ToDto(),
            OfficialSite = webChannel.OfficialSite
        };
    }

    public static CountryDto ToDto(this CountryEntity country)
    {
        return new CountryDto
        {
            Name = country.Name,
            Code = country.Code,
            Timezone = country.Timezone
        };
    }

    public static RatingDto ToDto(this RatingEntity rating)
    {
        return new RatingDto
        {
            Average = rating.Average
        };
    }

    public static ScheduleDto ToDto(this ScheduleEntity schedule)
    {
        return new ScheduleDto
        {
            Time = schedule.Time,
            Days = (schedule.Days is null) ? [] : [.. schedule.Days]
        };
    }

}
