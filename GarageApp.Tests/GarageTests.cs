using AutoFixture;
using AutoFixture.AutoMoq;
using GarageApp.Types;
using GarageApp.Vehicles;
using Moq;

namespace GarageApp.Tests;

public class GarageTests
{
    private const int Capacity = 10;
    private const int SeedSize = 5;
    private const string InvalidNumber = "Invalid number";

    private readonly Garage<Vehicle> _emptyGarage;
    private readonly Garage<Vehicle> _halfFullGarage;
    private readonly Garage<Vehicle> _fullGarage;
    private Vehicle _validVehicle;
    private int _validIndex;
    private string _validNumber;

    private readonly Fixture _fixture;


    public GarageTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new AutoMoqCustomization());

        _emptyGarage = CreateEmptyGarage(Capacity);
        _halfFullGarage = CreateGarageWithSeed(Capacity, SeedSize);
        _fullGarage = CreateGarageWithSeed(Capacity);

        _validIndex = 1;
        _validVehicle = _fullGarage[_validIndex]!;
        _validNumber = _validVehicle.RegistrationNumber;
    }

    public Car CreateCar() => _fixture.Create<Car>();

    public Vehicle[] CreateVehicles(int capacity)
    {
        var vehicles = new Vehicle[capacity];

        for (int i = 0; i < capacity; i++)
            vehicles[i] = _fixture.Create<Mock<Vehicle>>().Object;

        return vehicles;
    }

    public static Garage<Vehicle> CreateEmptyGarage(int capacity)
    {
        return new Garage<Vehicle>(capacity);
    }

    public Garage<Vehicle> CreateGarageWithSeed(int seedSize)
    {
        var seed = CreateVehicles(seedSize);
        return new Garage<Vehicle>(seed);
    }

    public Garage<Vehicle> CreateGarageWithSeed(int capacity, int seedSize)
    {
        var seed = CreateVehicles(seedSize);
        return new Garage<Vehicle>(capacity, seed);
    }

    [Fact]
    public void Constructor_InitializesGarage_WhenCapacityIsValid()
    {
        Assert.Equal(Capacity, _emptyGarage.Capacity);
        Assert.Empty(_emptyGarage.GetVehicles());
    }

    [Fact]
    public void Constructor_ThrowsArgumentException_WhenCapacityIsZeroOrNegative()
    {
        Assert.Throws<ArgumentException>(() => CreateEmptyGarage(0));
        Assert.Throws<ArgumentException>(() => CreateEmptyGarage(-10));
    }

    [Fact]
    public void IsEmpty_ReturnsTrue_WhenCreatedWithNoSeed()
    {
        Assert.True(_emptyGarage.IsEmpty);
    }

    [Fact]
    public void PlacesLeft_EqualsCapacity_WhenCreatedWithNoSeed()
    {
        Assert.Equal(Capacity, _emptyGarage.PlacesLeft);
    }

    [Fact]
    public void Constructor_WithSeed_InitializesGarageWithVehicles()
    {
        var seed = CreateVehicles(SeedSize);
        var garage = new Garage<Vehicle>(seed);

        Assert.Equal(seed[0], garage[0]);
    }

    [Fact]
    public void Constructor_WithSeed_SetsCapacityToSeedLength()
    {
        Assert.Equal(Capacity, _fullGarage.Capacity);
    }

    [Fact]
    public void IsFull_ReturnsTrue_WhenSeedFillsCapacity()
    {
        Assert.True(_fullGarage.IsFull);
    }

    [Fact]
    public void Count_EqualsSeedLength_WhenSeedFillsCapacity()
    {
        Assert.Equal(Capacity, _fullGarage.Count);
    }

    [Fact]
    public void Constructor_WithSeedSmallerThenCapactity_WorksAsExpected()
    {
        var garage = CreateGarageWithSeed(Capacity, SeedSize);

        Assert.Equal(SeedSize, garage.Count);
        Assert.Equal(Capacity, garage.Capacity);
    }

    [Fact]
    public void Constructor_WithSeedLargerThenCapactity_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CreateGarageWithSeed(0, SeedSize));
    }

    [Fact]
    public void PlacesLeft_ReturnsExpected_WhenCapacityExceedSeed()
    {
        Assert.Equal(Capacity - SeedSize, _halfFullGarage.PlacesLeft);
    }

    [Fact]
    public void IsEmpty_ReturnsFalse_WhenCapacityExceedSeed()
    {
        Assert.False(_halfFullGarage.IsEmpty);
    }

    [Fact]
    public void IsFull_ReturnsFalse_WhenCapacityExceedSeed()
    {
        Assert.False(_halfFullGarage.IsFull);
    }

    [Fact]
    public void GetVehicles_ReturnsEmptyCollection_WhenGarageIsEmpty()
    {
        Assert.Empty(_emptyGarage.GetVehicles());
    }

    [Fact]
    public void GetVehicles_ReturnsAllVehicles_WhenGarageIsFull()
    {
        var vehicles = CreateVehicles(Capacity);
        var garage = new Garage<Vehicle>(vehicles); ;

        Assert.Equal(vehicles, garage.GetVehicles());
    }

    [Fact]
    public void GetVehicles_ReturnsAllNotNullVehicles()
    {
        var vehicles = _halfFullGarage.GetVehicles();

        Assert.DoesNotContain(null, vehicles);
        Assert.Equal(SeedSize, vehicles.Count());
    }

    [Fact]
    public void GetVehiclesTypeCount_ReturnsExpectedResult()
    {
        int carNum = 5, planeNum = 4, busNum = 1;

        Vehicle[] vehicles = [
            .._fixture.CreateMany<Car>(carNum),
            _fixture.Create<Bus>(),
            .._fixture.CreateMany<Airplane>(planeNum)
        ];

        var garage = new Garage<Vehicle>(vehicles);
        var typeCount = garage.GetVehiclesTypeCount();

        Assert.Contains((VehicleType.Car, carNum), typeCount);
        Assert.Contains((VehicleType.Bus, busNum), typeCount);
        Assert.Contains((VehicleType.Airplane, planeNum), typeCount);
    }

    [Fact]
    public void TryFindByRegistrationNumber_ReturnsTrueAndIndex_IfVehicleFound()
    {
        Assert.True(_fullGarage.TryFindByRegistrationNumber(_validNumber, out var index));
        Assert.Equal(index, _validIndex);
    }

    [Fact]
    public void TryFindByRegistrationNumber_ReturnsFalseAndInvalidIndex_IfVehicleNotFound()
    {
        Assert.False(_fullGarage.TryFindByRegistrationNumber(InvalidNumber, out var index));
        Assert.Equal(-1, index);
    }

    [Fact]
    public void TryFindByRegistrationNumber_ThrowsArgumentException_IfNumberIsNull()
    {
        Assert.Throws<ArgumentException>(() => _fullGarage.TryFindByRegistrationNumber(null, out var _));
    }

    [Fact]
    public void FindByRegistrationNumber_ReturnsTrueAndInfo_IfVehicleFound()
    {
        (bool success, string message) = _fullGarage.FindByRegistrationNumber(_validNumber);
        Assert.True(success);
        Assert.Equal(_validVehicle.ToString(), message);
    }

    [Fact]
    public void FindByRegistrationNumber_ReturnsFalseAndCorrectMessage_IfVehicleNotFound()
    {
        (bool success, string message) = _fullGarage.FindByRegistrationNumber(InvalidNumber);
        Assert.False(success);
        Assert.Equal("Vehicle was not found", message);
    }

    [Fact]
    public void FindByRegistrationNumber_ThrowsArgumentException_IfNumberIsNull()
    {
        Assert.Throws<ArgumentException>(() => _fullGarage.FindByRegistrationNumber(null));
    }

    [Fact]
    public void Depart_ReturnsTrueAndCorrectMessage_IfVehicleWasFound()
    {
        (bool success, string message) = _fullGarage.Depart(_validNumber);
        Assert.True(success);
        Assert.Equal("Vehicle departed", message);
    }

    [Fact]
    public void Depart_RemovesVehicleFromGarage_IfVehicleWasFound()
    {
        _fullGarage.Depart(_validNumber);

        Assert.DoesNotContain(_validVehicle, _fullGarage.GetVehicles());
        Assert.Null(_fullGarage[_validIndex]);
    }

    [Fact]
    public void Depart_ReturnsFalseAndCorrectMessage_IfVehicleWasNotFound()
    {
        (bool success, string message) = _fullGarage.Depart(InvalidNumber);
        Assert.False(success);
        Assert.Equal("Vehicle was not found", message);
    }

    [Fact]
    public void Depart_ThrowsArgumentException_IfNumberIsNull()
    {
        Assert.Throws<ArgumentException>(() => _fullGarage.Depart(null));
    }

    [Fact]
    public void Park_ReturnsTrueAndCorrectMessage_IfVehicleWasParked()
    {
        var car = CreateCar();
        (bool success, string message) = _emptyGarage.Park(car);

        Assert.True(success);
        Assert.Equal($"The vehicle {car.RegistrationNumber} was parked", message);
    }

    [Fact]
    public void Park_ParksCarAsExpected_IfPossible()
    {
        var car = CreateCar();
        _emptyGarage.Park(car);

        Assert.Contains(car, _emptyGarage.GetVehicles());
    }

    [Fact]
    public void Park_ReturnsFalseAndCorrectMessage_IfGarageWasFull()
    {
        var car = CreateCar();
        (bool success, string message) = _fullGarage.Park(car);

        Assert.False(success);
        Assert.Equal($"The garage is full", message);
    }

    [Fact]
    public void Park_DoesNotParkCar_IfGarageWasFull()
    {
        var car = CreateCar();
        _fullGarage.Park(car);

        Assert.DoesNotContain(car, _fullGarage.GetVehicles());
    }

    [Fact]
    public void Park_ReturnsFalseAndMessage_IfVehicleIsDuplicate()
    {
        var vehicle = _halfFullGarage[0]!;
        (bool success, string message) = _halfFullGarage.Park(vehicle);

        Assert.False(success);
        Assert.Equal($"The vehicle is already parked", message);
    }

    [Fact]
    public void Park_DoesNotParkCar_IfVehicleIsDuplicate()
    {
        var vehicle = _halfFullGarage[0]!;
        _halfFullGarage.Park(vehicle);

        Assert.Equal(SeedSize, _halfFullGarage.GetVehicles().Count());
        Assert.Distinct(_halfFullGarage.GetVehicles());
    }

    [Fact]
    public void Park_ThrowsArgumentNotException_IfVehicleIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _emptyGarage.Park(null));
    }

    [Fact]
    public void TryFindSpot_ReturnsTrueAndIndex_WhenNullFound()
    {
        Assert.True(_halfFullGarage.TryFindSpot(out int index));
        Assert.Equal(SeedSize, index);
    }

    [Fact]
    public void TryFindSpot_ReturnsFalseAndInvalidIndex_WhenNullNotFound()
    {
        Assert.False(_fullGarage.TryFindSpot(out int index));
        Assert.Equal(-1, index);
    }

    [Fact]
    public void TryFindSpot_FindsRightIndex_AfterDepart()
    {
        _fullGarage.Depart(_validNumber);
        _fullGarage.TryFindSpot(out int index);

        Assert.Equal(_validIndex, index);
    }
}