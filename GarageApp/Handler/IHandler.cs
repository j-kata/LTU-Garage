using GarageApp.Vehicles;

namespace GarageApp.Handler;

public interface IHandler
{
    public bool HasGarage();
    public void CreateGarage(int capacity);
    public void CreateGarage(Vehicle[] vehicles);
    public int TotalPlaces();
    public int FreePlaces();
    public IEnumerable<string> ListVehicles();
    public IEnumerable<string> VehicleTypeStats();
}