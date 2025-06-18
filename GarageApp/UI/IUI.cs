namespace GarageApp.UI;

public interface IUI
{
    public string ReadLine();
    public void Write(string? value = "");
    public void WriteLine(string? value = "");
    public void IndentedWriteLine(string? value = "");
    public void Separator(char symbol = '-', int repeat = 15);
    public void Clear();
}