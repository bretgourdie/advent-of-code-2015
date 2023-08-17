using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace advent_of_code_2015.Day12;
internal class Day12 : AdventSolution
{
    protected override long part1ExampleExpected => 6 + 6 + 15 + 6;
    protected override long part1InputExpected => 191164;

    protected override long part2ExampleExpected => 6 + 4 + 0 + 6;
    protected override long part2InputExpected => -1;

    private long work(string[] input, Func<JToken, bool> addStrategy)
    {
        var line = input.Single();

        var jArray = JArray.Parse(line);

        return getSum(jArray, addStrategy);
    }

    private long getSum(
        JToken jToken,
        Func<JToken, bool> addStrategy)
    {
        long sum = 0;

        switch (jToken.Type)
        {
            case JTokenType.Array:
                foreach (var element in jToken)
                {
                    sum += getSum(element, addStrategy);
                }

                break;
            case JTokenType.Integer:
                sum += jToken.Value<long>();
                break;

            case JTokenType.Object:
                if (jToken.All(element => addStrategy(element)))
                {
                    foreach (var element in jToken)
                    {
                        sum += getSum(element, addStrategy);
                    }
                }

                break;

            case JTokenType.Property:
                foreach (var value in jToken.Values())
                {
                    sum += getSum(value, addStrategy);
                }

                break;
        }

        return sum;
    }

    private bool addEverything(JToken _) => true;

    private bool ignoreReds(JToken token)
    {
        foreach (var element in token)
        {
            if (element.Type == JTokenType.String)
            {
                if (element.Value<string>() == "red")
                {
                    return false;
                }
            }
        }

        return true;
    }

    protected override long part1Work(string[] input) =>
        work(input, addEverything);

    protected override long part2Work(string[] input) =>
        work(input, ignoreReds);
}
