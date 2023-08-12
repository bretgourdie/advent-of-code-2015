namespace advent_of_code_2015.Common;
internal struct Point2D
{
    public readonly long X;
    public readonly long Y;

    public Point2D(
        long x,
        long y)
    {
        X = x;
        Y = y;
    }

    public static Point2D Zero => new Point2D(0, 0);
}
