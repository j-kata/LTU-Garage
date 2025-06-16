using System.Collections;
using GarageApp.Extensions;
using GarageApp.Vehicles;

namespace GarageApp;

public class Garage<T> : IEnumerable<T?> where T : Vehicle
{
    private readonly T?[] _vehicles;

    public int Capacity { get; }

    public IEnumerable<T?> GetVehicles() => _vehicles.Where(v => v is not null);
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


    public void Park(T vehicle, Action<string> callback)
    {
        if (IsFull)
        {
            callback("No empty spot found. Garage is full.");
            return;
        }

        if (HasDuplicate(vehicle))
        {
            callback("Vehicle with that registration number is already parked.");
            return;
        }

        if (!TryFindSpot(out int index))
            throw new InvalidOperationException("Unexpected error. Garage is full");


        _vehicles[index] = vehicle;
        callback("The vehicle was parked");
    }

    public void Depart(string registrationNumber, Action<string> callback)
    {
        if (!TryFindByRegistrationNumber(registrationNumber, out int index))
        {
            callback("Vehicle with that registration number was not found.");
            return;
        }

        _vehicles[index] = null;
        callback("The vehicle departed.");
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