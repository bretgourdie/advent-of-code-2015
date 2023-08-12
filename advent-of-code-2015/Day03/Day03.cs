namespace advent_of_code_2015.Day03;
internal class Day03 : AdventSolution
{
    protected override long part1ExampleExpected => 2;

    protected override long part1InputExpected => 2565;

    protected override long part2ExampleExpected => 11;

    protected override long part2InputExpected => 2639;

    protected void visit(
        long x,
        long y,
        ISet<Point2D> visited)
    {
        var pos = new Point2D(x, y);

        if (!visited.Contains(pos))
        {
            visited.Add(pos);
        }
    }

    protected override long part1Work(string[] input) =>
        work(input, new Santa());

    protected override long part2Work(string[] input) =>
        work(input, new SantaAndRoboSanta());

    private long work(
        string[] input,
        IDeliveryStrategy deliveryStrategy)
    {
        long x = 0, y = 0;

        var line = input.Single();

        var visited = new HashSet<Point2D>();

        foreach (var letter in line)
        {
            deliveryStrategy.Visit(visited);
            deliveryStrategy.Move(letter);
        }

        deliveryStrategy.Visit(visited);

        return visited.Count;
    }
}
