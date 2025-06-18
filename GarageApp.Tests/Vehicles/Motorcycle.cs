using GarageApp.Types;
using GarageApp.Vehicles;

namespace GarageApp.Tests.Vehicles;

public class MotorcycleTests
{
    private const string Brand = "Harley-Davidson";
    private const string Model = "Street 750";
    private const string RNumber = "Devil666";
    private const int WheelsNumber = 2;

    private const bool HasSideCar = true;
    private const int CylinderVolume = 749;

    public static Motorcycle CreateMotorcycle(
        string rNumber = RNumber,
        string brand = Brand,
        string model = Model,
        ColorType color = ColorType.White,
        int wheelsNumber = WheelsNumber,
        bool hasSideCar = HasSideCar,
        int cylinderVolume = CylinderVolume
    )
    {
        return new Motorcycle(
            brand: brand,
            model: model,
            registrationNumber: rNumber,
            color: color,
            wheelsNumber: wheelsNumber,
            hasSideCar: hasSideCar,
            cylinderVolume: cylinderVolume
        );
    }

    [Fact]
    public void Constructor_InitializesProperties()
    {
        var mCycle = CreateMotorcycle();

        Assert.Equal(Brand, mCycle.Brand);
        Assert.Equal(Model, mCycle.Model);
        Assert.Equal(RNumber, mCycle.RegistrationNumber);
        Assert.Equal(ColorType.White, mCycle.Color);
        Assert.Equal(WheelsNumber, mCycle.WheelsNumber);
        // Motorcycle properties
        Assert.Equal(HasSideCar, mCycle.HasSideCar);
        Assert.Equal(CylinderVolume, mCycle.CylinderVolume);
    }

    [Fact]
    public void Constuctor_ThrowsArgumentException_IfCylinderVolumeIsZeroOrNegative()
    {
        Assert.Throws<ArgumentException>(() => CreateMotorcycle(cylinderVolume: 0));
        Assert.Throws<ArgumentException>(() => CreateMotorcycle(cylinderVolume: -5));
    }

    [Fact]
    public void Type_ReturnsMotorcycleType()
    {
        var vehicle = CreateMotorcycle();

        Assert.Equal(VehicleType.Motorcycle, vehicle.Type);
    }
}
