using Garage.Types;

namespace Garage.Vehicles;

public class Bus : Vehicle
{
    public uint NumberOfSeats { get; }
    public bool HasToilet { get; }

    public Bus(string registrationNumber, string brand, string model, ColorType color, uint numberOfWheels, uint numberOfSeats, bool hasToilet)
        : base(registrationNumber, brand, model, color, numberOfWheels)
    {
        NumberOfSeats = numberOfSeats;
        HasToilet = hasToilet;
    }
}
