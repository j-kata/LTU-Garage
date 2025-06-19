using GarageApp.Handler;
using GarageApp.UI;
using GarageApp.Helpers;
using GarageApp.Vehicles;
using GarageApp.Types;
using GarageApp.Extensions;

namespace GarageApp.Menu;

public class FilterVehicleMenu : BaseMenu
{
    private const string TypeChoice = "1";
    private const string BrandChoice = "2";
    private const string ModelChoice = "3";
    private const string ColorChoice = "4";
    private const string WheelsNumberChoice = "5";
    private const string ApplyFilterChoice = "6";
    private const string ReturnChoice = "0";

    private readonly Dictionary<string, (string name, Action filter)> _createOptions;
    private VehicleFilterData filterData = new();

    public FilterVehicleMenu(IUI ui, IHandler handler) : base(ui, handler)
    {
        _createOptions = new()
        {
            { TypeChoice, ("Type", FilterByType) },
            { BrandChoice, ("Brand", FilterByBrand) },
            { ModelChoice, ("Model", FilterByModel) },
            { ColorChoice, ("Color", FilterByColor) },
            { WheelsNumberChoice, ("Number of wheels", FilterByWheels) },
            { ApplyFilterChoice, ("Apply filters", ApplyFilters) },
            { ReturnChoice, ("Return", () => { } ) },
        };
    }

    public override string Title { get; } = "Filter vehicles by:";

    public override void Show()
    {
        _ui.WriteLine();

        foreach (var option in _createOptions)
            _ui.WriteLine($"{option.Key}. {option.Value.name}");
    }

    public override bool HandleChoice(string choice)
    {
        if (choice == ReturnChoice)
            return false;

        // Return to previous menu if Apply was chosen, otherwise keep choosing filters
        if (_createOptions.TryGetValue(choice, out var action))
        {
            action.filter();
            return choice != ApplyFilterChoice;
        }

        _ui.WriteLine("Unknown command. Try again.");
        return true;
    }

    private void ApplyFilters()
    {
        foreach (var v in _handler.FilterVehicles(filterData))
            _ui.WriteLine(v);
    }

    private void FilterByWheels()
    {
        filterData.WheelsNumber = Util.PromptUntilValidNumber(_ui, "Enter number of wheels: ", (x) => x >= 0);
    }

    private void FilterByColor()
    {
        filterData.Color = Util.PromptUnilValidEnumChoice<ColorType>(_ui, "Choose color: ");
    }

    private void FilterByModel()
    {
        filterData.Brand = Util.PromptUntilValidString(_ui, "Enter brand: ");
    }

    private void FilterByBrand()
    {
        filterData.Brand = Util.PromptUntilValidString(_ui, "Enter model: ");
    }

    private void FilterByType()
    {
        filterData.Type = Util.PromptUnilValidEnumChoice<VehicleType>(_ui, "Choose type: ");
    }
}