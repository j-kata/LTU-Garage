using GarageApp.Types;
using GarageApp.Vehicles;

namespace GarageApp.Handler;

public interface IHandler
{
    public bool HasGarage();
    public void CreateGarage(int capacity);
    public void CreateGarage(Vehicle[] vehicles);
    public IEnumerable<string> GarageOverview();
    public IEnumerable<string> ListVehicles();
    public IEnumerable<string> VehicleTypeStats();
    public string? FindByRegistration(string rNumber);
    public string DepartVehicle(string rNumber);
    public string ParkVehicle(Vehicle vehicle);
    public IEnumerable<string> FilterVehicles(VehicleFilterData data);
}