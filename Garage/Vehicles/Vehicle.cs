using Garage.Types;

namespace Garage.Vehicles;

public abstract class Vehicle : IVehicle
{
    public string Brand { get; }
    public string Model { get; }
    public string RegistrationNumber { get; }
    public Color Color { get; }
    public uint NumberOfWheels { get; }

    public Vehicle(string registrationNumber, string brand, string model, Color color, uint numberOfWheels)
    {
        RegistrationNumber = registrationNumber;
        Brand = brand;
        Model = model;
        Color = color;
        NumberOfWheels = numberOfWheels;
    }
}
