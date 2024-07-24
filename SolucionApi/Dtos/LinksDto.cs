namespace SolucionApi.Dtos;

public class LinksDto
{
    public SelfDto? Self { get; set; }
    public EpisodeDto? Previousepisode { get; set; }
    public EpisodeDto? Nextepisode { get; set; }

}