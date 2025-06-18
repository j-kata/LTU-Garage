using AutoFixture;
using GarageApp.Handler;
using GarageApp.Loader;
using GarageApp.Menu;
using GarageApp.Types;
using GarageApp.UI;
using GarageApp.Vehicles;
using Moq;

namespace GarageApp.Tests;

public class DefaultGarageMenuTests
{
    private const string DefaultGarageTitle = "Welcome to garage management!";
    private const string ParkMenuTitle = "Choose the type of vehicle you want to park:";
    private const string FilterMenuTitle = "Filter vehicles by:";
    private const string RNumberPrompt = "Enter registration number: ";
    private const string InvalidInput = "Invalid";
    private const string MenuChoicePark = "1";
    private const string MenuChoiceDepart = "2";
    private const string MenuChoicePrintList = "3";
    private const string MenuChoicePrintStat = "4";
    private const string MenuChoiceFindByNum = "5";
    private const string MenuChoiceFilter = "6";
    private const string MenuChoiceLoad = "7";
    private const string MenuChoiceExit = "0";
    private const string RegistrationNumber = "001";

    private readonly Mock<IUI> _ui = new();
    private readonly Mock<IHandler> _handler = new();
    private readonly Mock<ILoader<Vehicle>> _loader = new();
    private readonly DefaultGarageMenu _menu;
    private readonly Fixture _fixture;


    public DefaultGarageMenuTests()
    {
        _menu = new(_ui.Object, _handler.Object, _loader.Object);
        _fixture = new Fixture();

        _fixture.Customize<Car>(c => c.FromFactory(
            () => new Car("ABC", "Volvo", "XC60", ColorType.Red, 4, FuelType.Gasoline, 5)
        ));
    }

    [Fact]
    public void RunAndExit()
    {
        _ui.Setup(x => x.ReadLine()).Returns(MenuChoiceExit);
        _menu.Run();
    }

    [Fact]
    public void Run_PrintsTitle()
    {
        RunAndExit();
        _ui.Verify(x => x.WriteLine(DefaultGarageTitle));
    }

    [Fact]
    public void Run_ExitsLoop_IfExitOptionIsChosen()
    {
        RunAndExit();
        _ui.Verify(x => x.ReadLine(), Times.Once);
    }

    [Fact]
    public void Run_RetriesInput_UntilValidMenuChoice()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns(InvalidInput) // Invalid input
            .Returns(InvalidInput) // Invalid input
            .Returns(MenuChoiceExit); // Exit

        _menu.Run();
        _ui.Verify(x => x.ReadLine(), Times.Exactly(3));
    }

    [Fact]
    public void Run_PrintsStatistics_WithExpectedValues()
    {
        string[] overview = [
            "Total places: 7",
            "Free places: 4",
            "Occupied: 3"
        ];

        _handler.Setup(x => x.GarageOverview()).Returns(overview);

        RunAndExit();

        foreach (var item in overview)
            _ui.Verify(x => x.WriteLine(item), Times.Once);
    }

    [Fact]
    public void Run_PrintsVehiclesList_WhenOptionPrintListIsChosen()
    {
        string[] list = [
            "Car: Brand Model, Color [Number]",
            "Bus: Brand Model, Color [Number]"
        ];

        _handler.Setup(x => x.ListVehicles()).Returns(list);

        _ui.SetupSequence(x => x.ReadLine())
            .Returns(MenuChoicePrintList) // Choose Print
            .Returns(MenuChoiceExit); // Exit

        _menu.Run();

        foreach (var item in list)
            _ui.Verify(x => x.WriteLine(item), Times.Once);
    }

    [Fact]
    public void Run_PrintsVehiclesTypeStats_WhenOptionPrintStatIsChosen()
    {
        string[] stat = ["Car: 2", "Bus: 3"];
        _handler.Setup(x => x.VehicleTypeStats()).Returns(stat);

        _ui.SetupSequence(x => x.ReadLine())
            .Returns(MenuChoicePrintStat) // Choose Print Statistics
            .Returns(MenuChoiceExit); // Exit

        _menu.Run();

        foreach (var item in stat)
            _ui.Verify(x => x.WriteLine(item), Times.Once);
    }

    [Fact]
    public void Run_PrintsFindByRNumberResults_WhenOptionFindByNumberIsChosen()
    {
        var result = "Car: Brand Model, Color [Number]";
        _handler.Setup(x => x.FindByRegistration(RegistrationNumber)).Returns(result);

        _ui.SetupSequence(x => x.ReadLine())
            .Returns(MenuChoiceFindByNum) // Choose Find by registration number
            .Returns(RegistrationNumber) // Enter number
            .Returns(MenuChoiceExit); // Exit

        _menu.Run();
        _ui.Verify(x => x.WriteLine(result));
    }

    [Fact]
    public void Run_PrintsDepartResults_WhenOptionDepartIsChosen()
    {
        var result = "Car: Brand Model, Color [Number]";
        _handler.Setup(x => x.DepartVehicle(RegistrationNumber)).Returns(result);

        _ui.SetupSequence(x => x.ReadLine())
            .Returns(MenuChoiceDepart) // Choose Depart
            .Returns(RegistrationNumber) // Enter number
            .Returns(MenuChoiceExit); // Exit

        _menu.Run();
        _ui.Verify(x => x.WriteLine(result));
    }

    [Fact]
    public void Run_PrintsLoadedVehicles_WhenOptionLoadIsChosen()
    {
        string[] result = ["Vehicle [N1] was parked", "Vehicle [N1] was parked"];
        Vehicle[] vehicles = [_fixture.Create<Car>()];
        _loader.Setup(x => x.Load()).Returns(vehicles);

        _handler.SetupSequence(x => x.ParkVehicle(vehicles[0]))
            .Returns(result[0]).Returns(result[1]);

        _ui.SetupSequence(x => x.ReadLine())
            .Returns(MenuChoiceLoad) // Choose Load
            .Returns(MenuChoiceExit); // Exit

        _menu.Run();

        foreach (var r in result)
            _ui.Verify(x => x.WriteLine(r), Times.Once);
    }

    [Theory]
    [InlineData(MenuChoicePark, ParkMenuTitle)]
    [InlineData(MenuChoiceFilter, FilterMenuTitle)]
    public void Run_PrintsCorrectMenuTitle(string option, string title)
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns(option) // Choose option
            .Returns(MenuChoiceExit) // Return
            .Returns(MenuChoiceExit); // Exit

        _menu.Run();
        _ui.Verify(x => x.WriteLine(title), Times.Once);
    }

    [Theory]
    [InlineData(MenuChoiceDepart, RNumberPrompt)]
    [InlineData(MenuChoiceFindByNum, RNumberPrompt)]
    public void Run_PrintsCorrectPrompt(string option, string title)
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns(option) // Choose option
            .Returns(RegistrationNumber) // Enter number
            .Returns(MenuChoiceExit); // Exit

        _menu.Run();
        _ui.Verify(x => x.IndentedWriteLine(title));
    }
}