using GarageApp.Handler;
using GarageApp.Helpers;
using GarageApp.UI;

namespace GarageApp.Menu;

public abstract class BaseMenu(IUI ui, IHandler handler) : IMenu
{
    protected readonly IUI _ui = ui;
    protected readonly IHandler _handler = handler;
    protected virtual bool ShouldExit { get; set; } = false;

    // Handle menu flow. Return true if application should exit
    public virtual bool Run()
    {
        _ui.WriteLine(Title);

        bool keepRunning = true;
        while (keepRunning)
        {
            // Print menu options
            Show();

            // Get valid user input
            var input = Util.PromptUntilValidString(_ui, "Use numbers to navigate the menu:");

            // Handle input. Continue running menu if true 
            keepRunning = HandleChoice(input);
        }
        return ShouldExit;
    }

    public abstract string Title { get; }
    public abstract void Show();
    public abstract bool HandleChoice(string choice);
}