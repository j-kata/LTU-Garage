namespace GarageApp.UI;

public class ConsoleUI : IUI
{
    public string ReadLine()
    {
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public void Write(string? text)
    {
        Console.Write(text);
    }

    public void WriteLine(string? text)
    {
        Console.WriteLine(text);
    }
}
