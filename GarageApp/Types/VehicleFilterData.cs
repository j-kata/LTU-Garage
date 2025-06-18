namespace GarageApp.Types;

public record VehicleFilterData
{
    public VehicleType? Type { get; set; } = null;
    public string? Brand { get; set; } = null;
    public string? Model { get; set; } = null;
    public ColorType? Color { get; set; } = null;
    public int? WheelsNumber { get; set; } = null;
};