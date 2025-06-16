namespace GarageApp.Handler;

public interface IHandler
{
    public bool HasGarage();
    public void CreateGarage(int capacity);
}