using GarageApp.UI;

namespace GarageApp.Helpers;

public static class Util
{
    public static string PromptUntilValidString(IUI ui, string prompt, Func<string, bool> predicate = null)
    {
        while (true)
        {
            ui.IndentedWriteLine(prompt);
            var input = ui.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                ui.WriteLine("Invalid input. Try again");
                continue;
            }

            if (predicate != null && !predicate(input))
            {
                ui.WriteLine("Invalid input. Try again");
                continue;
            }
            return input;
        }
    }

    public static int PromptUntilValidNumber(IUI ui, string prompt, Func<int, bool> predicate = null)
    {
        string validString = PromptUntilValidString(ui, prompt, (validString) =>
            int.TryParse(validString, out int number) &&
            (predicate is null || predicate(number)));

        return int.Parse(validString);
    }
}