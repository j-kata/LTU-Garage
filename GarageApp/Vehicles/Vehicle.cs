using GarageApp.Types;
using GarageApp.Extensions;

namespace GarageApp.Vehicles;

public abstract class Vehicle
{
    private const int RNumberMax = 10;
    private const int BrandMax = 30;
    private const int ModelMax = 30;

    public string Brand { get; }
    public string Model { get; }
    public string RegistrationNumber { get; set; }
    public ColorType Color { get; }
    public int WheelsNumber { get; }
    public abstract VehicleType Type { get; }

    public Vehicle(string registrationNumber, string brand, string model, ColorType color, int wheelsNumber)
    {
        RegistrationNumber = registrationNumber
            .NotEmpty(nameof(registrationNumber))
            .MaxLength(RNumberMax, nameof(registrationNumber));

        Brand = brand.NotEmpty(nameof(brand))
            .MaxLength(BrandMax, nameof(brand));

        Model = model.NotEmpty(nameof(model))
            .MaxLength(ModelMax, nameof(model));

        Color = color;
        WheelsNumber = wheelsNumber.NotNegative(nameof(wheelsNumber));
    }

    public override string ToString()
    {
        return $"{Type}: {Brand} {Model}, {Color} [{RegistrationNumber}]";
    }
}
