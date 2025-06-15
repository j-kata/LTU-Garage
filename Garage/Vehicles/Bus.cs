namespace Garage.Vehicles;

public abstract class Bus : Vehicle
{
    public uint NumberOfSeats { get; }
    public bool HasToilet { get; }

    public Bus(string registrationNumber, string brand, string model, string color, uint numberOfWheels, uint numberOfSeats, bool hasToilet)
        : base(registrationNumber, brand, model, color, numberOfWheels)
    {
        NumberOfSeats = numberOfSeats;
        HasToilet = hasToilet;
    }
}
