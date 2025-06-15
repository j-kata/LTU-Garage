using Garage.Types;

namespace Garage.Vehicles;

public abstract class Boat : Vehicle
{
    public BoatType Type { get; }
    public double Length { get; }

    public Boat(string registrationNumber, string brand, string model, Color color, uint numberOfWheels, BoatType type, double length)
        : base(registrationNumber, brand, model, color, numberOfWheels)
    {
        Type = type;
        Length = length;
    }
}
