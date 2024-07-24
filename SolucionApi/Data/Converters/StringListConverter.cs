using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SolucionApi.Data.Converters;

public class StringListConverter : ValueConverter<List<string>, string>
{
    public StringListConverter()
        : base(strings => ConvertToString(strings), str => ConvertToList(str))
    {
    }

    private static string ConvertToString(List<string> strings)
        => strings == null ? string.Empty : string.Join(",", strings);

    private static List<string> ConvertToList(string str)
        => string.IsNullOrEmpty(str) ? new List<string>() : str.Split(',').ToList();
}