using Garage.Extensions;
using Garage.Types;

namespace Garage.Vehicles;

public abstract class Boat : Vehicle
{
    public BoatType BoatType { get; }
    public float Length { get; }

    public Boat(string registrationNumber, string brand, string model, Color color, uint numberOfWheels, BoatType boatType, float length)
        : base(registrationNumber, brand, model, color, numberOfWheels)
    {
        BoatType = boatType;
        Length = length.Positive(nameof(length));
    }
}
