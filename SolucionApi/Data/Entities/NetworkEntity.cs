namespace SolucionApi.Data.Entities;

public class NetworkEntity : Entity<string>
{

    public string Name { get; set; } = null!;
    public CountryEntity? Country { get; set; }
    public Uri? OfficialSite { get; set; }

    public virtual ICollection<ShowEntity> Shows { get; set; } = new List<ShowEntity>();
}