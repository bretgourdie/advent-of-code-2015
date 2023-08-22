namespace advent_of_code_2015.Day15;
internal class Ingredient
{
    public readonly string Name;
    public readonly IDictionary<string, long> Components;

    public Ingredient(string line)
    {
        var split = line.Split(": ");
        Name = split.First();

        Components = new Dictionary<string, long>();

        var splitComponents = split[1].Split(", ");

        foreach (var splitComponent in splitComponents)
        {
            var componentAndValue = splitComponent.Split(' ');
            Components[componentAndValue[0]] = long.Parse(componentAndValue[1]);
        }
    }

    public override string ToString() => Name;
}
