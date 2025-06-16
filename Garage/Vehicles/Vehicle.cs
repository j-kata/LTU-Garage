using Garage.Types;
using Garage.Extensions;

namespace Garage.Vehicles;

public abstract class Vehicle : IVehicle
{
    public string Brand { get; }
    public string Model { get; }
    public string RegistrationNumber { get; }
    public ColorType Color { get; }
    public int WheelsNumber { get; }
    public string Type => GetType().Name;

    public Vehicle(string registrationNumber, string brand, string model, ColorType color, int wheelsNumber)
    {
        RegistrationNumber = registrationNumber.NotEmpty(nameof(registrationNumber));
        Brand = brand.NotEmpty(nameof(brand));
        Model = model.NotEmpty(nameof(model));
        Color = color;
        WheelsNumber = wheelsNumber.NotNegative(nameof(wheelsNumber));
    }
}
