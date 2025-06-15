using Garage.Types;

namespace Garage.Vehicles;

public abstract class Car : Vehicle
{
    public FuelType Fuel { get; }
    public uint NumberOfDoors { get; }

    public Car(string registrationNumber, string brand, string model, Color color, uint numberOfWheels, FuelType fuel, uint numberOfDoors)
        : base(registrationNumber, brand, model, color, numberOfWheels)
    {
        Fuel = fuel;
        NumberOfDoors = numberOfDoors;
    }
}
