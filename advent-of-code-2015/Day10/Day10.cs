using System.Text;

namespace advent_of_code_2015.Day10;
internal class Day10 : AdventSolution
{
    protected override long part1ExampleExpected => 82350;
    protected override long part1InputExpected => 329356;

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }

    protected override long part1Work(string[] input)
    {
        var strNumber = input.Single();

        for (int iteration = 0; iteration < 40; iteration++)
        {
            int index = 0;
            var sb = new StringBuilder();

            while (index < strNumber.Length)
            {
                var number = strNumber[index];
                int count = 1;

                while (index + 1 < strNumber.Length && strNumber[index + 1] == number)
                {
                    count += 1;
                    index += 1;
                }

                sb.Append(count);
                sb.Append(number);

                index += 1;
            }

            strNumber = sb.ToString();
        }

        return strNumber.Length;
    }

    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }
}
