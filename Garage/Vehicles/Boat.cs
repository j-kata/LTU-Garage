namespace Garage.Vehicles;

public abstract class Boat : Vehicle
{
    public string BoatType { get; }
    public double Length { get; }

    public Boat(string registrationNumber, string brand, string model, string color, uint numberOfWheels, string boatType, double length)
        : base(registrationNumber, brand, model, color, numberOfWheels)
    {
        BoatType = boatType;
        Length = length;
    }
}
