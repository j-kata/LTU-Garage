using GarageApp.Handler;
using GarageApp.Vehicles;
using Moq;

namespace GarageApp.Tests;

public class GarageHandlerTests()
{
    private readonly Mock<Garage<Vehicle>> _garageNull;
    private readonly Mock<Garage<Vehicle>> _garage = new();
    private readonly GarageHandler _handler = new();

    [Fact]
    public void HasGarage_ReturnsFalseIfGarageNotCreated()
    {
        Assert.False(_handler.HasGarage());
    }

    [Fact]
    public void HasGarage_ReturnsTrueIfGaragePresent()
    {
        Assert.False(_handler.HasGarage());
    }
}