using System.Threading.Tasks.Dataflow;

namespace advent_of_code_2015.Day15;
internal class Day15 : AdventSolution
{
    private const long ingredient_limit = 100;

    protected override long part1ExampleExpected => 62842880;
    protected override long part1InputExpected => -1;
    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }

    private long work(string[] input)
    {
        var recipe = new Dictionary<Ingredient, long>();

        foreach (var line in input)
        {
            var ingredient = new Ingredient(line);
            recipe.Add(ingredient, 0);
        }

        return getBestRecipe(
            recipe,
            new Dictionary<string, long>(),
            long.MinValue);
    }

    private long getBestRecipe(
        IDictionary<Ingredient, long> recipe,
        IDictionary<string, long> knownRecipes,
        long bestScore)
    {
        var capacity = recipe.Sum(x => x.Value);

        if (capacity == ingredient_limit)
        {
            var score = scoreRecipe(recipe);
            knownRecipes[getRecipeString(recipe)] = score;
            return score;
        }

        foreach (var ingredient in recipe.Keys)
        {
            recipe[ingredient] += 1;

            var recipeString = getRecipeString(recipe);

            if (knownRecipes.ContainsKey(recipeString))
            {
                bestScore = knownRecipes[recipeString];
            }

            else
            {
                var subBestScore = getBestRecipe(recipe, knownRecipes, bestScore);
                knownRecipes[recipeString] = subBestScore;
                bestScore = Math.Max(bestScore, subBestScore);
            }

            recipe[ingredient] -= 1;
        }

        return bestScore;
    }

    private string getRecipeString(IDictionary<Ingredient, long> recipe)
    {
        return
            String.Join(",",
                recipe
                    .OrderBy(x => x.Key.ToString())
                    .Select(
                    x => $"{x.Key}:{x.Value}"
                )
            );
    }

    private long scoreRecipe(
        IDictionary<Ingredient, long> recipe)
    {
        var score =
            recipe.Aggregate((long)1, (acc, ingredientToAmount) =>
                acc * 
                (ingredientToAmount.Key.Capacity * ingredientToAmount.Value
                + ingredientToAmount.Key.Durability * ingredientToAmount.Value
                + ingredientToAmount.Key.Flavor * ingredientToAmount.Value
                + ingredientToAmount.Key.Texture * ingredientToAmount.Value)
            );

        return Math.Max(0, score);
    }

    protected override long part1Work(string[] input) =>
        work(input);
    
    protected override long part2Work(string[] input) =>
        work(input);
}
