using GarageApp.Handler;
using GarageApp.UI;

namespace GarageApp.Menu;


public class DefaultGarageMenu(IUI ui, IHandler handler) : BaseMenu(ui, handler)
{
    private const string ParkChoice = "1";
    private const string DepartChoice = "2";
    private const string PrintCount = "3";
    private const string PrintAll = "4";
    private const string FindByNumber = "5";
    private const string FindByParams = "6";
    private const string ExitChoice = "0";

    public override string Title { get; } = "Welcome to garage management!";

    public override void Show()
    {
        _ui.WriteLine(
            $"{ParkChoice}. Park new vehicle \n" +
            $"{DepartChoice}. Depart vehicle \n" +
            $"{PrintCount}. Print number of vehicles\n" +
            $"{PrintAll}. Print all vehicles\n" +
            $"{FindByNumber}. Find vehicle by registration Number\n" +
            $"{FindByParams}. Find vehicle by parameters\n" +
            $"{ExitChoice}. Exit"
        );
    }
    public override bool HandleChoice(string choice)
    {
        switch (choice)
        {
            case ParkChoice:
                ParkVehicle();
                return true;
            case DepartChoice:
                DepartVehicle();
                return true;
            case PrintCount:
                PrintVehicleCount();
                return true;
            case PrintAll:
                PrintAllVehicles();
                return true;
            case FindByNumber:
                FindVehicleByRNumber();
                return true;
            case FindByParams:
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

    private void FindVehicleByRNumber()
    {
        throw new NotImplementedException();
    }

    private void FindVehicleByParameters()
    {
        throw new NotImplementedException();
    }

    public void ParkVehicle() { }
    public void DepartVehicle() { }
    public void PrintVehicleCount() { }
    public void PrintAllVehicles() { }
    public void InvalidInput() { }
}