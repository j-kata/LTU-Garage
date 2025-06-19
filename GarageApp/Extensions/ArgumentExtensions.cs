namespace GarageApp.Extensions;

public static class ArgumentExtensions
{
    public static string NotEmpty(this string value, string name)
    {
        return !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentException("Should not be null or empty", name);
    }

    public static string MaxLength(this string value, int max, string name)
    {
        return value.Length <= max ? value
            : throw new ArgumentException($"Should not be longer than {max}", name);
    }

    public static double Positive(this double value, string name)
    {
        return value > 0 ? value
            : throw new ArgumentException("Should be positive", name);
    }

    // TODO: replace with one generic method
    public static int Positive(this int value, string name)
    {
        return value > 0 ? value
            : throw new ArgumentException("Should be positive", name);
    }

    public static T[] MaxLength<T>(this T[] array, int max, string name)
    {
        return array.Length <= max ? array
            : throw new ArgumentException($"Should be smaller than {max}", name);
    }

    public static int NotNegative(this int value, string name)
    {
        return value >= 0 ? value
            : throw new ArgumentException("Should be positive or 0", name);
    }

    public static T NotNull<T>(this T value, string name)
    {
        return value != null ? value
            : throw new ArgumentNullException(name);
    }
}