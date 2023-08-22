namespace advent_of_code_2015.Day16;
internal class Day16 : AdventSolution
{
    private readonly IDictionary<string, int> targetSue =
        new Dictionary<string, int>()
    {
        { "children", 3 },
        { "cats", 7 },
        { "samoyeds", 2},
        { "pomeranians", 3},
        { "akitas", 0},
        { "vizslas", 0},
        { "goldfish", 5},
        { "trees", 3},
        { "cars", 2},
        { "perfumes", 1}
    };

    protected override long part1ExampleExpected => 213;
    protected override long part1InputExpected => 213;
    protected override long part2ExampleExpected => 323;
    protected override long part2InputExpected => 323;

    private long work(
        string[] input,
        Func<string, int, int, bool> matches)
    {
        for (int ii = 0; ii < input.Length; ii++)
        {
            long sueNumber = ii + 1;
            var line = input[ii];

            if (qualifies(line, matches))
            {
                return sueNumber;
            }
        }

        return -1;
    }

    private bool qualifies(
        string line,
        Func<string, int, int, bool> matches)
    {
        var checkingDict = new Dictionary<string, int>();
        var splitBySue = line.Substring(line.IndexOf(":") + 2);
        var splitByComponent = splitBySue.Split(", ");

        foreach (var component in splitByComponent)
        {
            var componentAndValue = component.Split(": ");
            checkingDict[componentAndValue[0]] = int.Parse(componentAndValue[1]);
        }

        foreach (var key in checkingDict.Keys)
        {
            if (targetSue.ContainsKey(key))
            {
                if (!matches(key, checkingDict[key], targetSue[key]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool matchesExactly(
        string key,
        int factValue,
        int mfcsamValue) => factValue == mfcsamValue;

    private bool useOutdatedOutput(
        string key,
        int factValue,
        int mfcsamValue)
    {
        if (key == "cats" || key == "trees")
        {
            return factValue > mfcsamValue;
        }

        if (key == "pomeranians" || key == "goldfish")
        {
            return factValue < mfcsamValue;
        }

        return factValue == mfcsamValue;
    }

    protected override long part1Work(string[] input) =>
        work(input, matchesExactly);

    protected override long part2Work(string[] input) =>
        work(input, useOutdatedOutput);
}
