namespace advent_of_code_2015.Day20;
internal class Day20 : AdventSolution
{
    protected override long part1ExampleExpected => 8;
    protected override long part1InputExpected => 776160;

    protected override long part2ExampleExpected => 6;
    protected override long part2InputExpected => 786240;

    protected long work(
        string[] input,
        long presentsPerHouse,
        Func<long, long, bool> isInDeliverableHouse)
    {
        var presentThreshold = long.Parse(input.Single());
        long house, deliveredPresents = 0;

        for (house = 1; deliveredPresents < presentThreshold; house++)
        {
            deliveredPresents = 0;
            var squareRoot = Math.Sqrt(house);
            for (long elfIndex = 1; elfIndex <= squareRoot; elfIndex++)
            {
                if (house % elfIndex == 0)
                {
                    if (isInDeliverableHouse(house, elfIndex))
                    {
                        deliveredPresents += elfIndex * presentsPerHouse;
                    }

                    if (elfIndex != house / elfIndex)
                    {
                        if (elfIndex * elfIndex != house)
                        {
                            if (isInDeliverableHouse(house, house / elfIndex))
                            {
                                deliveredPresents += (house / elfIndex) * presentsPerHouse;
                            }
                        }
                    }
                }
            }
        }

        return house - 1;
    }

    private bool infiniteHousesDelivered(long house, long subHouse) => true;

    private bool first50HousesDelivered(long house, long subHouse) => house <= subHouse * 50;

    protected override long part1Work(string[] input) =>
        work(input, 10, infiniteHousesDelivered);

    protected override long part2Work(string[] input) =>
        work(input, 11, first50HousesDelivered);
}
