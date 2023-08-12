namespace advent_of_code_2015.Day02;
internal class Day02 : AdventSolution
{
    protected override long part1ExampleExpected => 58 + 43;

    protected override long part1InputExpected => 1588178;

    protected override long part2ExampleExpected => 34 + 14;

    protected override long part2InputExpected => 3783758;

    private long work(string[] input,
        Func<long, long, long, long> lengthCalculation)
    {
        return input.Sum(
            x =>
            {
                var split = x.Split('x');

                var l = long.Parse(split[0]);
                var w = long.Parse(split[1]);
                var h = long.Parse(split[2]);

                return lengthCalculation(l, w, h);
            });
    }

    private long wrappingPaper(
        long l,
        long w,
        long h)
    {
        return
            2 * l * w
            + 2 * w * h
            + 2 * h * l
            + smallestArea(l, w, h);
    }

    private long ribbon(
        long l,
        long w,
        long h)
    {
        return shortestDistance(l, w, h)
            + l * w * h;
    }

    private long shortestDistance(
        long l,
        long w,
        long h)
    {
        return smallest(l, w, h,
            (a, b) => a + a + b + b);
    }


    private long smallestArea(
        long l,
        long w,
        long h) =>
        smallest(l, w, h,
            (a, b) => a * b);

    private long smallest(
        long l,
        long w,
        long h,
        Func<long, long, long> eval)
    {
        return Math.Min(
            Math.Min(
                eval(l, w), eval(l, h)),
            eval(w, h));
    }

    protected override long part1Work(string[] input) =>
        work(input, wrappingPaper);

    protected override long part2Work(string[] input) =>
        work(input, ribbon);
}
