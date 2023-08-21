namespace advent_of_code_2015.Day14;
internal class Day14 : AdventSolution
{
    private const int seconds = 2503;

    protected override long part1ExampleExpected => 2660;
    protected override long part1InputExpected => 2696;
    protected override long part2ExampleExpected => 1564;
    protected override long part2InputExpected => 1084;

    protected long work(
        string[] input,
        Action<IEnumerable<Reindeer>> afterRoundAction,
        Func<IEnumerable<Reindeer>, long> getAnswer)
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

            afterRoundAction(allReindeer);
        }

        return getAnswer(allReindeer);
    }

    private void doNothing(IEnumerable<Reindeer> allReindeer) { }

    private void scoreFarthest(IEnumerable<Reindeer> allReindeer)
    {
        var maxDistance = farthestTraveled(allReindeer);

        var allWinningReindeer = allReindeer.Where(x => x.DistanceTraveled == maxDistance);

        foreach (var winningReindeer in allWinningReindeer)
        {
            winningReindeer.AwardPoint();
        }
    }

    private long farthestTraveled(IEnumerable<Reindeer> allReindeer) => allReindeer.Max(x => x.DistanceTraveled);

    private long bestScore(IEnumerable<Reindeer> allReindeers) => allReindeers.Max(x => x.Score);

    protected override long part1Work(string[] input) =>
        work(input, doNothing, farthestTraveled);

    protected override long part2Work(string[] input) =>
        work(input, scoreFarthest, bestScore);
}
