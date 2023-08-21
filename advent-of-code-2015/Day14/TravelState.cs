namespace advent_of_code_2015.Day14;
internal abstract class TravelState
{
    protected readonly long travelSpeed,
        travelTime,
        restTime;

    public long DistanceTraveled { get; protected set; }

    protected TravelState(
        long travelSpeed,
        long travelTime,
        long restTime,
        long distanceTraveled)
    {
        this.travelSpeed = travelSpeed;
        this.travelTime = travelTime;
        this.restTime = restTime;
        this.DistanceTraveled = distanceTraveled;
    }

    public abstract TravelState Update();
}
