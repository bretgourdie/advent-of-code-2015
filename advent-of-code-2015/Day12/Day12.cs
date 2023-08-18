using Newtonsoft.Json.Linq;

namespace advent_of_code_2015.Day12;
internal class Day12 : AdventSolution
{
    protected override long part1ExampleExpected => 6 + 6 + 15 + 6;
    protected override long part1InputExpected => 191164;

    protected override long part2ExampleExpected => 6 + 4 + 0 + 6;
    protected override long part2InputExpected => 87842;

    private long work(string[] input, Func<JObject, bool> addStrategy)
    {
        var line = input.Single();

        var jArray = JArray.Parse(line);

        return getSum(jArray, addStrategy);
    }

    private long getSum(
        JToken jToken,
        Func<JObject, bool> shouldAdd)
    {
        long sum = 0;

        switch (jToken.Type)
        {
            case JTokenType.Array:
                foreach (var element in jToken)
                {
                    sum += getSum(element, shouldAdd);
                }
                break;

            case JTokenType.Integer:
                sum += jToken.Value<long>();
                break;

            case JTokenType.Object:
                if (jToken is JObject jObject)
                {
                    if (shouldAdd(jObject))
                    {
                        foreach (var element in jToken)
                        {
                            sum += getSum(element, shouldAdd);
                        }
                    }
                }
                break;

            case JTokenType.Property:
                if (jToken is JProperty jProperty)
                {
                    sum += getSum(jProperty.Value, shouldAdd);
                }
                break;
        }

        return sum;
    }

    private bool addEverything(JToken _) => true;

    private bool ignoreReds(JObject jObject)
    {
        foreach (var element in jObject)
        {
            var valueToken = element.Value;
            if (valueToken?.Type == JTokenType.String)
            {
                var value = valueToken.Value<string>();

                if (value == "red")
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
