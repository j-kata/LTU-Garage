using System.Diagnostics.CodeAnalysis;

namespace Garage.Vehicles;

public abstract class Vehicle : IVehicle
{
    public string Brand { get; }
    public string Model { get; }
    public string RegistrationNumber { get; }
    public string Color { get; }
    public uint NumberOfWheels { get; }

    public Vehicle(string registrationNumber, string brand, string model, string color, uint numberOfWheels)
    {
        RegistrationNumber = registrationNumber;
        Brand = brand;
        Model = model;
        Color = color;
        NumberOfWheels = numberOfWheels;
    }
}
