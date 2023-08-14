namespace advent_of_code_2015.Day07;
internal class Day07 : AdventSolutionTemplate<ushort, ushort>
{
    private const string part1Target = "a";
    private const string part2Target = "b";

    private void work(
        string[] input,
        IDictionary<string, ushort> identifiers)
    {
        var rules = new HashSet<Rule>();

        foreach (var line in input)
        {
            rules.Add(new Rule(line));
        }


        while (rules.Any())
        {
            var rulesToRemove = new HashSet<Rule>();

            foreach (var rule in rules)
            {
                if (rule.CanAppraise(identifiers))
                {
                    rule.Appraise(identifiers);
                    rulesToRemove.Add(rule);
                }
            }

            foreach (var ruleToRemove in rulesToRemove)
            {
                rules.Remove(ruleToRemove);
            }
        }
    }

    protected override ushort part1Work(string[] input)
    {
        var identifiers = new Dictionary<string, ushort>();
        work(input, identifiers);
        return identifiers[part1Target];
    }

    protected override ushort part1ExampleExpected => 72;
    protected override ushort part1InputExpected => 46065;
    protected override ushort part2Work(string[] input)
    {
        var identifiers = new Dictionary<string, ushort>();
        work(input, identifiers);
        var aValue = identifiers[part1Target];
        identifiers = new Dictionary<string, ushort>();
        identifiers[part2Target] = aValue;
        work(input, identifiers);
        return identifiers[part1Target];
    }

    protected override ushort part2ExampleExpected => 72;
    protected override ushort part2InputExpected => 14134;
}
