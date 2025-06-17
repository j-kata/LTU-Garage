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

    // TODO: Remove duplicate function. Rewrite as one generic function for both int and double
    public static double PromptUntilValidDouble(IUI ui, string prompt, Func<double, bool> predicate = null)
    {
        string validString = PromptUntilValidString(ui, prompt, (validString) =>
            double.TryParse(validString, out double number) &&
            (predicate is null || predicate(number)));

        return int.Parse(validString);
    }

    public static TEnum PromptUnilValidEnumChoice<TEnum>(IUI ui, string prompt) where TEnum : struct, Enum
    {
        var values = Enum.GetValues<TEnum>();
        var options = string.Join("\n", values.Select((value, i) => $"{i + 1}. {value}"));
        var index = PromptUntilValidNumber(ui, $"{prompt}\n{options}", (x) => x >= 1 && x <= values.Length);

        return values[index - 1];
    }
}