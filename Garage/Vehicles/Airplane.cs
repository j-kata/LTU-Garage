using Garage.Extensions;
using Garage.Types;

namespace Garage.Vehicles;

public class Airplane : Vehicle
{
    public uint NumberOfEngines { get; }
    public float WingSpan { get; }

    public Airplane(string registrationNumber, string brand, string model, ColorType color, uint numberOfWheels, uint numberOfEngines, float wingSpan)
        : base(registrationNumber, brand, model, color, numberOfWheels)
    {
        NumberOfEngines = numberOfEngines;
        WingSpan = wingSpan.Positive(nameof(wingSpan));
    }
}
