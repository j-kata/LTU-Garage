using GarageApp.Types;
using GarageApp.Vehicles;

namespace GarageApp.Handler;

public class GarageHandler : IHandler
{
    private const string InvalidOperation = "Garage was not created";
    private Garage<Vehicle>? _garage;

    public bool HasGarage() => _garage is not null;

    public void CreateGarage(int capacity) =>
        _garage = new Garage<Vehicle>(capacity);

    public void CreateGarage(Vehicle[] vehicles) =>
        _garage = new Garage<Vehicle>(vehicles);

    public IEnumerable<string> GarageOverview()
    {
        if (_garage == null)
            return [InvalidOperation];

        return [
            $"Total places: {_garage.Capacity}",
            $"Free places: {_garage.PlacesLeft}",
            $"Occupied: {_garage.Count}"
        ];
    }

    public IEnumerable<string> ListVehicles()
    {
        if (_garage == null || _garage.IsEmpty)
            return ["Garage is empty"];

        return _garage.GetVehicles().Select(v => v.ToString());
    }

    public IEnumerable<string> VehicleTypeStats()
    {
        if (_garage == null || _garage.IsEmpty)
            return ["Garage is empty"];

        return _garage.GetVehiclesTypeCount()
            .Select(v => $"{v.name}: {v.count}");
    }

    public string FindByRegistration(string rNumber) =>
        _garage?.FindByRegistrationNumber(rNumber).message
            ?? InvalidOperation;

    public string DepartVehicle(string rNumber) =>
        _garage?.Depart(rNumber).message ?? InvalidOperation;

    public string ParkVehicle(Vehicle vehicle) =>
        _garage?.Park(vehicle).message ?? InvalidOperation;

    public IEnumerable<string> FilterVehicles(VehicleFilterData data) =>
        _garage?.GetVehicles().Where(v =>
            (data.Type is null || data.Type == v.Type) &&
            (data.Color is null || data.Color == v.Color) &&
            (data.WheelsNumber is null || data.WheelsNumber == v.WheelsNumber) &&
            (data.Brand is null || v.Brand.Contains(data.Brand, StringComparison.InvariantCultureIgnoreCase)) &&
            (data.Model is null || v.Model.Contains(data.Model, StringComparison.InvariantCultureIgnoreCase)))
            .Select(v => v.ToString()) ?? [];
}