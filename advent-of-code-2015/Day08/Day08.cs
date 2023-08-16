namespace advent_of_code_2015.Day08;
internal class Day08 : AdventSolution
{
    protected override long part1ExampleExpected => 12;
    protected override long part1InputExpected => 1350;

    protected override long part2ExampleExpected => 19;
    protected override long part2InputExpected => 2085;

    protected long work(
        string[] input,
        Func<StringAppraisal, string, long> minuendFunction,
        Func<StringAppraisal, string, long> subtrahendFunction)
    {
        long minuend = 0, subtrahend = 0;
        var appraisal = new StringAppraisal();

        foreach (var line in input)
        {
            long codeSize = appraisal.GetCodeSize(line);
            long inMemorySize = appraisal.GetInMemorySize(line);

            long minuendCurrent = minuendFunction(appraisal, line);
            long subtrahendCurrent = subtrahendFunction(appraisal, line);

            minuend += minuendCurrent;
            subtrahend += subtrahendCurrent;
        }

        return minuend - subtrahend;
    }

    protected override long part1Work(string[] input) =>
        work(
            input,
            (sa, s) => sa.GetCodeSize(s),
            (sa, s) => sa.GetInMemorySize(s));

    protected override long part2Work(string[] input) =>
        work(
            input,
            (sa, s) => sa.GetEncodedSize(s),
            (sa, s) => sa.GetCodeSize(s));
}
