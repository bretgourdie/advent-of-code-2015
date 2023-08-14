namespace advent_of_code_2015.Day07;
internal class Rule
{
    private readonly string Target;
    private readonly IList<string> Sources;
    private readonly Operation TheOperation;

    public Rule(string line)
    {
        var split = line.Split(" -> ");
        Target = split[1];

        Sources = new List<string>();
        var sourceSplit = split[0].Split(' ');

        if (sourceSplit.Length == 1)
        {
            TheOperation = Operation.Set;
            Sources.Add(sourceSplit[0]);
        }

        else if (sourceSplit.Length == 2)
        {
            TheOperation = Operation.Not;
            Sources.Add(sourceSplit[1]);
        }

        else if (sourceSplit.Length == 3)
        {
            Sources.Add(sourceSplit[0]);
            Sources.Add(sourceSplit[2]);

            switch (sourceSplit[1])
            {
                case "AND":
                    TheOperation = Operation.And;
                    break;
                case "OR":
                    TheOperation = Operation.Or;
                    break;
                case "LSHIFT":
                    TheOperation = Operation.LeftShift;
                    break;
                case "RSHIFT":
                    TheOperation = Operation.RightShift;
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
        return Sources.All(x =>
            ushort.TryParse(x, out ushort value)
            || identifiers.ContainsKey(x));
    }

    public void Appraise(IDictionary<string, ushort> identifiers)
    {
        int value;

        switch (TheOperation)
        {
            case Operation.Set:
                value = getValueOrIdentifier(Sources[0], identifiers);
                break;
            case Operation.And:
                value =
                    getValueOrIdentifier(Sources[0], identifiers)
                    & getValueOrIdentifier(Sources[1], identifiers);
                break;
            case Operation.Or:
                value =
                    getValueOrIdentifier(Sources[0], identifiers)
                    | getValueOrIdentifier(Sources[1], identifiers);
                break;
            case Operation.LeftShift:
                value =
                    getValueOrIdentifier(Sources[0], identifiers)
                    << getValueOrIdentifier(Sources[1], identifiers);
                break;
            case Operation.RightShift:
                value =
                    getValueOrIdentifier(Sources[0], identifiers)
                    >> getValueOrIdentifier(Sources[1], identifiers);
                break;
            case Operation.Not:
                value = ~getValueOrIdentifier(Sources[0], identifiers);
                break;
            default:
                throw new NotImplementedException();
        }

        identifiers[Target] = (ushort)value;
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
}
