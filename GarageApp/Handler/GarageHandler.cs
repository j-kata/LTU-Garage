using GarageApp.Vehicles;

namespace GarageApp.Handler;

public class GarageHandler : IHandler
{
    private readonly Garage<Vehicle>? _garage;

    public bool HasGarage() => _garage is not null;
}