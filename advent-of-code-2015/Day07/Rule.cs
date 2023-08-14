namespace advent_of_code_2015.Day07;
internal class Rule
{
    private readonly string target;
    private readonly IList<string> sources;
    private readonly Operation theOperation;
    private readonly string raw;

    public Rule(string line)
    {
        raw = line;

        var split = line.Split(" -> ");
        target = split[1];

        sources = new List<string>();
        var sourceSplit = split[0].Split(' ');

        if (sourceSplit.Length == 1)
        {
            theOperation = Operation.Set;
            sources.Add(sourceSplit[0]);
        }

        else if (sourceSplit.Length == 2)
        {
            theOperation = Operation.Not;
            sources.Add(sourceSplit[1]);
        }

        else if (sourceSplit.Length == 3)
        {
            sources.Add(sourceSplit[0]);
            sources.Add(sourceSplit[2]);

            switch (sourceSplit[1])
            {
                case "AND":
                    theOperation = Operation.And;
                    break;
                case "OR":
                    theOperation = Operation.Or;
                    break;
                case "LSHIFT":
                    theOperation = Operation.LeftShift;
                    break;
                case "RSHIFT":
                    theOperation = Operation.RightShift;
                    break;
                default:
                    throw new ArgumentException(nameof(line));
            }
        }

        else
        {
            throw new ArgumentException(nameof(line));
        }
    }

    public bool CanAppraise(IDictionary<string, ushort> identifiers)
    {
        return sources.All(x =>
            ushort.TryParse(x, out ushort value)
            || identifiers.ContainsKey(x));
    }

    public void Appraise(IDictionary<string, ushort> identifiers)
    {
        if (identifiers.ContainsKey(target))
        {
            return;
        }

        int value;

        switch (theOperation)
        {
            case Operation.Set:
                value = getValueOrIdentifier(sources[0], identifiers);
                break;
            case Operation.And:
                value =
                    getValueOrIdentifier(sources[0], identifiers)
                    & getValueOrIdentifier(sources[1], identifiers);
                break;
            case Operation.Or:
                value =
                    getValueOrIdentifier(sources[0], identifiers)
                    | getValueOrIdentifier(sources[1], identifiers);
                break;
            case Operation.LeftShift:
                value =
                    getValueOrIdentifier(sources[0], identifiers)
                    << getValueOrIdentifier(sources[1], identifiers);
                break;
            case Operation.RightShift:
                value =
                    getValueOrIdentifier(sources[0], identifiers)
                    >> getValueOrIdentifier(sources[1], identifiers);
                break;
            case Operation.Not:
                value = ~getValueOrIdentifier(sources[0], identifiers);
                break;
            default:
                throw new NotImplementedException();
        }

        identifiers[target] = (ushort)value;
    }

    private ushort getValueOrIdentifier(
        string raw,
        IDictionary<string, ushort> identifiers)
    {
        if (ushort.TryParse(raw, out ushort value))
        {
            return value;
        }

        return identifiers[raw];
    }

    private enum Operation
    {
        Set,
        And,
        Or,
        LeftShift,
        RightShift,
        Not
    }

    public override string ToString()
    {
        return raw;
    }
}
