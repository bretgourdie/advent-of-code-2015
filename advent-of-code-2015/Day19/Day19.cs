namespace advent_of_code_2015.Day19;
internal class Day19 : AdventSolution
{
    protected override long part1ExampleExpected => 7;
    protected override long part1InputExpected => 509;
    protected override long part2ExampleExpected => 6;
    protected override long part2InputExpected => -1;

    private long work(string[] input)
    {
        var translations = getTranslations(input);
        var startString = getStartString(input);

        return findPermutations(translations, startString);
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
                int index = startString.IndexOf(toFind);

                while (index != -1)
                {
                    var firstHalf = startString.Substring(0, index);
                    var secondHalf = startString.Substring(index + toFind.Length);

                    var permute = firstHalf + substitution + secondHalf;
                    permutations.Add(permute);

                    index = startString.IndexOf(toFind, index + 1);
                }
            }
        }

        return permutations.Count;
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
        work(input);

    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }
}
