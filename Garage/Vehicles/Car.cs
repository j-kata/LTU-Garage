namespace Garage.Vehicles;

public abstract class Car : Vehicle
{
    public string FuelType { get; }
    public uint NumberOfDoors { get; }

    public Car(string registrationNumber, string brand, string model, string color, uint numberOfWheels, string fuelType, uint numberOfDoors)
        : base(registrationNumber, brand, model, color, numberOfWheels)
    {
        FuelType = fuelType;
        NumberOfDoors = numberOfDoors;
    }
}
