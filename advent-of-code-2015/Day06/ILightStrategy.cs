namespace advent_of_code_2015.Day06;
internal interface ILightStrategy
{
    void TurnOn(long x, long y, long[,] grid);
    void TurnOff(long x, long y, long[,] grid);
    void Toggle(long x, long y, long[,] grid);
}
