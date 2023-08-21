namespace advent_of_code_2015.Day13;
internal class Day13 : AdventSolution
{
    const char space = ' ';

    protected override long part1ExampleExpected => 330;

    protected override long part1InputExpected => 733;

    protected override long part2ExampleExpected => 286;

    protected override long part2InputExpected => 725;

    private long work(
        string[] input,
        Action<IList<Guest>> seatingStrategy)
    {
        return bestPermute(
            getGuestList(input, seatingStrategy),
            new List<Guest>(),
            long.MinValue);
    }

    private long bestPermute(
        IList<Guest> availableGuests,
        IList<Guest> table,
        long incomingBest)
    {
        if (!availableGuests.Any())
        {
            return determineValue(table);
        }

        var best = incomingBest;

        foreach (var guest in availableGuests)
        {
            table.Add(guest);

            var guestsLeft = availableGuests.Where(x => x.Name != guest.Name).ToList();

            best = Math.Max(
                best,
                bestPermute(
                    guestsLeft,
                    table,
                    best)
                );

            table.Remove(guest);
        }

        return best;
    }

    private long determineValue(IList<Guest> table)
    {
        long value = 0;

        for (int index = 0; index < table.Count; index++)
        {
            var guest = table[index];

            var circularIndex = index + table.Count;

            var leftGuestIndex = (circularIndex - 1) % table.Count;
            var rightGuestIndex = (circularIndex + 1) % table.Count;

            var leftGuest = table[leftGuestIndex];
            var rightGuest = table[rightGuestIndex];

            var leftGuestValue = guest.Appraise(leftGuest);
            var rightGuestValue = guest.Appraise(rightGuest);

            value += leftGuestValue + rightGuestValue;
        }

        return value;
    }

    private IList<Guest> getGuestList(
        IList<string> input,
        Action<IList<Guest>> seatingStrategy)
    {
        var guestList = new List<Guest>();

        var guestCount = getGuestCount(input);

        for (int guestIndex = 0; guestIndex < guestCount; guestIndex++)
        {
            var linesToSkip = guestIndex * (guestCount - 1);

            var name = getGuestName(input[linesToSkip]);
            var lines = input.Skip(linesToSkip).Take(guestCount - 1).ToList();

            var guest = new Guest(name, lines);

            guestList.Add(guest);
        }

        seatingStrategy(guestList);

        return guestList;
    }

    private int getGuestCount(IList<string> input)
    {
        var firstGuestName = getGuestName(input.First());

        int guests = 1;

        while (firstGuestName == getGuestName(input[guests - 1]))
        {
            guests += 1;
        }

        return guests;
    }

    private string getGuestName(string line)
    {
        return line.Split(space).First();
    }

    private void seatEveryoneElse(IList<Guest> guestList) { }

    private void seatYourself(IList<Guest> guestList)
    {
        guestList.Add(new Guest("Yourself"));
    }

    protected override long part1Work(string[] input) =>
        work(input, seatEveryoneElse);

    protected override long part2Work(string[] input) =>
        work(input, seatYourself);
}
