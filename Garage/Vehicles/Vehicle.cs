using Garage.Types;
using Garage.Extensions;

namespace Garage.Vehicles;

public abstract class Vehicle : IVehicle
{
    public string Brand { get; }
    public string Model { get; }
    public string RegistrationNumber { get; }
    public Color Color { get; }
    public uint NumberOfWheels { get; }
    public string Type => GetType().Name;

    public Vehicle(string registrationNumber, string brand, string model, Color color, uint numberOfWheels)
    {
        RegistrationNumber = registrationNumber.NonEmpty(nameof(registrationNumber));
        Brand = brand.NonEmpty(nameof(brand));
        Model = model.NonEmpty(nameof(model));
        Color = color;
        NumberOfWheels = numberOfWheels;
    }
}
