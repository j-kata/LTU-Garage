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

    public Garage(int capacity, T?[] seed) : this(capacity)
    {
        if (seed.Length > capacity)
            throw new ArgumentException("The number of vehicles exceeds the garage capacity", nameof(capacity));

        for (var i = 0; i < seed.Length; i++)
            _vehicles[i] = seed[i];
    }

    public Garage(T?[] seed) : this(seed.Length, seed) { }


    public void Park(T vehicle, Action<string> callback)
    {
        if (IsFull)
        {
            callback("Illegal action! Garage is Full");
            return;
        }

        int index = Array.FindIndex(_vehicles, v => v is null);
        _vehicles[index] = vehicle;

        callback("The vehicle was parked");
    }

    public void Depart(string registrationNumber, Action<string> callback)
    {
        int index = Array.FindIndex(_vehicles, v => registrationNumber.EqualsCaseIgnore(v?.RegistrationNumber));

        if (index == -1)
        {
            callback("Vehicle was not found.");
            return;
        }
        _vehicles[index] = null;

        callback("The vehicle departed.");
    }

    public T? FindByRegistrationNumber(string registrationNumber)
    {
        return _vehicles.FirstOrDefault(v => registrationNumber.EqualsCaseIgnore(v?.RegistrationNumber));
    }

    public IEnumerator<T?> GetEnumerator()
    {
        foreach (var v in _vehicles)
            yield return v;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}