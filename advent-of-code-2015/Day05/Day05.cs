namespace advent_of_code_2015.Day05;
internal class Day05 : AdventSolution
{
    private readonly IEnumerable<char> vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
    private readonly IEnumerable<string> badWords = new string[] { "ab", "cd", "pq", "xy" };

    protected override long part1ExampleExpected => 0;

    protected override long part1InputExpected => 258;

    protected override long part2ExampleExpected => 2;

    protected override long part2InputExpected => 53;

    private long work(
        string[] input,
        Func<string, bool> isNice)
    {
        long niceStrings = 0;

        foreach (var word in input)
        {
            niceStrings += isNice(word) ? 1 : 0;
        }

        return niceStrings;
    }

    private bool isNiceRidiculous(string word)
    {
        return
            hasAtLeastThreeVowels(word)
            && hasLetterTwiceInARow(word)
            && !hasBadStrings(word);
    }

    private bool hasAtLeastThreeVowels(string word)
    {
        return word.Count(x => vowels.Contains(x)) >= 3;
    }

    private bool hasLetterTwiceInARow(string word)
    {
        for (int ii = 0; ii < word.Length - 1; ii++)
        {
            if (word[ii] == word[ii + 1])
                return true;
        }
        return false;
    }

    private bool hasBadStrings(string word)
    {
        return badWords.Any(badWord =>
            word.Contains(badWord));
    }

    private bool isNiceReal(string word)
    {
        return hasPairAtLeastTwiceNotOverlapping(word)
            && hasAtLeastOneLetterWhichRepeatsWithLetterBetween(word);
    }

    private bool hasPairAtLeastTwiceNotOverlapping(string word)
    {
        string previousPair = String.Empty;
        var pairs = new HashSet<string>();

        for (int ii = 0; ii < word.Length - 1; ii++)
        {
            var pair = word.Substring(ii, 2);

            if (pairs.Contains(pair))
            {
                if (previousPair != pair)
                {
                    return true;
                }
            }

            else
            {
                pairs.Add(pair);
            }

            previousPair = pair;
        }

        return false;
    }

    private bool hasAtLeastOneLetterWhichRepeatsWithLetterBetween(string word)
    {
        for (int ii = 0; ii < word.Length - 2; ii++)
        {
            var sub = word.Substring(ii, 3);
            if (sub[0] == sub[2])
            {
                return true;
            }
        }

        return false;
    }

    protected override long part1Work(string[] input) =>
        work(input, isNiceRidiculous);

    protected override long part2Work(string[] input) =>
        work(input, isNiceReal);
}
