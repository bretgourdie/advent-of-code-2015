using System.Drawing;

namespace advent_of_code_2015.Day18;
internal class Day18 : AdventSolution
{
    private const char on = '#';
    private const char off = '.';

    protected override long part1ExampleExpected => 4;
    protected override long part1InputExpected => -1;
    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }

    private long work(string[] input)
    {
        int bounds = input.Length;
        var steps = getSteps(bounds);

        var turnedOnLights = getInitialTurnedOnLights(input);

        for (int step = 0; step < steps; step++)
        {
            var turnOns = new HashSet<Point2D>();
            var turnOffs = new HashSet<Point2D>();

            foreach (var turnedOnLight in turnedOnLights)
            {
                var turnedOnLightNeighbors = getNeighbors(turnedOnLight, bounds);

                if (!shouldKeepOn(numberTurnedOn(turnedOnLightNeighbors, bounds, turnedOnLights)))
                {
                    turnOffs.Add(turnedOnLight);
                }

                foreach (var potentialLight in turnedOnLightNeighbors.Where(neighbor => !turnedOnLights.Contains(neighbor)))
                {
                    var neighbors = getNeighbors(potentialLight, bounds);

                    if (shouldTurnOn(numberTurnedOn(neighbors, bounds, turnedOnLights)))
                    {
                        turnOns.Add(potentialLight);
                    }
                }
            }

            foreach (var turnOff in turnOffs)
            {
                turnedOnLights.Remove(turnOff);
            }

            foreach (var turnOn in turnOns)
            {
                turnedOnLights.Add(turnOn);
            }
        }

        return turnedOnLights.Count;
    }

    private bool shouldTurnOn(int neighborsOn) => neighborsOn == 3;

    private bool shouldKeepOn(
        int neighborsOn) => neighborsOn >= 2 && neighborsOn <= 3;

    private IEnumerable<Point2D> getNeighbors(
        Point2D light,
        int bounds)
    {
        for (int ii = -1; ii <= 1; ii++)
        {
            for (int jj = -1; jj <= 1; jj++)
            {
                var newX = light.X + ii;
                var newY = light.Y + jj;

                if (newX >= 0
                    && newX < bounds
                    && newY >= 0
                    && newY < bounds
                    && !(newX == light.X && newY == light.Y))
                {
                    yield return new Point2D(newX, newY);
                }
            }
        }
    }

    private int numberTurnedOn(
        IEnumerable<Point2D> potentialTurnedOnLights,
        int bounds,
        ISet<Point2D> turnedOnLights)
    {
        var lightsThatAreOn = potentialTurnedOnLights.Count(potentialLight => turnedOnLights.Contains(potentialLight));

        return lightsThatAreOn;
    }

    private ISet<Point2D> getInitialTurnedOnLights(IList<string> input)
    {
        var lights = new HashSet<Point2D>();

        for (int ii = 0; ii < input.Count; ii++)
        {
            for (int jj = 0; jj < input[ii].Length; jj++)
            {
                if (input[ii][jj] == on)
                {
                    lights.Add(
                        new Point2D(
                            ii,
                            jj));
                }
            }
        }

        return lights;
    }

    private int getSteps(int bounds)
    {
        if (bounds < 100)
        {
            return 4;
        }

        return 100;
    }

    protected override long part1Work(string[] input) =>
        work(input);

    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }
}
