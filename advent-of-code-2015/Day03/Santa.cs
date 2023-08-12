namespace advent_of_code_2015.Day03;
internal class Santa : IDeliveryStrategy
{
    long x, y;

    public Santa()
    {
        x = 0;
        y = 0;
    }

    public void Visit(ISet<Point2D> visited)
    {
        var pos = new Point2D(x, y);

        if (!visited.Contains(pos))
        {
            visited.Add(pos);
        }
    }

    public void Move(char letter)
    {
        switch (letter)
        {
            case '>': x++; break;
            case '<': x--; break;
            case '^': y++; break;
            case 'v': y--; break;
        }
    }
}
