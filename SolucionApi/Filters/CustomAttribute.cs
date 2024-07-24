namespace SolucionApi.Filters;

public class CustomAttribute
{
    public bool ContainsAttribute { get; }
    public bool Mandatory { get; }

    public CustomAttribute(bool containsAttribute, bool mandatory)
    {
        ContainsAttribute = containsAttribute;
        Mandatory = mandatory;
    }
}