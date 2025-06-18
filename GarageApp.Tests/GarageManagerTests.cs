using GarageApp.Handler;
using GarageApp.Loader;
using GarageApp.Manager;
using GarageApp.UI;
using GarageApp.Vehicles;
using Moq;

namespace GarageApp.Tests;

public class GarageManagerTests
{
    private const string EmptyGarageTitle = "No garage is chosen";
    private const string DefaultGarageTitle = "Welcome to garage management!";
    private const string MenuChoiceCreateGarage = "1";
    private const string MenuChoiceExit = "0";
    private const int GarageCapacity = 5;

    private readonly Mock<IUI> _ui = new();
    private readonly Mock<IHandler> _handler = new();
    private readonly Mock<ILoader<Vehicle>> _loader = new();
    private GarageManager _manager;

    public GarageManagerTests()
    {
        _manager = new(_ui.Object, _handler.Object, _loader.Object);
    }

    [Fact]
    public void Run_Exits_IfKeepRunnigIsFalse()
    {
        _handler.Setup(h => h.HasGarage()).Returns(true);
        _ui.Setup(x => x.ReadLine()).Returns(MenuChoiceExit); // Exit loop

        _manager.Run();
        _ui.Verify(x => x.ReadLine(), Times.Once);
    }

    [Fact]
    public void Run_ShowsEmptyGarageMenu_IfNoGaragePresent()
    {
        _handler.Setup(h => h.HasGarage()).Returns(false); // Garage was not created
        _ui.Setup(x => x.ReadLine()).Returns(MenuChoiceExit); // Exit

        _manager.Run();
        _ui.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains(EmptyGarageTitle))));
    }

    [Fact]
    public void Run_ShowsDefaultGarageMenu_IfGaragePresent()
    {
        _handler.Setup(h => h.HasGarage()).Returns(true); // Garage is present
        _ui.Setup(x => x.ReadLine()).Returns(MenuChoiceExit); // Exit

        _manager.Run();
        _ui.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains(DefaultGarageTitle))));
    }

    [Fact]
    public void Run_RunsMultipleTime_IfKeepRunnigIsTrue()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns(MenuChoiceCreateGarage)   // Create new garage
            .Returns(GarageCapacity.ToString)  // Enter garage size
            .Returns(MenuChoiceExit); // Exit

        _handler.SetupSequence(h => h.HasGarage())
            .Returns(false) // No garage on first iteration
            .Returns(true); // Garage was created

        _manager.Run();
        _ui.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains(EmptyGarageTitle))));
        _ui.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains(DefaultGarageTitle))));
        _handler.Verify(h => h.CreateGarage(GarageCapacity), Times.Once);
    }
}