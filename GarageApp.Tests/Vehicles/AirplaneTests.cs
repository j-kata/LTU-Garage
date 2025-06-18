using GarageApp.Types;
using GarageApp.Vehicles;

namespace GarageApp.Tests.Vehicles;

public class AirplaneTests
{
    private const string Brand = "Cessna";
    private const string Model = "172 Skyhawk";
    private const string RNumber = "Devil666";
    private const int WheelsNumber = 3;

    private const int EnginesNumber = 1;
    private const double WingSpan = 11;

    public static Airplane CreateAirplane(
        string rNumber = RNumber,
        string brand = Brand,
        string model = Model,
        ColorType color = ColorType.White,
        int wheelsNumber = WheelsNumber,
        int enginesNumber = EnginesNumber,
        double wingSpan = WingSpan
    )
    {
        return new Airplane(
            brand: brand,
            model: model,
            registrationNumber: rNumber,
            color: color,
            wheelsNumber: wheelsNumber,
            enginesNumber: enginesNumber,
            wingSpan: wingSpan
        );
    }

    [Fact]
    public void Constructor_InitializesProperties()
    {
        var plane = CreateAirplane();

        Assert.Equal(Brand, plane.Brand);
        Assert.Equal(Model, plane.Model);
        Assert.Equal(RNumber, plane.RegistrationNumber);
        Assert.Equal(ColorType.White, plane.Color);
        Assert.Equal(WheelsNumber, plane.WheelsNumber);
        // Airplane properties
        Assert.Equal(EnginesNumber, plane.EnginesNumber);
        Assert.Equal(WingSpan, plane.WingSpan);
    }

    [Fact]
    public void Constuctor_ThrowsArgumentException_IfEngineNumberIsZeroOrNegative()
    {
        Assert.Throws<ArgumentException>(() => CreateAirplane(enginesNumber: -3));
        Assert.Throws<ArgumentException>(() => CreateAirplane(enginesNumber: 0));
    }

    [Fact]
    public void Constuctor_ThrowsArgumentException_IfWingSpanIsZeroOrNegative()
    {
        Assert.Throws<ArgumentException>(() => CreateAirplane(wingSpan: -2.3));
        Assert.Throws<ArgumentException>(() => CreateAirplane(wingSpan: 0));
    }

    [Fact]
    public void Type_ReturnsAirplaneType()
    {
        var vehicle = CreateAirplane();

        Assert.Equal(VehicleType.Airplane, vehicle.Type);
    }
}
