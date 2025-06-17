using GarageApp.Handler;
using GarageApp.Helpers;
using GarageApp.UI;

namespace GarageApp.Menu;

public abstract class BaseMenu(IUI ui, IHandler handler) : IMenu
{
    protected readonly IUI _ui = ui;
    protected readonly IHandler _handler = handler;
    protected virtual bool ContinueToNextMenu { get; set; } = true;


    public virtual bool Run()
    {
        _ui.WriteLine(Title);

        bool keepRunning = true;
        while (keepRunning)
        {
            Show();

            var input = Util.PromptUntilValidString(_ui, "Use numbers to navigate the menu:");
            keepRunning = HandleChoice(input);
        }
        return ContinueToNextMenu;
    }

    public abstract string Title { get; }
    public abstract void Show();
    public abstract bool HandleChoice(string choice);
}