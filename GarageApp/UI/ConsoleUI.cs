namespace GarageApp.UI;

public class ConsoleUI : IUI
{
    public string ReadLine()
    {
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public void Write(string? text = null)
    {
        Console.Write(text);
    }

    public void WriteLine(string? text = null)
    {
        Console.WriteLine(text);
    }
}
