using GarageApp.Handler;
using GarageApp.UI;

namespace GarageApp.Manager;

public class GarageManager : IManager
{
    private readonly IUI _ui;
    private readonly IHandler _handler;
    private bool _keepRunning = true;

    public GarageManager(IUI ui, IHandler handler)
    {
        _ui = ui;
        _handler = handler;
    }

    public void Run()
    {
        while (_keepRunning)
        {
            ShowMainMenu();
            var choice = _ui.ReadLine();
            // AnalyzeChoice(choice);
        }
    }

    private void ShowMainMenu()
    {
        var menu = _handler.HasGarage() ? GetGarageMenu() : GetDefaultMenu();
        _ui.WriteLine(menu);
    }

    private static string GetDefaultMenu()
    {
        return
            "No Garage Is Chosen\n" +
            "1. Create New Garage \n" +
            "2. Load Garage From Source \n" +
            "0. Exit";

    }

    private static string GetGarageMenu()
    {
        return
            "Welcome To Garage Management!\n" +
            "1. Park New Vehicle \n" +
            "2. Depart Vehicle \n" +
            "3. Print Number Of Vehicles\n" +
            "4. Print All Vehicles\n" +
            "5. Find Vehicle By Registration Number\n" +
            "6. Find Vehicle By Parameters\n" +
            "0. Exit";
    }
}