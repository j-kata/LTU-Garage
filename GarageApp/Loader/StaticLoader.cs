using GarageApp.Types;
using GarageApp.Vehicles;

namespace GarageApp.Loader;

public class StaticLoader : ILoader<Vehicle>
{
    public Vehicle[] Load()
    {
        return [
            new Airplane("Sky001", "Cessna", "172 Skyhawk", ColorType.White, 3, 1, 11.0),
            new Airplane("Jet900", "Gulfstream", "G600", ColorType.Silver, 6, 2, 28.7),
            new Car("ABC123", "Volvo", "XC60", ColorType.Red, 4, FuelType.Diesel, 5),
            new Car("XYZ789", "Ford", "Mustang", ColorType.Black, 4, FuelType.Gasoline, 2),
            new Motorcycle("BIKE01", "Harley-Davidson", "Street 750", ColorType.Blue, 2, 749, false),
            new Motorcycle("MOTO99", "Yamaha", "Tricity 300", ColorType.Pink, 3, 292, false),
            new Bus("BUS001", "Mercedes-Benz", "Citaro", ColorType.Yellow, 6, 44, true),
            new Bus("BUS777", "Volvo", "7900 Electric", ColorType.White, 8, 52, true),
            new Boat("BOAT01", "Bayliner", "Element E18", ColorType.White, BoatType.Yacht, 5.49),
            new Boat("SAIL22", "Beneteau", "Oceanis 30.1", ColorType.Blue, BoatType.Sailboat, 9.53)
        ];
    }
}
