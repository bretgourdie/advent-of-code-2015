namespace advent_of_code_2015.Day07;
internal class Day07 : AdventSolutionTemplate<ushort, ushort>
{
    protected override ushort part1Work(string[] input)
    {
        var rules = new HashSet<Rule>();

        foreach (var line in input)
        {
            rules.Add(new Rule(line));
        }

        var identifiers = new Dictionary<string, ushort>();

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

        return identifiers["a"];
    }

    protected override ushort part1ExampleExpected => 72;
    protected override ushort part1InputExpected => 46065;
    protected override ushort part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override ushort part2ExampleExpected { get; }
    protected override ushort part2InputExpected { get; }
}
