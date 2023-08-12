using System.Security.Cryptography;
using System.Text;

namespace advent_of_code_2015.Day04;
internal class Day04 : AdventSolution
{
    protected override long part1ExampleExpected => 609043;

    protected override long part1InputExpected => 282749;

    protected override long part2ExampleExpected => 6742839;

    protected override long part2InputExpected => 9962624;

    private long work(string[] input,
        string target)
    {
        var key = input.Single();
        long ii = 1;

        while (true)
        {
            var hashed = MD5Hash($"{key}{ii}");
            if (hashed.StartsWith(target))
            {
                return ii;
            }

            ii += 1;
        }
    }

    private string MD5Hash(string input)
    {
        using (var md5 = MD5.Create())
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }

    protected override long part1Work(string[] input) =>
        work(input, "00000");

    protected override long part2Work(string[] input) =>
        work(input, "000000");
}
