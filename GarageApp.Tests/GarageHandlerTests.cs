using GarageApp.Handler;
using GarageApp.Types;
using GarageApp.Vehicles;
using Moq;

namespace GarageApp.Tests;

public class GarageHandlerTests
{
    private const string InvalidOperation = "Garage was not created";
    private const string Message = "Message";
    private const string GarageIsEmpty = "Garage is empty";
    private readonly Mock<IGarage<Vehicle>> _garage = new();
    private readonly GarageHandler _handler = new();

    private readonly Car _car =
        new("0001", "Car", "Model", ColorType.Red, 4, FuelType.Gasoline, 5);

    private readonly Vehicle[] _vehicles =
        [
            new Car("0001", "Car", "Model", ColorType.Red, 4, FuelType.Gasoline, 5),
            new Boat("0002", "Boat", "Model", ColorType.Blue, BoatType.Bowrider, 5),
            new Airplane("0003", "Airplane", "Model", ColorType.Green, 2, 3, 11),
            new Motorcycle("004", "Motorcycle", "Model", ColorType.Black, 2, 750, true),
            new Bus("005", "Bus", "Model", ColorType.Red, 8, 44, true)
        ];

    [Fact]
    public void HasGarage_ReturnsFalseIfGarageNotCreated()
    {
        Assert.False(_handler.HasGarage());
    }

    [Fact]
    public void HasGarage_ReturnsTrueIfGaragePresent()
    {
        _handler.SetGarage(_garage.Object);
        Assert.True(_handler.HasGarage());
    }

    [Fact]
    public void CreateGarage_WithCapacity_CreatesGarage()
    {
        _handler.CreateGarage(10);
        Assert.True(_handler.HasGarage());
    }

    [Fact]
    public void CreateGarage_WithSeed_CreatesGarage()
    {
        _handler.CreateGarage(_vehicles);
        Assert.True(_handler.HasGarage());
    }

    [Fact]
    public void SetGarage_SetsGarage()
    {
        _handler.SetGarage(_garage.Object);
        Assert.True(_handler.HasGarage());
    }

    [Fact]
    public void GarageOverview_ReturnsInvalidOperation_IfGarageNotCreated()
    {
        var result = _handler.GarageOverview();

        Assert.Single(result);
        Assert.Equal(InvalidOperation, result.First());
    }

    [Fact]
    public void GarageOverview_ReturnsExpectedResults_IfGaragePresent()
    {
        int capacity = 10, free = 4, occupied = 6;

        _garage.Setup(g => g.Capacity).Returns(capacity);
        _garage.Setup(g => g.PlacesLeft).Returns(free);
        _garage.Setup(g => g.Count).Returns(occupied);

        _handler.SetGarage(_garage.Object);
        var result = _handler.GarageOverview();

        Assert.Contains($"Total places: {capacity}", result);
        Assert.Contains($"Free places: {free}", result);
        Assert.Contains($"Occupied: {occupied}", result);
    }

    [Fact]
    public void GetVehicleList_ReturnsEmpty_IfGarageNotCreated()
    {
        var result = _handler.GetVehicleList();

        Assert.Single(result);
        Assert.Equal(GarageIsEmpty, result.First());
    }

    [Fact]
    public void GetVehicleList_ReturnsEmpty_IfGarageIsEmpty()
    {
        _garage.Setup(g => g.IsEmpty).Returns(true);
        _handler.SetGarage(_garage.Object);

        var result = _handler.GetVehicleList();

        Assert.Single(result);
        Assert.Equal(GarageIsEmpty, result.First());
    }

    [Fact]
    public void GetVehicleList_ReturnsExpectedResult_IfGarageIsPresent()
    {
        var vehicleStrings = _vehicles.Select(s => s.ToString());

        _garage.Setup(g => g.IsEmpty).Returns(false);
        _garage.Setup(g => g.GetVehicles()).Returns(_vehicles);
        _handler.SetGarage(_garage.Object);

        var result = _handler.GetVehicleList();

        Assert.Equal(_vehicles.Select(s => s.ToString()), result);
    }

    [Fact]
    public void GetVehicleTypeStats_ReturnsEmpty_IfGarageNotCreated()
    {
        var result = _handler.GetVehicleTypeStats();

        Assert.Single(result);
        Assert.Equal(GarageIsEmpty, result.First());
    }

    [Fact]
    public void GetVehicleTypeStats_ReturnsEmpty_IfGarageIsEmpty()
    {
        _garage.Setup(g => g.IsEmpty).Returns(true);
        _handler.SetGarage(_garage.Object);

        var result = _handler.GetVehicleTypeStats();

        Assert.Single(result);
        Assert.Equal(GarageIsEmpty, result.First());
    }

