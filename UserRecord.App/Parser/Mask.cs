namespace UserRecord.App.Parser;

internal class Mask
{
    private string[] _matchMasks;

    public Mask (string[] matchMasks)
    {
        _matchMasks = matchMasks;
    }

    private string ExtractValue(string value)
    {
        var pair = value.Split(':');

        if (_matchMasks.Contains(pair[0]))
            return pair[1];

        return null;
    }

    public string[] ExtractParameters(string[] values)
    {
        string[] parameters = new string[_matchMasks.Length];

        for (int i = 0; i < values.Length; i++)
        {
            string idValue = values[i];
            parameters[i] = ExtractValue(idValue);
        }

        return parameters;
    }
}