using GarageApp.Vehicles;

namespace GarageApp.Handler;

public class GarageHandler : IHandler
{
    private Garage<Vehicle>? _garage;

    public bool HasGarage() => _garage is not null;

    public void CreateGarage(int capacity) =>
        _garage = new Garage<Vehicle>(capacity);

    public void CreateGarage(Vehicle[] vehicles) =>
        _garage = new Garage<Vehicle>(vehicles);

    public int TotalPlaces() => _garage?.Capacity ?? 0;
    public int FreePlaces() => _garage?.PlacesLeft ?? 0;

    public IEnumerable<string> ListVehicles() =>
        _garage?.GetVehicles()
            .Select(v => v.ToString()) ?? [];

    public IEnumerable<string> VehicleTypeStats() =>
        _garage?.GetVehiclesTypeCount()
            .Select(v => $"{v.name}: {v.count}") ?? [];
}