using UserRecord.Entity.Dto;

namespace UserRecord.App.Parser;

internal class ArgumentParser
{
    private static Dictionary<string, Mask> _masks = new()
    {
        ["-add"] = new Mask(new string[] {"FirstName", "LastName", "SalaryPerHour"}),
        ["-update"] = new Mask(new string[] {"Id", "FirstName", "LastName", "SalaryPerHour"}),
        ["-delete"] = new Mask(new string[] {"Id"}),
        ["-delete"] = new Mask(new string[] {"Id"}),
        ["-get"] = new Mask(new string[] {"Id"}),
        ["-getall"] = null
    };

    public static string[] GetParameters(string cmd, string[] values)
    {
        if (!_masks.ContainsKey(cmd))
            throw new Exception($"Not supported option -> {cmd}");

        var mask = _masks[cmd];

        if (mask != null)
            return mask.ExtractParameters(values);
        else
            return null;
    }
}

