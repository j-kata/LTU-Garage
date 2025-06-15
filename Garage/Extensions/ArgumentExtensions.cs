namespace Garage.Extensions;

public static class ArgumentExtensions
{
    public static string NonEmpty(this string value, string name)
    {
        return !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentException(name);
    }

    public static uint NonZero(this uint value, string name)
    {
        return value != 0 ? value
            : throw new ArgumentException(name);
    }

    public static float Positive(this float value, string name)
    {
        return value > 0 ? value
            : throw new ArgumentException(name);
    }
}