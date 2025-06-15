namespace Garage.Vehicles;

public abstract class Airplane : Vehicle
{
    public uint NumberOfEngines { get; }
    public double WingSpan { get; }

    public Airplane(string registrationNumber, string brand, string model, string color, uint numberOfWheels, uint numberOfEngines, double wingSpan)
        : base(registrationNumber, brand, model, color, numberOfWheels)
    {
        NumberOfEngines = numberOfEngines;
        WingSpan = wingSpan;
    }
}
