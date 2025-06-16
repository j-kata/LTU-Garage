using Garage.Types;
using Garage.Vehicles;

namespace Garage.Tests.Vehicles;

public class BusTests
{
    private const string Brand = "Mercedes-Benz";
    private const string Model = "Citaro";
    private const string RNumber = "Devil666";
    private const int WheelsNumber = 6;

    private const bool HasToilet = false;
    private const int SeatsNumber = 44;

    public static Bus CreateBus(
        string rNumber = RNumber,
        string brand = Brand,
        string model = Model,
        ColorType color = ColorType.White,
        int wheelsNumber = WheelsNumber,
        bool hasToilet = HasToilet,
        int seatsNumber = SeatsNumber
    )
    {
        return new Bus(
            brand: brand,
            model: model,
            registrationNumber: rNumber,
            color: color,
            wheelsNumber: wheelsNumber,
            hasToilet: hasToilet,
            seatsNumber: seatsNumber
        );
    }

    [Fact]
    public void Constructor_InitializesProperties()
    {
        var bus = CreateBus();

        Assert.Equal(Brand, bus.Brand);
        Assert.Equal(Model, bus.Model);
        Assert.Equal(RNumber, bus.RegistrationNumber);
        Assert.Equal(ColorType.White, bus.Color);
        Assert.Equal(WheelsNumber, bus.WheelsNumber);
        // Bus properties
        Assert.Equal(HasToilet, bus.HasToilet);
        Assert.Equal(SeatsNumber, bus.SeatsNumber);
    }


    [Fact]
    public void Constuctor_ThrowsArgumentException_IfSeatsNumberIsNegative()
    {
        Assert.Throws<ArgumentException>(() => CreateBus(seatsNumber: -9));
    }
}
