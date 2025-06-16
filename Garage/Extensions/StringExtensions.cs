namespace Garage.Extensions;

public static class StringExtensions
{
    public static bool EqualsCaseIgnore(this string value, string? otherValue)
    {
        return otherValue is not null
            && value.Equals(otherValue, StringComparison.OrdinalIgnoreCase);
    }
}