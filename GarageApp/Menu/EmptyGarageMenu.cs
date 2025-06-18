using GarageApp.Handler;
using GarageApp.UI;
using GarageApp.Helpers;
using GarageApp.Loader;
using GarageApp.Vehicles;

namespace GarageApp.Menu;


public class EmptyGarageMenu : BaseMenu
{
    private const string AddChoice = "1";
    private const string LoadChoice = "2";
    private const string ExitChoice = "0";

    private readonly ILoader<Vehicle> _loader;
    private readonly Dictionary<string, (string name, Action action)> _menuOptions;

    public EmptyGarageMenu(IUI ui, IHandler handler, ILoader<Vehicle> loader) : base(ui, handler)
    {
        _loader = loader;
        _menuOptions = new Dictionary<string, (string name, Action action)>
        {
            { AddChoice, ("Create new garage", CreateGarage) },
            { LoadChoice, ("Create garage from source", LoadGarage) },
            { ExitChoice, ("Exit", () => ShouldExit = true) },
        };
    }

    public override string Title { get; } = "No garage is chosen";

    public override void Show()
    {
        _ui.Separator();
        foreach (var option in _menuOptions)
            _ui.WriteLine($"{option.Key}. {option.Value.name}");

        _ui.Separator();
    }

    // Handle input. Return false to exit from menu 
    public override bool HandleChoice(string choice)
    {
        // Check if input is a valid menu option
        if (_menuOptions.TryGetValue(choice, out var value))
        {
            value.action();
            return false;
        }

        _ui.WriteLine("Unknown command. Try again.");
        return true;
    }

    // Ask for valid garage capacity and create new garage
    public void CreateGarage()
    {
        var capacity = Util.PromptUntilValidNumber(_ui, "Enter garage size: ", (val) => val > 0);
        _handler.CreateGarage(capacity);
    }

    // Create garage from source
    public void LoadGarage()
    {
        var vehicles = _loader.Load();
        _handler.CreateGarage(vehicles);
    }
}