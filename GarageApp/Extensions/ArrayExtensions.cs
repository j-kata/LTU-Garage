using GarageApp.Vehicles;

namespace GarageApp.Extensions;

public static class VehicleArrayExtensions
{
    public static int FirstNullIndex<T>(this T?[] array) where T : Vehicle
    {
        return Array.FindIndex(array, v => v is null);
    }

    public static int FirstWithRegistrationNumber<T>(this T?[] array, string rNumber) where T : Vehicle
    {
        return Array.FindIndex(array, v => rNumber.EqualsCaseIgnore(v?.RegistrationNumber));
    }

}