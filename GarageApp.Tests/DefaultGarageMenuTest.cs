using AutoFixture;
using AutoFixture.AutoMoq;
using GarageApp.Handler;
using GarageApp.Loader;
using GarageApp.Manager;
using GarageApp.Menu;
using GarageApp.UI;
using GarageApp.Vehicles;
using Moq;

namespace GarageApp.Tests;

public class DefaultGarageMenuTests
{
    private const string DefaultGarageTitle = "Welcome to garage management!";
    // private const string CreateGaragePrompt = "Create new garage";
    private const string RNumberPrompt = "Enter registration number: ";
    private const string GarageIsEmpty = "Garage is empty";
    private const string InvalidInput = "Invalid";
    private const string MenuChoicePrintList = "3";
    private const string MenuChoicePrintStat = "4";
    private const string MenuChoiceFindByNum = "5";
    private const string MenuChoiceExit = "0";
    private const int GarageCapacity = 5;
    private const int PlacesLeft = 20;
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
        _fixture.Customize(new AutoMoqCustomization());
    }

    public Vehicle[] CreateVehicles(int capacity)
    {
        var vehicles = new Vehicle[capacity];

        for (int i = 0; i < capacity; i++)
            vehicles[i] = _fixture.Create<Mock<Vehicle>>().Object;

        return vehicles;
    }

    [Fact]
    public void Run_PrintsTitle()
    {
        _ui.Setup(x => x.ReadLine()).Returns(MenuChoiceExit); // Exit loop

        _menu.Run();
        _ui.Verify(x => x.WriteLine(It.Is<string>(s => s.Equals(DefaultGarageTitle))));
    }

    [Fact]
    public void Run_RunsMultipleTimes_UntilInputIsValid()
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
        _ui.Setup(x => x.ReadLine()).Returns(MenuChoiceExit); // Exit loop

        _menu.Run();

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
            _ui.Verify(x => x.WriteLine(item.ToString()), Times.Once);
    }

    [Fact]
    public void Run_PromtsForRNumber_WhenOptionFindByNumberIsChosen()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns(MenuChoiceFindByNum) // Choose Find by registration number
            .Returns(RegistrationNumber) // Enter number
            .Returns(MenuChoiceExit); // Exit

        _menu.Run();
        _ui.Verify(x => x.IndentedWriteLine(RNumberPrompt));
    }

    [Fact]
    public void Run_PrintsFindByRNumberResults_WhenOptionFindByNumberIsChosend()
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
    public void Run_ExitsLoop_IfExitOptionIsChose()
    {
        _ui.Setup(x => x.ReadLine()).Returns(MenuChoiceExit); // Exit loop

        _menu.Run();
        _ui.Verify(x => x.ReadLine(), Times.Once);
    }
}