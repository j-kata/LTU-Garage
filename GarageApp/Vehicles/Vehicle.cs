using GarageApp.Types;
using GarageApp.Extensions;

namespace GarageApp.Vehicles;

public abstract class Vehicle : IVehicle
{
    public string Brand { get; }
    public string Model { get; }
    public string RegistrationNumber { get; }
    public ColorType Color { get; }
    public int WheelsNumber { get; }
    public abstract VehicleType Type { get; }

    // TODO: restrict registration number
    public Vehicle(string registrationNumber, string brand, string model, ColorType color, int wheelsNumber)
    {
        RegistrationNumber = registrationNumber.NotEmpty(nameof(registrationNumber));
        Brand = brand.NotEmpty(nameof(brand));
        Model = model.NotEmpty(nameof(model));
        Color = color;
        WheelsNumber = wheelsNumber.NotNegative(nameof(wheelsNumber));
    }

    public override string ToString()
    {
        return $"{Type}: {Brand} {Model}, {Color} [{RegistrationNumber}]";
    }
}
