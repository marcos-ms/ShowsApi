using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SolucionApi.Data.Converters;

public class UriStringConverter : ValueConverter<Uri?, string?>
{
    public UriStringConverter() : base(x => ConvertToString(x), x => ConvertToUri(x))
    {
        
    }
    
    private static string? ConvertToString(Uri? uri) => uri?.ToString();

    private static Uri? ConvertToUri(string? uri) => uri is null ? null : new Uri(uri);
}