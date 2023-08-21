using System.Runtime.CompilerServices;

namespace advent_of_code_2015.Day14;
internal class Day14 : AdventSolution
{
    private const int seconds = 2503;

    protected override long part1Work(string[] input)
    {
        var allReindeer = new List<Reindeer>();

        foreach (var line in input)
        {
            allReindeer.Add(new Reindeer(line));
        }

        for (int ii = 0; ii < seconds; ii++)
        {
            foreach (var reindeer in allReindeer)
            {
                reindeer.Update();
            }
        }

        return allReindeer.Max(x => x.DistanceTraveled);
    }

    protected override long part1ExampleExpected => 2660;
    protected override long part1InputExpected => 2696;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
