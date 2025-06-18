using GarageApp.Types;
using GarageApp.Vehicles;

namespace GarageApp.Tests.Vehicles;

public class CarTests
{
    private const string Brand = "Ford";
    private const string Model = "Mustang";
    private const string RNumber = "Devil666";
    private const int WheelsNumber = 4;
    private const int DoorsNumber = 4;

    public static Car CreateCar(
        string rNumber = RNumber,
        string brand = Brand,
        string model = Model,
        ColorType color = ColorType.White,
        int wheelsNumber = WheelsNumber,
        FuelType fuelType = FuelType.Gasoline,
        int doorsNumber = DoorsNumber
    )
    {
        return new Car(
            brand: brand,
            model: model,
            registrationNumber: rNumber,
            color: color,
            wheelsNumber: wheelsNumber,
            fuelType: fuelType,
            doorsNumber: doorsNumber
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
        Assert.Equal(WheelsNumber, car.WheelsNumber);
        // Car properties
        Assert.Equal(DoorsNumber, car.DoorsNumber);
        Assert.Equal(FuelType.Gasoline, car.FuelType);
    }

    // Common Properties
    [Fact]
    public void Constuctor_ThrowsArgumentException_IfRegistrationNumberIsNullOrEmpty()
    {
        Assert.Throws<ArgumentException>(() => CreateCar(rNumber: "  "));
        Assert.Throws<ArgumentException>(() => CreateCar(rNumber: null));
        Assert.Throws<ArgumentException>(() => CreateCar(rNumber: new string('a', 11)));
    }

    // Common Properties
    [Fact]
    public void Constuctor_ThrowsArgumentException_IfBrandIsInvalid()
    {
        Assert.Throws<ArgumentException>(() => CreateCar(brand: "  "));
        Assert.Throws<ArgumentException>(() => CreateCar(brand: null));
        Assert.Throws<ArgumentException>(() => CreateCar(brand: new string('a', 31)));
    }

    // Common Properties
    [Fact]
    public void Constuctor_ThrowsArgumentException_IfModelIsInvalid()
    {
        Assert.Throws<ArgumentException>(() => CreateCar(model: "  "));
        Assert.Throws<ArgumentException>(() => CreateCar(model: null));
        Assert.Throws<ArgumentException>(() => CreateCar(model: new string('a', 50)));
    }


    // Common Properties
    [Fact]
    public void Constuctor_ThrowsArgumentException_IfWheelsNumberIsNegative()
    {
        Assert.Throws<ArgumentException>(() => CreateCar(wheelsNumber: -3));
    }

    // Car Properties
    [Fact]
    public void Type_ReturnsCarType()
    {
        var vehicle = CreateCar();

        Assert.Equal(VehicleType.Car, vehicle.Type);
    }

    // Car properties
    [Fact]
    public void Constuctor_ThrowsArgumentException_IfDoorsNumberIsZeroOrNegative()
    {
        Assert.Throws<ArgumentException>(() => CreateCar(doorsNumber: 0));
        Assert.Throws<ArgumentException>(() => CreateCar(doorsNumber: -5));
    }
}
