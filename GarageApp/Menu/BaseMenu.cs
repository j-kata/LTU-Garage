using GarageApp.Handler;
using GarageApp.UI;

namespace GarageApp.Menu;

public abstract class BaseMenu(IUI ui, IHandler handler) : IMenu
{
    protected readonly IUI _ui = ui;
    protected readonly IHandler _handler = handler;

    public abstract string Title { get; }
    public abstract void Show();
    public abstract bool HandleChoice(string choice);

    public virtual void Run()
    {
        _ui.WriteLine(Title);

        bool keepRunning = true;
        while (keepRunning)
        {
            Show();
            // TODO: Add PromtUntilValid
            var input = _ui.ReadLine();
            keepRunning = HandleChoice(input);
        }
    }
}