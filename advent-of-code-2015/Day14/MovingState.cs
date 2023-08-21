namespace advent_of_code_2015.Day14;
internal class MovingState : TravelState
{
    private long timeInState = 0;

    public MovingState(
        long travelSpeed,
        long travelTime,
        long restTime,
        long distanceTraveled) :
        base(
            travelSpeed,
            travelTime,
            restTime,
            distanceTraveled) { }

    public override TravelState Update()
    {
        timeInState += 1;
        DistanceTraveled += travelSpeed;

        if (timeInState >= travelTime)
        {
            return new RestState(
                travelSpeed,
                travelTime,
                restTime,
                DistanceTraveled);
        }

        return this;
    }
}
