using GarageApp.Handler;
using GarageApp.Helpers;
using GarageApp.Loader;
using GarageApp.UI;
using GarageApp.Vehicles;

namespace GarageApp.Menu;

public class DefaultGarageMenu : BaseMenu
{
    private const string ParkChoice = "1";
    private const string DepartChoice = "2";
    private const string PrintListChoice = "3";
    private const string PrintStatChoice = "4";
    private const string FindByNumberChoice = "5";
    private const string FilterChoice = "6";
    private const string LoadVehiclesChoice = "7";
    private const string ExitChoice = "0";

    private readonly ILoader<Vehicle> _loader;
    private readonly Dictionary<string, (string name, Action action)> _menuOptions;

    public DefaultGarageMenu(IUI ui, IHandler handler, ILoader<Vehicle> loader) : base(ui, handler)
    {
        _loader = loader;
        _menuOptions = new Dictionary<string, (string name, Action action)>{
            { ParkChoice, ("Park new vehicle", ParkVehicle) },
            { DepartChoice, ("Depart vehicle", DepartVehicle) },
            { PrintListChoice, ("Print list of vehicles", PrintVehiclesList) },
            { PrintStatChoice, ("Print vehicle type statistics", PrintVehicleTypeStats) },
            { FindByNumberChoice, ("Find vehicle by registration number", FindVehicleByRNumber) },
            { FilterChoice, ("Filter vehicles by parameters", FilterVehicles) },
            { LoadVehiclesChoice, ("Load vehicles from source", LoadVehiclesFromSource) },
            { ExitChoice, ("Exit", () =>  ShouldExit = true) },
        };
    }

    public override string Title { get; } = "Welcome to garage management!";

    public override void Show()
    {
        _ui.Separator();
        // Overview
        foreach (var line in _handler.GarageOverview())
            _ui.WriteLine(line);

        _ui.Separator();
        // Options
        foreach (var option in _menuOptions)
            _ui.WriteLine($"{option.Key}. {option.Value.name}");

        _ui.Separator();
    }

    public override bool HandleChoice(string choice)
    {
        if (_menuOptions.TryGetValue(choice, out var value))
        {
            value.action.Invoke();
            return choice != ExitChoice;
        }

        _ui.WriteLine("Unknown command. Try again.");
        return true;
    }


    public void ParkVehicle()
    {
        new ParkVehicleMenu(_ui, _handler).Run();
    }

    public void DepartVehicle()
    {
        var rNumber = Util.PromptUntilValidString(_ui, "Enter registration number: ");
        _ui.WriteLine(_handler.DepartVehicle(rNumber));
    }

    public void PrintVehiclesList()
    {
        foreach (var vehicle in _handler.ListVehicles())
            _ui.WriteLine(vehicle);
    }

    public void PrintVehicleTypeStats()
    {
        foreach (var vehicle in _handler.VehicleTypeStats())
            _ui.WriteLine(vehicle);
    }

    private void FindVehicleByRNumber()
    {
        var rNumber = Util.PromptUntilValidString(_ui, "Enter registration number: ");
        _ui.WriteLine(_handler.FindByRegistration(rNumber));
    }

    private void FilterVehicles()
    {
        new FilterVehicleMenu(_ui, _handler).Run();
    }

    private void LoadVehiclesFromSource()
    {
        var vehicles = _loader.Load();
        foreach (var vehicle in vehicles)
            _ui.WriteLine(_handler.ParkVehicle(vehicle));
    }
}