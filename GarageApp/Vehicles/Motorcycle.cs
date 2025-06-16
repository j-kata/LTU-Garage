using GarageApp.Extensions;
using GarageApp.Types;

namespace GarageApp.Vehicles;

public class Motorcycle : Vehicle
{
    public int CylinderVolume { get; }
    public bool HasSideCar { get; }

    public Motorcycle(string registrationNumber, string brand, string model, ColorType color, int wheelsNumber, int cylinderVolume, bool hasSideCar)
        : base(registrationNumber, brand, model, color, wheelsNumber)
    {
        CylinderVolume = cylinderVolume.Positive(nameof(cylinderVolume));
        HasSideCar = hasSideCar;
    }
}
