namespace advent_of_code_2015.Day01;
internal class Day01 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        input.Single().Count(x => x == '(') - input.Single().Count(x => x == ')');


    protected override long part1ExampleExpected => 3;
    protected override long part1InputExpected => 232;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
