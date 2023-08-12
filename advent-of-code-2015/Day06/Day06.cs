using System.Data;

namespace advent_of_code_2015.Day06;
internal class Day06 : AdventSolution
{
    protected override long part1ExampleExpected => 1002;

    protected override long part1InputExpected => 377891;

    protected override long part2ExampleExpected => 2002;

    protected override long part2InputExpected => 14110788;

    private long work(string[] input, ILightStrategy lightStrategy)
    {
        var grid = new long[1000,1000];

        foreach (var line in input)
        {
            var split = line.Split(' ');

            var action = getAction(split[0], split[1], lightStrategy);

            var startSplit = getPoint(split[1], split[2]);
            var xStart = long.Parse(startSplit[0]);
            var yStart = long.Parse(startSplit[1]);

            var endSplit = getPoint(split[3], split.Length > 4 ? split[4] : split[3]);
            var xEnd = long.Parse(endSplit[0]);
            var yEnd = long.Parse(endSplit[1]);

            for (long x = xStart; x <= xEnd; x++)
            {
                for (long y = yStart; y <= yEnd; y++)
                {
                    action.Invoke(x, y, grid);
                }
            }
        }

        long lightsOn = 0;
        for (long x = 0; x < grid.GetLength(0); x++)
        {
            for (long y = 0; y < grid.GetLength(1); y++)
            {
                lightsOn += grid[y, x];
            }
        }

        return lightsOn;
    }

    private string[] getPoint(
        string str1,
        string str2)
    {
        const char coordinateSeparator = ',';

        return str1.Contains(coordinateSeparator)
            ? str1.Split(coordinateSeparator)
            : str2.Split(coordinateSeparator);
    }

    private Action<long, long, long[,]> getAction(
        string command1,
        string command2,
        ILightStrategy strategy)
    {
        string command = command1 +
            (command1 == "turn"
                ? " " + command2
                : String.Empty);

        switch (command)
        {
            case "turn on":
                return strategy.TurnOn;
            case "turn off":
                return strategy.TurnOff;
            case "toggle":
                return strategy.Toggle;
            default:
                throw new ArgumentException(nameof(command));
        }
    }

    protected override long part1Work(string[] input) =>
        work(input, new WithoutBrightness());

    protected override long part2Work(string[] input) =>
        work(input, new WithBrightness());
}
