using GarageApp.Handler;
using GarageApp.Loader;
using GarageApp.Manager;
using GarageApp.UI;

namespace GarageApp;

class Program
{
    static void Main(string[] args)
    {
        var manager = new GarageManager(
            new ConsoleUI(),
            new GarageHandler(),
            new StaticLoader());

        manager.Run();
    }
}
