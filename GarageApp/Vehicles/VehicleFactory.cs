using GarageApp.Types;

namespace GarageApp.Vehicles;

public static class VehicleFactory
{
    // TODO: refactor with generics, leave one public function, get type of vehicle as a parameter and vehicle from within
    public static Vehicle CreateCar(VehicleCommonData data, FuelType fuelType, int doorsNumber)
    {
        return new Car(
            registrationNumber: data.RNumber,
            brand: data.Brand,
            model: data.Model,
            color: data.Color,
            wheelsNumber: data.WheelsNumber,
            fuelType: fuelType,
            doorsNumber: doorsNumber);
    }

    public static Vehicle CreateMotorcycle(VehicleCommonData data, int cylinderVolume, bool hasSideCar)
    {
        return new Motorcycle(
            registrationNumber: data.RNumber,
            brand: data.Brand,
            model: data.Model,
            color: data.Color,
            wheelsNumber: data.WheelsNumber,
            cylinderVolume: cylinderVolume,
            hasSideCar: hasSideCar);
    }

    public static Vehicle CreateAirplane(VehicleCommonData data, int enginesNumber, double wingSpan)
    {
        return new Airplane(
            registrationNumber: data.RNumber,
            brand: data.Brand,
            model: data.Model,
            color: data.Color,
            wheelsNumber: data.WheelsNumber,
            enginesNumber: enginesNumber,
            wingSpan: wingSpan);
    }

    public static Vehicle CreateBoat(VehicleCommonData data, BoatType boatType, double length)
    {
        return new Boat(
            registrationNumber: data.RNumber,
            brand: data.Brand,
            model: data.Model,
            color: data.Color,
            boatType: boatType,
            length: length);
    }

    public static Vehicle CreateBus(VehicleCommonData data, int seatsNumber, bool hasToilet)
    {
        return new Bus(
            registrationNumber: data.RNumber,
            brand: data.Brand,
            model: data.Model,
            color: data.Color,
            wheelsNumber: data.WheelsNumber,
            seatsNumber: seatsNumber,
            hasToilet: hasToilet);
    }
}