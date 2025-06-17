using GarageApp.Handler;
using GarageApp.Menu;
using GarageApp.UI;

namespace GarageApp.Manager;

public class GarageManager(IUI ui, IHandler handler) : IManager
{
    private readonly IUI _ui = ui;
    private readonly IHandler _handler = handler;

    public void Run()
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            BaseMenu menu = _handler.HasGarage()
                ? new DefaultGarageMenu(_ui, _handler)
                : new EmptyGarageMenu(_ui, _handler);

            keepRunning = menu.Run();
        }
    }
}