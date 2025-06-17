namespace GarageApp.Types;

public record VehicleCommonData(
    string RNumber,
    string Brand,
    string Model,
    ColorType Color,
    int WheelsNumber
);
