namespace advent_of_code_2015.Day19;
internal class Day19 : AdventSolution
{
    const string singleElectron = "e";

    protected override long part1ExampleExpected => 7;
    protected override long part1InputExpected => 509;
    protected override long part2ExampleExpected => 6;
    protected override long part2InputExpected => -1;

    private long work(
        string[] input,
        Func<IDictionary<string, IList<string>>, string, long> workFunction)
    {
        var translations = getTranslations(input);
        var startString = getStartString(input);

        return workFunction(translations, startString);
    }

    private long findPermutations(
        IDictionary<string, IList<string>> translations,
        string startString)
    {
        var permutations = new HashSet<string>();

        foreach (var translationAndSubstitutions in translations)
        {
            var toFind = translationAndSubstitutions.Key;
            var substitutions = translationAndSubstitutions.Value;

            foreach (var substitution in substitutions)
            {
                var replacements = getReplacements(
                    startString,
                    toFind,
                    substitution);

                foreach (var replacement in replacements)
                {
                    permutations.Add(replacement);
                }
            }
        }

        return permutations.Count;
    }

    private IEnumerable<string> getReplacements(
        string startString,
        string toFind,
        string substitution)
    {
        int index = startString.IndexOf(toFind);

        while (index != -1)
        {
            var firstHalf = startString.Substring(0, index);
            var secondHalf = startString.Substring(index + toFind.Length);

            var permute = firstHalf + substitution + secondHalf;
            yield return permute;

            index = startString.IndexOf(toFind, index + 1);
        }
    }

    private long findFewestSteps(
        IDictionary<string, IList<string>> translations,
        string targetString)
    {

        return findFewestSteps(
            new Dictionary<string, long>() { {singleElectron, 0} },
            translations,
            targetString);
    }

    private long findFewestSteps(
        IDictionary<string, long> evaluatingStrings,
        IDictionary<string, IList<string>> translations,
        string targetString)
    {
        if (evaluatingStrings.TryGetValue(targetString, out long steps))
        {
            return steps;
        }

        var postReplacements = new Dictionary<string, long>();

        foreach (var evaluatingString in evaluatingStrings.Keys)
        {
            foreach (var translationAndSubstitutions in translations)
            {
                var translation = translationAndSubstitutions.Key;
                var substitutions = translationAndSubstitutions.Value;

                foreach (var substitution in substitutions)
                {
                    var replacements = getReplacements(
                        evaluatingString,
                        translation,
                        substitution);

                    foreach (var replacement in replacements)
                    {
                        postReplacements.Add(
                            replacement,
                            evaluatingStrings[evaluatingString] + 1);
                    }
                }
            }
        }

        foreach (var evaluatingString in evaluatingStrings.Keys)
        {
            foreach (var other in evaluatingStrings.Keys)
            {
                postReplacements[evaluatingString + other] =
                    evaluatingStrings[evaluatingString]
                    + evaluatingStrings[other];
            }
        }

        return 1 + findFewestSteps(
            postReplacements,
            translations,
            targetString);
    }

    private IDictionary<string, IList<string>> getTranslations(
        IList<string> input)
    {
        var translations = new Dictionary<string, IList<string>>();

        foreach (var line in input)
        {
            if (line == String.Empty)
            {
                break;
            }

            var split = line.Split(" => ");

            if (!translations.ContainsKey(split[0]))
            {
                translations[split[0]] = new List<string>();
            }

            translations[split[0]].Add(split[1]);
        }

        return translations;
    }

    private string getStartString(IList<string> input) => input.Last();

    protected override long part1Work(string[] input) =>
        work(input, findPermutations);

    protected override long part2Work(string[] input) =>
        work(input, findFewestSteps);
}
