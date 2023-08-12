namespace advent_of_code_2015.Day01;
internal class Day01 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        input.Single().Count(x => x == '(') - input.Single().Count(x => x == ')');


    protected override long part1ExampleExpected => -1;
    protected override long part1InputExpected => 232;
    protected override long part2Work(string[] input)
    {
        var directions = input.Single();
        long floor = 0;
        for (int ii = 0; ii < directions.Length; ii++)
        {
            var letter = directions[ii];
            switch (letter)
            {
                case ')':
                    floor -= 1;
                    break;
                case '(':
                    floor += 1;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (floor == -1)
            {
                return ii + 1;
            }
        }

        throw new ArgumentException(nameof(input));
    }

    protected override long part2ExampleExpected => 5;
    protected override long part2InputExpected => 1783;
}
