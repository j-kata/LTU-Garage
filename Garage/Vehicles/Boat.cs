using Garage.Extensions;
using Garage.Types;

namespace Garage.Vehicles;

public class Boat : Vehicle
{
    public BoatType BoatType { get; }
    public double Length { get; }

    public Boat(string registrationNumber, string brand, string model, ColorType color, BoatType boatType, double length)
        : base(registrationNumber, brand, model, color, 0)
    {
        BoatType = boatType;
        Length = length.Positive(nameof(length));
    }
}
