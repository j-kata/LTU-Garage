using GarageApp.Types;
using GarageApp.Vehicles;

namespace GarageApp;

public interface IGarage<T> where T : Vehicle
{
    T? this[int index] { get; }

    int Capacity { get; }
    int Count { get; }
    int PlacesLeft { get; }
    bool IsEmpty { get; }
    bool IsFull { get; }

    (bool, string message) Depart(string registrationNumber);
    (bool, string message) FindByRegistrationNumber(string registrationNumber);
    IEnumerator<T?> GetEnumerator();
    IEnumerable<T> GetVehicles();
    IEnumerable<(VehicleType name, int count)> GetVehiclesTypeCount();
    bool HasDuplicate(T vehicle);
    (bool, string message) Park(T vehicle);
    bool TryFindByRegistrationNumber(string rNumber, out int index);
    bool TryFindSpot(out int index);
}