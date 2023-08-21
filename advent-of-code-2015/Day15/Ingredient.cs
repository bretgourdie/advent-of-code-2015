namespace advent_of_code_2015.Day15;
internal class Ingredient
{
    public readonly string Name;
    public readonly long Capacity;
    public readonly long Durability;
    public readonly long Flavor;
    public readonly long Texture;
    public readonly long Calories;

    public Ingredient(string line)
    {
        var split = line.Split(": ");
        Name = split.First();

        var components = split[1].Split(' ');
        Capacity = parse(components[1]);
        Durability = parse(components[3]);
        Flavor = parse(components[5]);
        Texture = parse(components[7]);
        Calories = parse(components[9]);
    }

    private long parse(string component)
    {
        return long.Parse(component.Replace(",", String.Empty));
    }

    public override string ToString() => Name;
}
