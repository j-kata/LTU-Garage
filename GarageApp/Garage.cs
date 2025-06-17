using System.Collections;
using GarageApp.Extensions;
using GarageApp.Vehicles;

namespace GarageApp;

public class Garage<T> : IEnumerable<T?> where T : Vehicle
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
        if (seed.Length > capacity)
            throw new ArgumentException("The number of vehicles exceeds the garage capacity", nameof(capacity));

        for (var i = 0; i < seed.Length; i++)
        {
            if (seed[i] is not null && !HasDuplicate(seed[i]))
                _vehicles[i] = seed[i];
        }
    }

    public Garage(T[] seed) : this(seed.Length, seed) { }

    public IEnumerable<T> GetVehicles()
    {
        foreach (var vehicle in _vehicles)
            if (vehicle is not null) yield return vehicle;
    }

    public IEnumerable<(string name, int count)> GetVehiclesTypeCount()
    {
        return GetVehicles().GroupBy(v => v.Type).Select(kv => (kv.Key, kv.Count()));
    }

    public bool Park(T vehicle)
    {
        if (IsFull || HasDuplicate(vehicle))
            return false;

        if (!TryFindSpot(out int index))
            throw new InvalidOperationException("Unexpected error. Garage is full");

        _vehicles[index] = vehicle;
        return true;
    }

    public bool Depart(string registrationNumber)
    {
        if (!TryFindByRegistrationNumber(registrationNumber, out int index))
            return false;

        _vehicles[index] = null;
        return true;
    }

    public bool TryFindByRegistrationNumber(string rNumber, out int index)
    {
        index = _vehicles.FirstWithRegistrationNumber(rNumber);
        return index != -1;
    }

    public T? FindByRegistrationNumber(string registrationNumber)
    {
        return TryFindByRegistrationNumber(registrationNumber, out int index) ? _vehicles[index] : null;
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