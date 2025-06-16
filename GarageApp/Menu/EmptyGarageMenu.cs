using GarageApp.Handler;
using GarageApp.UI;

namespace GarageApp.Menu;


public class EmptyGarageMenu(IUI ui, IHandler handler) : BaseMenu(ui, handler)
{
    private const string AddChoice = "1";
    private const string LoadChoice = "2";
    private const string ExitChoice = "0";

    public override string Title { get; } = "No garage is chosen";

    public override void Show()
    {
        _ui.WriteLine(
            $"{AddChoice}. Create new garage \n" +
            $"{LoadChoice}. Load garage from source \n" +
            $"{ExitChoice}. Exit"
        );
    }
    public override bool HandleChoice(string choice)
    {
        switch (choice)
        {
            case AddChoice:
                CreateGarage();
                return false;
            case LoadChoice:
                LoadGarage();
                return false;
            case ExitChoice:
                Environment.Exit(0);
                return false;
            default:
                InvalidInput();
                return true;
        }
    }

    // TODO: Add helpers
    public void CreateGarage()
    {
        // TODO: fix no arguments_ui.WriteLine();
        _ui.Write("Enter garage size: ");
        var input = _ui.ReadLine();
        var capacity = int.Parse(input);
        _handler.CreateGarage(capacity);
    }

    public void LoadGarage() { }
    public void InvalidInput() { }
}