using Garage.Extensions;
using Garage.Types;

namespace Garage.Vehicles;

public class Car : Vehicle
{
    public FuelType FuelType { get; }
    public uint NumberOfDoors { get; }

    public Car(string registrationNumber, string brand, string model, ColorType color, uint numberOfWheels, FuelType fuelType, uint numberOfDoors)
        : base(registrationNumber, brand, model, color, numberOfWheels)
    {
        FuelType = fuelType;
        NumberOfDoors = numberOfDoors.NonZero(nameof(numberOfDoors));
    }
}
