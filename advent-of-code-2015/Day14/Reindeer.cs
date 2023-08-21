namespace advent_of_code_2015.Day14;
internal class Reindeer
{
    private TravelState travelState;

    public readonly string Name;

    public long Score { get; private set; }

    public long DistanceTraveled { get; private set; }

    public Reindeer(string line)
    {
        var split = line.Split(' ');

        Name = split[0];
        var travelSpeed = long.Parse(split[3]);
        var travelTime = long.Parse(split[6]);
        var restTime = long.Parse(split[13]);

        travelState = new MovingState(
            travelSpeed,
            travelTime,
            restTime,
            distanceTraveled: 0);
    }

    public void AwardPoint()
    {
        Score += 1;
    }

    public void Update()
    {
        travelState = travelState.Update();
        DistanceTraveled = travelState.DistanceTraveled;
    }

    public override string ToString() => Name;
}
