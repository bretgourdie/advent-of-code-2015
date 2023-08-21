namespace advent_of_code_2015.Day14;
internal class RestState : TravelState
{
    private long timeInState = 0;

    public RestState(
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

        if (timeInState >= restTime)
        {
            return new MovingState(
                travelSpeed,
                travelTime,
                restTime,
                DistanceTraveled);
        }

        return this;
    }
}
