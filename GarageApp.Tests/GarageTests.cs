using AutoFixture;
using AutoFixture.AutoMoq;
using GarageApp.Vehicles;
using Moq;

namespace GarageApp.Tests;

public class GarageTests
{
    private const int Capacity = 10;
    private const int SeedSize = 5;
    private readonly Fixture _fixture;

    public GarageTests()
    {
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

    // Constructor without seed
    [Fact]
    public void Constructor_InitializesGarage_WhenCapacityIsValid()
    {
        var garage = CreateEmptyGarage(Capacity);

        Assert.Equal(Capacity, garage.Capacity);
        Assert.Empty(garage.GetVehicles());
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
        var garage = CreateEmptyGarage(Capacity);

        Assert.True(garage.IsEmpty);
    }

    [Fact]
    public void PlacesLeft_EqualsCapacity_WhenCreatedWithNoSeed()
    {
        var garage = CreateEmptyGarage(Capacity);

        Assert.Equal(Capacity, garage.PlacesLeft);
    }

    // Constructor with seed
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
        var garage = CreateGarageWithSeed(SeedSize);

        Assert.Equal(SeedSize, garage.Capacity);
    }

    [Fact]
    public void IsFull_ReturnsTrue_WhenSeedFillsCapacity()
    {
        var garage = CreateGarageWithSeed(SeedSize);

        Assert.True(garage.IsFull);
    }

    [Fact]
    public void Count_EqualsSeedLength_WhenSeedFillsCapacity()
    {
        var garage = CreateGarageWithSeed(SeedSize);

        Assert.Equal(SeedSize, garage.Count);
    }

    // Constructor with capacity and seed 
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
        var garage = CreateGarageWithSeed(Capacity, SeedSize);

        Assert.Equal(Capacity - SeedSize, garage.PlacesLeft);
    }

    [Fact]
    public void IsEmpty_ReturnsFalse_WhenCapacityExceedSeed()
    {
        var garage = CreateGarageWithSeed(Capacity, SeedSize);

        Assert.False(garage.IsEmpty);
    }

    [Fact]
    public void IsFull_ReturnsFalse_WhenCapacityExceedSeed()
    {
        var garage = CreateGarageWithSeed(Capacity, SeedSize);

        Assert.False(garage.IsFull);
    }
}