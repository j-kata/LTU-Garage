namespace GarageApp.Extensions;

public static class ArgumentExtensions
{
    public static string NotEmpty(this string value, string name)
    {
        return !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentException(name);
    }

    public static double Positive(this double value, string name)
    {
        return value > 0 ? value
            : throw new ArgumentException(name);
    }

    public static int Positive(this int value, string name)
    {
        return value > 0 ? value
            : throw new ArgumentException(name);
    }


    public static int NotNegative(this int value, string name)
    {
        return value >= 0 ? value
            : throw new ArgumentException(name);
    }
}