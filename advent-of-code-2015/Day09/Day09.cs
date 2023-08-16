namespace advent_of_code_2015.Day09;
internal class Day09 : AdventSolution
{
    protected override long part1ExampleExpected => 605;
    protected override long part1InputExpected => 251;

    protected override long part2ExampleExpected => 982;
    protected override long part2InputExpected => 898;

    private long work(
        string[] input,
        long worst,
        Func<long, long, long> heuristic)
    {
        var nodeToOptions = new Dictionary<string, IDictionary<string, long>>();

        foreach (var line in input)
        {
            var equalSplit = line.Split(" = ");
            var distance = long.Parse(equalSplit[1]);
            var places = equalSplit[0].Split(" to ");

            for (int ii = 0; ii < places.Length; ii++)
            {
                var currentPlace = places[ii];
                var targetPlace = places[(ii + 1) % places.Length];

                if (!nodeToOptions.ContainsKey(currentPlace))
                {
                    nodeToOptions[currentPlace] = new Dictionary<string, long>();
                }

                nodeToOptions[currentPlace][targetPlace] = distance;
            }
        }

        long currentBest = worst;

        foreach (var start in nodeToOptions.Keys)
        {
            var visited = new HashSet<string>();
            visited.Add(start);
            var evaluating = findTargetDistance(start, 0, nodeToOptions, visited, worst, heuristic);

            currentBest = heuristic(currentBest, evaluating);
        }

        return currentBest;
    }

    private long findTargetDistance(
        string current,
        long currentLength,
        IDictionary<string, IDictionary<string, long>> nodeToOptions,
        ISet<string> visited,
        long worst,
        Func<long, long, long> heuristic)
    {
        visited.Add(current);

        if (visited.Count == nodeToOptions.Keys.Count)
        {
            return currentLength;
        }

        long bestPath = worst;

        var remainingLocations = nodeToOptions.Keys.Where(x => !visited.Contains(x));

        foreach (var location in remainingLocations)
        {
            var cost = nodeToOptions[current][location];
            var evaluating = findTargetDistance(
                location,
                currentLength + cost,
                nodeToOptions,
                new HashSet<string>(visited),
                worst,
                heuristic);

            bestPath = heuristic(bestPath, evaluating);
        }

        return bestPath;
    }

    protected override long part1Work(string[] input) =>
        work(input,
            long.MaxValue,
            Math.Min);

    protected override long part2Work(string[] input) =>
        work(input,
            long.MinValue,
            Math.Max);

}
