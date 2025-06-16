using GarageApp.Extensions;
using GarageApp.Types;

namespace GarageApp.Vehicles;

public class Car : Vehicle
{
    public FuelType FuelType { get; }
    public int DoorsNumber { get; }

    public Car(string registrationNumber, string brand, string model, ColorType color, int wheelsNumber, FuelType fuelType, int doorsNumber)
        : base(registrationNumber, brand, model, color, wheelsNumber)
    {
        FuelType = fuelType;
        DoorsNumber = doorsNumber.Positive(nameof(doorsNumber));
    }
}
