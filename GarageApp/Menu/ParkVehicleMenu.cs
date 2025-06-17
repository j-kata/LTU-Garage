using GarageApp.Handler;
using GarageApp.UI;
using GarageApp.Helpers;
using GarageApp.Vehicles;
using GarageApp.Types;
using GarageApp.Extensions;

namespace GarageApp.Menu;

public class ParkVehicleMenu : BaseMenu
{
    private const string CarChoice = "1";
    private const string MotorcycleChoice = "2";
    private const string AirplaneChoice = "3";
    private const string BoatChoice = "4";
    private const string BusChoice = "5";
    private const string ReturnChoice = "0";
    private readonly string[] YesNo = ["y", "n"];

    private readonly Dictionary<string, (string name, Func<Vehicle> create)> _createOptions;

    public ParkVehicleMenu(IUI ui, IHandler handler) : base(ui, handler)
    {
        _createOptions = new()
        {
            { CarChoice, ("Car", CreateCar) },
            { MotorcycleChoice, ("Motorcycle", CreateMotorcycle) },
            { AirplaneChoice, ("Airplane", CreateAirplane) },
            { BoatChoice, ("Boat", CreateBoat) },
            { BusChoice, ("Bus", CreateBus) },
        };
    }

    public override string Title { get; } = "Choose the type of vehicle you want to park:";

    public override void Show()
    {
        _ui.WriteLine();

        foreach (var option in _createOptions)
            _ui.WriteLine($"{option.Key}. {option.Value.name}");

        _ui.WriteLine($"{ReturnChoice}. Return");
    }

    public override bool HandleChoice(string choice)
    {
        if (choice == ReturnChoice)
        {
            ContinueToNextMenu = false;
            return false;
        }

        if (_createOptions.TryGetValue(choice, out var func))
        {
            var vehicle = func.create();
            _ui.WriteLine(_handler.ParkVehicle(vehicle));
            return false;
        }

        _ui.WriteLine("Unknown command. Try again.");
        return true;
    }

    private Vehicle CreateCar()
    {
        var commonData = PromptCommonData();
        var fuelType = Util.PromptUnilValidEnumChoice<FuelType>(_ui, "Choose fuel type: ");
        var doorsNumber = Util.PromptUntilValidNumber(_ui, "Enter number of doors: ", (x) => x > 0);

        return VehicleFactory.CreateCar(commonData, fuelType, doorsNumber);
    }

    private Vehicle CreateMotorcycle()
    {
        var commonData = PromptCommonData();
        var cylinderVolume = Util.PromptUntilValidNumber(_ui, "Enter cylinder volume: ", (x) => x > 0);
        var hasSideCar = Util.PromptUntilValidString(_ui, "Has side car? y/n ", (x) => YesNo.Contains(x.ToLower()));

        return VehicleFactory.CreateMotorcycle(commonData, cylinderVolume, hasSideCar.EqualsCaseIgnore("y"));
    }

    private Vehicle CreateAirplane()
    {
        var commonData = PromptCommonData();
        var enginesNumber = Util.PromptUntilValidNumber(_ui, "Enter number of engines: ", (x) => x > 0);
        var wingSpan = Util.PromptUntilValidDouble(_ui, "Enter wing span: ", (x) => x > 0);

        return VehicleFactory.CreateAirplane(commonData, enginesNumber, wingSpan);
    }

    private Vehicle CreateBoat()
    {
        var commonData = PromptCommonData();
        var boatType = Util.PromptUnilValidEnumChoice<BoatType>(_ui, "Choose boat type: ");
        var length = Util.PromptUntilValidDouble(_ui, "Enter length: ", (x) => x > 0);

        return VehicleFactory.CreateBoat(commonData, boatType, length);
    }

    private Vehicle CreateBus()
    {
        var commonData = PromptCommonData();
        var seatsNumber = Util.PromptUntilValidNumber(_ui, "Enter number of seats: ", (x) => x >= 0);
        var hasToilet = Util.PromptUntilValidString(_ui, "Has toilet? y/n ", (x) => YesNo.Contains(x.ToLower()));

        return VehicleFactory.CreateBus(commonData, seatsNumber, hasToilet.EqualsCaseIgnore("y"));
    }

    private VehicleCommonData PromptCommonData()
    {
        var rNumber = Util.PromptUntilValidString(_ui, "Enter registration number: ");
        var brand = Util.PromptUntilValidString(_ui, "Enter brand: ");
        var model = Util.PromptUntilValidString(_ui, "Enter model: ");
        var color = Util.PromptUnilValidEnumChoice<ColorType>(_ui, "Choose color: ");
        var wheelsNumber = Util.PromptUntilValidNumber(_ui, "Enter number of wheels: ", (x) => x >= 0);

        return new VehicleCommonData(rNumber, brand, model, color, wheelsNumber);
    }
}