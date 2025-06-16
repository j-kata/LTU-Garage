namespace GarageApp.UI;

public interface IUI
{
    public string ReadLine();
    public void Write(string? value);
    public void WriteLine(string? value);
}