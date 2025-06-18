using GarageApp.Extensions;
using GarageApp.Types;

namespace GarageApp.Vehicles;

public class Bus : Vehicle
{
    public int SeatsNumber { get; }
    public bool HasToilet { get; }

    public override VehicleType Type => VehicleType.Bus;

    public Bus(string registrationNumber, string brand, string model, ColorType color, int wheelsNumber, int seatsNumber, bool hasToilet)
        : base(registrationNumber, brand, model, color, wheelsNumber)
    {
        SeatsNumber = seatsNumber.Positive(nameof(seatsNumber));
        HasToilet = hasToilet;
    }
}
