using Garage.Extensions;
using Garage.Types;

namespace Garage.Vehicles;

public class Bus : Vehicle
{
    public int SeatsNumber { get; }
    public bool HasToilet { get; }

    public Bus(string registrationNumber, string brand, string model, ColorType color, int wheelsNumber, int seatsNumber, bool hasToilet)
        : base(registrationNumber, brand, model, color, wheelsNumber)
    {
        SeatsNumber = seatsNumber.Positive(nameof(seatsNumber));
        HasToilet = hasToilet;
    }
}
