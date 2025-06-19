using System.Collections;
using GarageApp.Extensions;
using GarageApp.Types;
using GarageApp.Vehicles;

namespace GarageApp;

public class Garage<T> : IEnumerable<T?>, IGarage<T> where T : Vehicle
{
    private readonly T?[] _vehicles;

    public int Capacity { get; }
    public int Count => GetVehicles().Count();
    public int PlacesLeft => Capacity - Count;
    public bool IsEmpty => PlacesLeft == Capacity;
    public bool IsFull => Count == Capacity;
    public T? this[int index] => _vehicles[index];

    public Garage(int capacity)
    {
        Capacity = capacity.Positive(nameof(capacity));
        _vehicles = new T[Capacity];
    }

    public Garage(int capacity, T[] seed) : this(capacity)
    {
        seed.MaxLength(capacity, nameof(capacity));

        for (var i = 0; i < seed.Length; i++)
            if (seed[i] != null && !HasDuplicate(seed[i]))
                _vehicles[i] = seed[i];
    }

    public Garage(T[] seed) : this(seed.Length, seed) { }

    public IEnumerable<T> GetVehicles()
    {
        foreach (var vehicle in _vehicles)
            if (vehicle is not null) yield return vehicle;
    }

    public IEnumerable<(VehicleType name, int count)> GetVehiclesTypeCount()
    {
        return GetVehicles().GroupBy(v => v.Type).Select(kv => (kv.Key, kv.Count()));
    }

    public (bool, string message) Park(T vehicle)
    {
        vehicle.NotNull(nameof(vehicle));

        if (IsFull)
            return (false, "The garage is full");

        if (HasDuplicate(vehicle))
            return (false, "The vehicle is already parked");

        if (!TryFindSpot(out int index))
            throw new InvalidOperationException("Unexpected error. Garage is full");

        _vehicles[index] = vehicle;
        return (true, $"The vehicle {vehicle.RegistrationNumber} was parked");
    }

    public (bool, string message) Depart(string registrationNumber)
    {
        if (!TryFindByRegistrationNumber(registrationNumber, out int index))
            return (false, "Vehicle was not found");

        _vehicles[index] = null;
        return (true, "Vehicle departed");
    }

    public (bool, string message) FindByRegistrationNumber(string registrationNumber)
    {
        return TryFindByRegistrationNumber(registrationNumber, out int index)
            ? (true, _vehicles[index]!.ToString())
            : (false, "Vehicle was not found");
    }

    public bool TryFindByRegistrationNumber(string rNumber, out int index)
    {
        rNumber.NotEmpty(nameof(rNumber));

        index = _vehicles.FirstWithRegistrationNumber(rNumber);
        return index != -1;
    }

    public bool HasDuplicate(T vehicle)
    {
        return TryFindByRegistrationNumber(vehicle.RegistrationNumber, out int _);
    }

    public bool TryFindSpot(out int index)
    {
        index = _vehicles.FirstNullIndex();
        return index != -1;
    }

    public IEnumerator<T?> GetEnumerator()
    {
        foreach (var v in _vehicles)
            yield return v;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}