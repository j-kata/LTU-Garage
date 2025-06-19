using GarageApp.Handler;
using GarageApp.Loader;
using GarageApp.Menu;
using GarageApp.UI;
using GarageApp.Vehicles;

namespace GarageApp.Manager;

public class GarageManager(IUI ui, IHandler handler, ILoader<Vehicle> loader) : IManager
{
    private readonly IUI _ui = ui;
    private readonly IHandler _handler = handler;
    private readonly ILoader<Vehicle> _loader = loader;

    public void Run()
    {
        bool shouldExit = false;
        // Run until menu signals that Exit was called
        while (!shouldExit)
        {
            _ui.Clear();

            // Choose menu depending on garage state
            BaseMenu menu = _handler.HasGarage()
                ? new DefaultGarageMenu(_ui, _handler, _loader)
                : new EmptyGarageMenu(_ui, _handler, _loader);

            shouldExit = menu.Run();
        }
    }
}