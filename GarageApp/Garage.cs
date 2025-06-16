using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GarageApp.Extensions;
using GarageApp.Vehicles;

namespace GarageApp;

public class Garage<T> : IEnumerable<T?> where T : Vehicle
{
    private readonly T?[] _vehicles;
    private readonly int _capacity;

    public int Size => _capacity;
    public int PlacesLeft => _vehicles.Where(v => v is not null).Count();
    public bool IsEmpty => PlacesLeft == _capacity;
    public bool IsFull => PlacesLeft == 0;
    public IEnumerable<T?> GetVehicles() => _vehicles.Where(v => v is not null);
    public int Count => GetVehicles().Count();

    public Garage(int capacity)
    {
        _capacity = capacity.Positive(nameof(capacity));
        _vehicles = new T[_capacity];
    }

    public Garage(int capacity, T[] seed) : this(capacity)
    {
        if (seed.Length > capacity)
            throw new ArgumentException("", nameof(capacity));

        for (var i = 0; i < seed.Length; i++)
        {
            _vehicles[i] = seed[i];
        }
    }

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