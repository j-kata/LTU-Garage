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

    public bool IsGarageEmpty() => _garage?.IsEmpty ?? true;
    public int TotalPlaces() => _garage?.Capacity ?? 0;
    public int FreePlaces() => _garage?.PlacesLeft ?? 0;

    public IEnumerable<string> ListVehicles() =>
        _garage?.GetVehicles()
            .Select(v => v.ToString()) ?? [];

    public IEnumerable<string> VehicleTypeStats() =>
        _garage?.GetVehiclesTypeCount()
            .Select(v => $"{v.name}: {v.count}") ?? [];

    public string FindByRegistration(string rNumber) =>
        _garage?.FindByRegistrationNumber(rNumber)?.ToString()
            ?? "Vehice was not found";

    public string DepartVehicle(string rNumber) =>
        _garage is not null && _garage.Depart(rNumber)
            ? "Vehicle departed" : "Vehice was not found";

    public string ParkVehicle(Vehicle vehicle) =>
        _garage is not null && _garage.Park(vehicle)
            ? "Vehicle was parked" : "Garage is full or vehicle is already parked";
}