using Garage.Extensions;
using Garage.Types;

namespace Garage.Vehicles;

public class Airplane : Vehicle
{
    public int EnginesNumber { get; }
    public double WingSpan { get; }

    public Airplane(string registrationNumber, string brand, string model, ColorType color, int wheelsNumber, int enginesNumber, double wingSpan)
        : base(registrationNumber, brand, model, color, wheelsNumber)
    {
        EnginesNumber = enginesNumber.Positive(nameof(enginesNumber));
        WingSpan = wingSpan.Positive(nameof(wingSpan));
    }
}
