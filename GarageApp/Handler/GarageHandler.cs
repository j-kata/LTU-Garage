using GarageApp.Vehicles;

namespace GarageApp.Handler;

public class GarageHandler : IHandler
{
    private Garage<Vehicle>? _garage;

    public bool HasGarage() => _garage is not null;

    public void CreateGarage(int capacity)
    {
        _garage = new Garage<Vehicle>(capacity);
    }
}