    [Fact]
    public void GetVehicleTypeStats_ReturnsExpectedResult_IfGarageIsPresent()
    {
        IEnumerable<(VehicleType name, int count)> vehicleStat = [
            (VehicleType.Car, 5),
            (VehicleType.Airplane, 6),
        ];

        var vehicleStatStrings = vehicleStat.Select(v => $"{v.name}: {v.count}");

        _garage.Setup(g => g.IsEmpty).Returns(false);
        _garage.Setup(g => g.GetVehiclesTypeCount()).Returns(vehicleStat);
        _handler.SetGarage(_garage.Object);

        var result = _handler.GetVehicleTypeStats();

        Assert.All(result, item => Assert.Contains(item, vehicleStatStrings));
    }

    [Fact]
    public void FindByRegistration_ReturnsInvalidOperation_IfGarageNotCreated()
    {
        Assert.Equal(InvalidOperation, _handler.FindByRegistration("001"));
    }

    [Fact]
    public void FindByRegistration_ReturnsMessage_IfSuccess()
    {
        _garage.Setup(g => g.FindByRegistrationNumber("001"))
            .Returns((true, Message));
        _handler.SetGarage(_garage.Object);

        Assert.Equal(Message, _handler.FindByRegistration("001"));
    }

    [Fact]
    public void FindByRegistration_ReturnsMessage_IfFailure()
    {
        _garage.Setup(g => g.FindByRegistrationNumber("001"))
            .Returns((false, Message));
        _handler.SetGarage(_garage.Object);

        Assert.Equal(Message, _handler.FindByRegistration("001"));
    }

    [Fact]
    public void DepartVehicle_ReturnsInvalidOperation_IfGarageNotCreated()
    {
        Assert.Equal(InvalidOperation, _handler.DepartVehicle("001"));
    }

    [Fact]
    public void DepartVehicle_ReturnsMessage_IfSuccess()
    {
        _garage.Setup(g => g.Depart("001"))
            .Returns((true, Message));
        _handler.SetGarage(_garage.Object);

        Assert.Equal(Message, _handler.DepartVehicle("001"));
    }

    [Fact]
    public void DepartVehicle_ReturnsMessage_IfFailure()
    {
        _garage.Setup(g => g.Depart("001"))
            .Returns((false, Message));
        _handler.SetGarage(_garage.Object);

        Assert.Equal(Message, _handler.DepartVehicle("001"));
    }

    [Fact]
    public void ParkVehicle_ReturnsInvalidOperation_IfGarageNotCreated()
    {
        Assert.Equal(InvalidOperation, _handler.ParkVehicle(_car));
    }

    [Fact]
    public void ParkVehicle_ReturnsMessage_IfSuccess()
    {
        _garage.Setup(g => g.Park(_car)).Returns((true, Message));
        _handler.SetGarage(_garage.Object);

        Assert.Equal(Message, _handler.ParkVehicle(_car));
    }

    [Fact]
    public void ParkVehicle_ReturnsMessage_IfFailure()
    {
        _garage.Setup(g => g.Park(_car)).Returns((false, Message));
        _handler.SetGarage(_garage.Object);

        Assert.Equal(Message, _handler.ParkVehicle(_car));
    }

    [Fact]
    public void FilterVehicles_ReturnsAll_WhenNoFilterSet()
    {
        _garage.Setup(g => g.GetVehicles()).Returns(_vehicles);
        _handler.SetGarage(_garage.Object);

        var data = new VehicleFilterData();
        var result = _handler.FilterVehicles(data);

        Assert.Equal(_vehicles.Select(v => v.ToString()), result);
    }

    [Fact]
    public void FilterVehicles_FiltersByColor()
    {
        _garage.Setup(g => g.GetVehicles()).Returns(_vehicles);
        _handler.SetGarage(_garage.Object);

        var data = new VehicleFilterData { Color = ColorType.Red };
        var result = _handler.FilterVehicles(data);

        Assert.Equal(2, result.Count());
        Assert.Contains(_vehicles.First().ToString(), result);
        Assert.Contains(_vehicles.Last().ToString(), result);
    }

    [Fact]
    public void FilterVehicles_FiltersByColorAndWheelsNumber()
    {
        _garage.Setup(g => g.GetVehicles()).Returns(_vehicles);
        _handler.SetGarage(_garage.Object);

        var data = new VehicleFilterData
        {
            Color = ColorType.Black,
            WheelsNumber = 2
        };

        var result = _handler.FilterVehicles(data);

        Assert.Single(result);
        Assert.Contains(_vehicles[3].ToString(), result);
    }

    [Fact]
    public void FilterVehicles_FiltersByBrandAndModel_CaseIgnore()
    {
        _garage.Setup(g => g.GetVehicles()).Returns(_vehicles);
        _handler.SetGarage(_garage.Object);

        var data = new VehicleFilterData
        {
            Model = "MODEL",
            Brand = "AIRpl"
        };

        var result = _handler.FilterVehicles(data);

        Assert.Single(result);
        Assert.Contains(_vehicles[2].ToString(), result);
    }
}