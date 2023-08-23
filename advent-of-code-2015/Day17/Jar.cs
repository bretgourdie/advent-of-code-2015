namespace advent_of_code_2015.Day17;
internal class Jar
{
    private readonly DateTime hashTime;
    public readonly int Capacity;

    public Jar(string line)
    {
        hashTime = DateTime.Now;
        Capacity = int.Parse(line);
    }
    
    public override int GetHashCode() =>
        hashTime.GetHashCode() * 17 * Capacity;

    public override string ToString() =>
        $"{Capacity}({GetHashCode()}";
}
