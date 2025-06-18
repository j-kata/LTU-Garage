using GarageApp.Handler;
using GarageApp.Loader;
using GarageApp.Manager;
using GarageApp.Menu;
using GarageApp.UI;
using GarageApp.Vehicles;
using Moq;

namespace GarageApp.Tests;

public class EmptyGarageMenuTests
{
    private const string EmptyGarageTitle = "No garage is chosen";
    private const string CreateGaragePrompt = "Create new garage";
    private const string GarageSizePrompt = "Enter garage size: ";
    private const string InvalidInput = "Invalid";
    private const string MenuChoiceCreateGarage = "1";
    private const string MenuChoiceLoadGarage = "2";
    private const string MenuChoiceExit = "0";
    private const int GarageCapacity = 5;

    private readonly Mock<IUI> _ui = new();
    private readonly Mock<IHandler> _handler = new();
    private readonly Mock<ILoader<Vehicle>> _loader = new();
    private readonly EmptyGarageMenu _menu;

    public EmptyGarageMenuTests()
    {
        _menu = new(_ui.Object, _handler.Object, _loader.Object);
    }


    [Fact]
    public void Run_PrintsTitle()
    {
        _ui.Setup(x => x.ReadLine()).Returns(MenuChoiceExit); // Exit loop

        _menu.Run();
        _ui.Verify(x => x.WriteLine(It.Is<string>(s => s.Equals(EmptyGarageTitle))));
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
    public void Run_PromptsForGarageSize_WhenOptionCreateIsChosen()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns(MenuChoiceCreateGarage) // Choose create
            .Returns(GarageCapacity.ToString()) // Enter capacity
            .Returns(MenuChoiceExit); // Exit

        _menu.Run();
        _ui.Verify(x => x.IndentedWriteLine(It.Is<string>(s => s.Equals(GarageSizePrompt))));
    }

    [Fact]
    public void Run_ShowsPromptsForGarageSize_UntilInputIsValid()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns(MenuChoiceCreateGarage) // Choose create
            .Returns(InvalidInput) // Invalid capacity
            .Returns(GarageCapacity.ToString()) // Valid capacity
            .Returns(MenuChoiceExit); // Exit

        _menu.Run();
        _ui.Verify(x => x.IndentedWriteLine(It.Is<string>(s => s.Equals(GarageSizePrompt))), Times.Exactly(2));
    }

    [Fact]
    public void Run_ExitsLoop_WhenGarageIsCreated()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns(MenuChoiceCreateGarage)    // Choose create
            .Returns(GarageCapacity.ToString());  // Enter capacity

        _menu.Run();
        _handler.Verify(h => h.CreateGarage(GarageCapacity), Times.Once);
        _ui.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains(CreateGaragePrompt))), Times.Once);
    }

    [Fact]
    public void Run_ExitsLoop_WhenGarageIsLoaded()
    {
        var vehicles = new Vehicle[GarageCapacity];
        _loader.Setup(x => x.Load()).Returns(vehicles);

        _ui.SetupSequence(x => x.ReadLine()).Returns(MenuChoiceLoadGarage);    // Choose load

        _menu.Run();
        _handler.Verify(h => h.CreateGarage(vehicles), Times.Once);
        _ui.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains(CreateGaragePrompt))), Times.Once);
    }

    [Fact]
    public void Run_ExitsLoop_IfExitOptionIsChose()
    {
        _ui.Setup(x => x.ReadLine()).Returns(MenuChoiceExit); // Exit loop

        _menu.Run();
        _ui.Verify(x => x.ReadLine(), Times.Once);
    }
}