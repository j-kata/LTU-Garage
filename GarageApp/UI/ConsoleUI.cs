namespace GarageApp.UI;

public class ConsoleUI : IUI
{
    public string ReadLine()
    {
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public void Write(string? text = "")
    {
        Console.Write(text);
    }

    public void WriteLine(string? text = "")
    {
        Console.WriteLine(text);
    }

    public void IndentedWriteLine(string? text = "")
    {
        WriteLine();
        WriteLine(text);
    }

    public void Clear()
    {
        Console.Clear();
    }

    public void Separator(char symbol = '-', int repeat = 15)
    {
        WriteLine(new string(symbol, repeat));
    }
}
