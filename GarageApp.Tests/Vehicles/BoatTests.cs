using GarageApp.Types;
using GarageApp.Vehicles;

namespace GarageApp.Tests.Vehicles;

public class BoatTests
{
    private const string Brand = "Bayliner";
    private const string Model = "Element E18";
    private const string RNumber = "Devil666";
    private const int WheelsNumber = 0;

    private const BoatType Type = BoatType.Bowrider;
    private const double Length = 5.49;

    public static Boat CreateBoat(
        string rNumber = RNumber,
        string brand = Brand,
        string model = Model,
        ColorType color = ColorType.White,
        BoatType boatType = Type,
        double length = Length
    )
    {
        return new Boat(
            brand: brand,
            model: model,
            registrationNumber: rNumber,
            color: color,
            boatType: boatType,
            length: length
        );
    }

    [Fact]
    public void Constructor_InitializesProperties()
    {
        var boat = CreateBoat();

        Assert.Equal(Brand, boat.Brand);
        Assert.Equal(Model, boat.Model);
        Assert.Equal(RNumber, boat.RegistrationNumber);
        Assert.Equal(ColorType.White, boat.Color);
        Assert.Equal(WheelsNumber, boat.WheelsNumber);
        // Boat properties
        Assert.Equal(Type, boat.BoatType);
        Assert.Equal(Length, boat.Length);
    }

    [Fact]
    public void Constuctor_ThrowsArgumentException_IfLengthIsZeroOrNegative()
    {
        Assert.Throws<ArgumentException>(() => CreateBoat(length: -9.4));
        Assert.Throws<ArgumentException>(() => CreateBoat(length: 0));
    }
}
