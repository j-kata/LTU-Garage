namespace GarageApp.Menu;

public interface IMenu
{
    public string Title { get; }
    public void Show();
    public bool HandleChoice(string choice);
}