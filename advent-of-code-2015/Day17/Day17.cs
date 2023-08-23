namespace advent_of_code_2015.Day17;
internal class Day17 : AdventSolution
{
    private const int small_capacity = 25;
    private const int large_capacity = 150;
    private const char separator = ',';

    protected override long part1ExampleExpected => 4;
    protected override long part1InputExpected => 654;
    protected override long part2ExampleExpected => 3;
    protected override long part2InputExpected => 57;

    private long work(
        string[] input,
        Func<IDictionary<string, long>, int, long> getAnswer)
    {
        int capacity = getCapacity(input.Length);

        var combinations = new Dictionary<string, long>();

        var jars = new List<Jar>();
        foreach (var line in input)
        {
            jars.Add(new Jar(line));
        }

        return getAnswer(
            combinationsOfContainers(
                jars,
                capacity,
                new List<Jar>(),
                combinations),
            capacity
        );
    }

    private IDictionary<string, long> combinationsOfContainers(
        IList<Jar> jars,
        int capacity,
        IList<Jar> currentStack,
        IDictionary<string, long> combinations)
    {
        var currentStackSum = currentStack.Sum(x => x.Capacity);

        var stack = String.Join(separator, currentStack.Select(x => x.ToString()).OrderBy(x => x));

        if (combinations.ContainsKey(stack))
        {
            return combinations;
        }

        combinations.Add(stack, currentStackSum);

        if (currentStackSum == capacity)
        {
            return combinations;
        }

        else if (currentStackSum > capacity)
        {
            return combinations;
        }

        foreach (var jar in jars)
        {
            currentStack.Add(jar);
            combinationsOfContainers(
                jars.Where(x => x != jar).ToList(),
                capacity,
                currentStack,
                combinations);
            currentStack.Remove(jar);
        }

        return combinations;
    }

    private int getCapacity(int inputLength)
    {
        if (inputLength == 5)
        {
            return small_capacity;
        }

        return large_capacity;
    }

    private long numberOfCombinations(IDictionary<string, long> combinations, int capacity) =>
        combinations.Count(x => x.Value == capacity);

    private long minimumNumberOfContainers(IDictionary<string, long> combinations, int capacity)
    {
        var correctCombinations = combinations.Where(kv => kv.Value == capacity);

        var minSeparators = correctCombinations.Min(kv => kv.Key.Count(letter => letter == separator));

        var correctMinimumCombinations =
            correctCombinations.Where(kv => kv.Key.Count(letter => letter == separator) == minSeparators);

        return correctMinimumCombinations.Count();
    }

    protected override long part1Work(string[] input) =>
        work(input, numberOfCombinations);

    protected override long part2Work(string[] input) =>
        work(input, minimumNumberOfContainers);
}
