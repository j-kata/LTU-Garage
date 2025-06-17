using GarageApp.Vehicles;

namespace GarageApp.Loader;

public interface ILoader<T>
{
    public T[] Load();
}