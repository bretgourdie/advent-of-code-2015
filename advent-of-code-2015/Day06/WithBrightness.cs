namespace advent_of_code_2015.Day06;
internal class WithBrightness : ILightStrategy
{
    public void Toggle(long x, long y, long[,] grid)
    {
        grid[y, x] += 2;
    }

    public void TurnOff(long x, long y, long[,] grid)
    {
        grid[y, x] = Math.Max(0, grid[y, x] - 1);
    }

    public void TurnOn(long x, long y, long[,] grid)
    {
        grid[y, x] += 1;
    }
}
