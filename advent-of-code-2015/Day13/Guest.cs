namespace advent_of_code_2015.Day13;
internal class Guest
{
    public readonly string Name;

    private readonly IDictionary<string, long> preferences;

    public Guest(
        string name,
        IEnumerable<string> strPreferences)
    {
        Name = name;

        preferences = parsePreferences(strPreferences);
    }

    public Guest(
        string name)
    {
        Name = name;
        preferences = new Dictionary<string, long>();
    }

    private IDictionary<string, long> parsePreferences(IEnumerable<string> strPreferences)
    {
        var preferences = new Dictionary<string, long>();

        foreach (var strPreference in strPreferences)
        {
            var split = strPreference.Split(' ');

            long sign = split[2] == "gain" ? 1 : -1;

            long amount = long.Parse(split[3]);

            var guest = split[10].Replace(".", String.Empty);

            preferences[guest] = amount * sign;
        }

        return preferences;
    }

    public long Appraise(Guest other)
    {
        if (!preferences.ContainsKey(other.Name))
        {
            return 0;
        }

        return preferences[other.Name];
    }
}
