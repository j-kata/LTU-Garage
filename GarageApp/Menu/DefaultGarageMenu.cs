using GarageApp.Handler;
using GarageApp.Helpers;
using GarageApp.UI;
using GarageApp.Vehicles;

namespace GarageApp.Menu;


public class DefaultGarageMenu(IUI ui, IHandler handler) : BaseMenu(ui, handler)
{
    private const string ParkChoice = "1";
    private const string DepartChoice = "2";
    private const string PrintListChoice = "3";
    private const string PrintStatChoice = "4";
    private const string FindByNumberChoice = "5";
    private const string FindByParamsChoice = "6";
    private const string ExitChoice = "0";

    public override string Title { get; } = "Welcome to garage management!";

    public override void Show()
    {
        _ui.IndentedWriteLine(
            $"Garage capacity is {_handler.TotalPlaces()}. " +
            $"{_handler.FreePlaces()} spot(s) are free."
        );

        _ui.WriteLine(
            $"{ParkChoice}. Park new vehicle \n" +
            $"{DepartChoice}. Depart vehicle \n" +
            $"{PrintListChoice}. Print list of vehicles\n" +
            $"{PrintStatChoice}. Print vehicle type statistics\n" +
            $"{FindByNumberChoice}. Find vehicle by registration number\n" +
            $"{FindByParamsChoice}. Find vehicle by parameters\n" +
            $"{ExitChoice}. Exit"
        );
    }

    public override bool HandleChoice(string choice)
    {
        // TODO: add check if garage is empty?
        switch (choice)
        {
            case ParkChoice:
                ParkVehicle();
                return true;

            case DepartChoice:
                DepartVehicle();
                return true;

            case PrintListChoice:
                PrintVehiclesList();
                return true;

            case PrintStatChoice:
                PrintVehicleTypeStats();
                return true;

            case FindByNumberChoice:
                FindVehicleByRNumber();
                return true;

            case FindByParamsChoice:
                FindVehicleByParameters();
                return true;

            case ExitChoice:
                ContinueToNextMenu = false;
                return false;

            default:
                InvalidInput();
                return true;
        }
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

    private void FindVehicleByParameters()
    {
        throw new NotImplementedException();
    }

    public void InvalidInput()
    {
        _ui.WriteLine("Unknown command. Try again.");
    }
}