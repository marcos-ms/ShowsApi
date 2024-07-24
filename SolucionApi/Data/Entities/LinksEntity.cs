namespace SolucionApi.Data.Entities;

public class LinksEntity
{
    public SelfEntity Self { get; set; }
    public EpisodeEntity Previousepisode { get; set; }
    public EpisodeEntity Nextepisode { get; set; }
}
