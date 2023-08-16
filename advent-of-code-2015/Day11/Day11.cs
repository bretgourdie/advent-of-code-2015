namespace advent_of_code_2015.Day11;
internal class Day11 : AdventSolutionTemplate<string, string>
{
    private readonly IEnumerable<char> ambiguousCharacters = new char[] { 'i', 'o', 'l' };

    protected override string part1ExampleExpected => "abcdffaa";
    protected override string part1InputExpected => "cqjxxyzz";

    protected override string part2ExampleExpected => "abcdffbb";
    protected override string part2InputExpected => "cqkaabcc";

    private string work(string input)
    {
        var currentPassword = input;
        var alphabet = generateAlphabet().ToList();

        while (true)
        {
            var workingPassword = new string(currentPassword.Reverse().ToArray());

            bool needToCarry = true;
            for (int ii = 0; ii < workingPassword.Length && needToCarry; ii++)
            {
                var letter = workingPassword[ii];

                var newLetter = alphabet[(letter - alphabet.First() + 1) % alphabet.Count];

                var firstHalf = workingPassword.Substring(0, ii);
                var secondHalf = workingPassword.Substring(ii + 1, workingPassword.Length - ii - 1);

                workingPassword = firstHalf + newLetter + secondHalf;

                needToCarry = letter == alphabet.Last();
            }

            currentPassword = new string(workingPassword.Reverse().ToArray());

            var one = hasIncreasing3Straight(currentPassword);
            var two = onlyHasUnambiguousCharacters(currentPassword);
            var three = hasTwoDifferentPairs(currentPassword);

            if (one && two && three)
            {
                return currentPassword;
            }
        }
    }

    private bool hasIncreasing3Straight(string password)
    {
        for (int ii = 0; ii + 2 < password.Length; ii++)
        {
            char first = password[ii];
            char second = password[ii + 1];
            char third = password[ii + 2];

            var straightOneTwo = (first + 1) == second;
            var straightTwoThree = (second + 1) == third;

            if (straightOneTwo && straightTwoThree)
            {
                return true;
            }
        }

        return false;
    }

    private bool onlyHasUnambiguousCharacters(string password)
    {
        return password.All(x => !ambiguousCharacters.Contains(x));
    }

    private bool hasTwoDifferentPairs(string password)
    {
        int index = 0;

        var substrings = new HashSet<string>();

        while (index + 1 < password.Length)
        {
            var substring = password.Substring(index, 2);
            if (substring[0] == substring[1])
            {
                substrings.Add(substring);
                index += 1;
            }

            index += 1;
        }

        return substrings.Count >= 2;
    }

    private IEnumerable<char> generateAlphabet()
    {
        for (char letter = 'a'; letter <= 'z'; letter++)
        {
            yield return letter;
        }
    }

    protected override string part1Work(string[] input) =>
        work(input.Single());

    protected override string part2Work(string[] input) =>
        work(work(input.Single()));
}
