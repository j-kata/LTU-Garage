namespace Garage.Vehicles;

public abstract class Motorcycle : Vehicle
{
    public uint CylinderVolume { get; }
    public bool HasSideCar { get; }

    public Motorcycle(string registrationNumber, string brand, string model, string color, uint numberOfWheels, uint cylinderVolume, bool hasSideCar)
        : base(registrationNumber, brand, model, color, numberOfWheels)
    {
        CylinderVolume = cylinderVolume;
        HasSideCar = hasSideCar;
    }
}
