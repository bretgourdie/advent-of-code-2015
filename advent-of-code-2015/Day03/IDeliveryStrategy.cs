namespace advent_of_code_2015.Day03;
internal interface IDeliveryStrategy
{
    void Move(char letter);
    void Visit(ISet<Point2D> visited);
}
