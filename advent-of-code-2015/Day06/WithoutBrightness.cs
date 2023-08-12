namespace advent_of_code_2015.Day06;
internal class WithoutBrightness : ILightStrategy
{
    public void TurnOn(
        long x,
        long y,
        long[,] grid)
    {
        grid[y, x] = 1;
    }

    public void TurnOff(
        long x,
        long y,
        long[,] grid)
    {
        grid[y, x] = 0;
    }

    public void Toggle(
        long x,
        long y,
        long[,] grid)
    {
        grid[y, x] = grid[y, x] == 1
            ? 0
            : 1;
    }

}
