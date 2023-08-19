using System.Text.Json;

namespace advent_of_code_2015.Day12;
internal class Day12 : AdventSolution
{
    protected override long part1ExampleExpected => 6 + 6 + 15 + 6;
    protected override long part1InputExpected => 191164;

    protected override long part2ExampleExpected => 6 + 4 + 0 + 6;
    protected override long part2InputExpected => 87842;

    private long work(
        string[] input,
        Func<JsonProperty, bool> addStrategy)
    {
        var line = input.Single();

        var jDocument = JsonDocument.Parse(line);

        return getSum(jDocument.RootElement, addStrategy);
    }

    private long getSum(
        JsonElement jsonElement,
        Func<JsonProperty, bool> shouldAdd)
    {
        long sum = 0;

        switch (jsonElement.ValueKind)
        {
            case JsonValueKind.Array:
                foreach (var child in jsonElement.EnumerateArray())
                {
                    if (child.ValueKind == JsonValueKind.Number)
                    {
                        var value = child.GetInt64();
                        sum += value;
                    }

                    else
                    {
                        var innerSum = getSum(child, shouldAdd);
                        sum += innerSum;
                    }
                }
                break;

            case JsonValueKind.Object:
                var children = jsonElement.EnumerateObject();
                if (children.All(subElement => shouldAdd(subElement)))
                {
                    foreach (var child in children)
                    {
                        var childValue = child.Value;
                        sum += getSum(childValue, shouldAdd);
                    }
                }
                break;

            case JsonValueKind.Number:
                sum += jsonElement.GetInt64();
                break;
        }

        return sum;
    }

    private bool addEverything(JsonProperty _) => true;

    private bool addIfNotRed(JsonProperty jsonProperty)
    {
        var jsonValueElement = jsonProperty.Value;

        var isRed = false;

        if (jsonValueElement.ValueKind == JsonValueKind.String)
        {
            var jsonValue = jsonValueElement.GetString();
            isRed = jsonValue == "red";
        }

        return !isRed;
    }

    protected override long part1Work(string[] input) =>
        work(input, addEverything);

    protected override long part2Work(string[] input) =>
        work(input, addIfNotRed);
}
