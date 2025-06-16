using Garage.Types;
using Garage.Vehicles;

namespace Garage.Tests.Vehicles;

public class CarTests
{
    private const string Brand = "Ford";
    private const string Model = "Mustang";
    private const string RNumber = "Devil666";
    private const uint WheelsNumber = 4;
    private const uint DoorsNumber = 4;

    public static Car CreateCar(
        string rNumber = RNumber,
        string brand = Brand,
        string model = Model,
        ColorType color = ColorType.White,
        uint wheelsNumber = WheelsNumber,
        FuelType fuelType = FuelType.Gasoline,
        uint doorsNumber = DoorsNumber
    )
    {
        return new Car(
            brand: brand,
            model: model,
            registrationNumber: rNumber,
            color: color,
            numberOfWheels: wheelsNumber,
            fuelType: fuelType,
            numberOfDoors: doorsNumber
        );
    }

    [Fact]
    public void Constructor_InitializesProperties()
    {
        var car = CreateCar();

        Assert.Equal(Brand, car.Brand);
        Assert.Equal(Model, car.Model);
        Assert.Equal(RNumber, car.RegistrationNumber);
        Assert.Equal(ColorType.White, car.Color);
        Assert.Equal(WheelsNumber, car.NumberOfWheels);
        Assert.Equal(DoorsNumber, car.NumberOfDoors);
        Assert.Equal(FuelType.Gasoline, car.FuelType);
    }

    [Fact]
    public void Constuctor_ThrowsArgumentException_IfRegistrationNumberIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => CreateCar(rNumber: "  "));
    }

    [Fact]
    public void Constuctor_ThrowsArgumentException_IfBrandIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => CreateCar(brand: "  "));
    }

    [Fact]
    public void Constuctor_ThrowsArgumentException_IfModelIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => CreateCar(model: "  "));
    }

    [Fact]
    public void GetType_ReturnsClassName()
    {
        var vehicle = CreateCar();

        Assert.Equal("Car", vehicle.Type);
    }

    [Fact]
    public void Constuctor_ThrowsArgumentException_IfNumbeOfDoorsIsZero()
    {
        Assert.Throws<ArgumentException>(() => CreateCar(doorsNumber: 0));
    }
}
