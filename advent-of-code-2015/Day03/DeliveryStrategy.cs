namespace advent_of_code_2015.Day03;
internal class DeliveryStrategy
{
    private readonly IList<Point2D> visitors;
    private int turn;

    public DeliveryStrategy(int numberOfVisitors)
    {
        visitors = new List<Point2D>();

        for (int ii = 0; ii < numberOfVisitors; ii++)
        {
            visitors.Add(Point2D.Zero);
        }
    }

    public void Visit(ISet<Point2D> visited)
    {
        foreach (var visitor in visitors)
        {
            visited.Add(visitor);
        }
    }

    public void Move(char letter)
    {
        visitors[turn] = move(visitors[turn], letter);
        turn = (turn + 1) % visitors.Count;
    }

    private Point2D move(
        Point2D pos,
        char letter)
    {
        long x = pos.X;
        long y = pos.Y;

        switch (letter)
        {
            case '>': x++; break;
            case '<': x--; break;
            case '^': y++; break;
            case 'v': y--; break;
        }

        return new Point2D(x, y);
    } 
}
