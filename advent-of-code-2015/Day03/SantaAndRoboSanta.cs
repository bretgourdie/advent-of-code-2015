namespace advent_of_code_2015.Day03;
internal class SantaAndRoboSanta : IDeliveryStrategy
{
    Point2D santaPosition;
    Point2D roboSantaPosition;
    bool isSantaTurn;

    public SantaAndRoboSanta()
    {
        santaPosition = Point2D.Zero;
        roboSantaPosition = Point2D.Zero;
        isSantaTurn = true;
    }

    public void Visit(ISet<Point2D> visited)
    {
        visit(santaPosition, visited);
        visit(roboSantaPosition, visited);
    }

    private void visit(Point2D pos, ISet<Point2D> visited)
    {
        if (!visited.Contains(pos))
        {
            visited.Add(pos);
        }
    }

    public void Move(char letter)
    {
        if (isSantaTurn)
        {
            santaPosition = move(santaPosition, letter);
        }

        else
        {
            roboSantaPosition = move(roboSantaPosition, letter);
        }

        isSantaTurn = !isSantaTurn;
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
