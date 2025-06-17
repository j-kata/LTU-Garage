using GarageApp.Handler;
using GarageApp.UI;
using GarageApp.Helpers;
using GarageApp.Loader;
using GarageApp.Vehicles;

namespace GarageApp.Menu;


public class EmptyGarageMenu(IUI ui, IHandler handler, ILoader<Vehicle> loader)
    : BaseMenu(ui, handler)
{
    private const string AddChoice = "1";
    private const string LoadChoice = "2";
    private const string ExitChoice = "0";

    public override string Title { get; } = "No garage is chosen";

    public override void Show()
    {
        _ui.IndentedWriteLine(
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
                ContinueToNextMenu = false;
                return false;

            default:
                InvalidInput();
                return true;
        }
    }

    public void CreateGarage()
    {
        var capacity = Util.PromptUntilValidNumber(_ui, "Enter garage size: ", (val) => val > 0);
        _handler.CreateGarage(capacity);
    }

    public void LoadGarage()
    {
        var vehicles = loader.Load();
        _handler.CreateGarage(vehicles);
    }

    public void InvalidInput()
    {
        _ui.WriteLine("Unknown command. Try again.");
    }
}