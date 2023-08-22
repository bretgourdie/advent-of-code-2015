namespace advent_of_code_2015.Day15;
internal class Day15 : AdventSolution
{
    private const long ingredient_limit = 100;

    protected override long part1ExampleExpected => 62842880;
    protected override long part1InputExpected => 18965440;
    protected override long part2ExampleExpected => 57600000;
    protected override long part2InputExpected => 15862900;

    private long work(
        string[] input,
        Func<IDictionary<Ingredient, long>, bool> canMake)
    {
        var recipe = new Dictionary<Ingredient, long>();

        foreach (var line in input)
        {
            var ingredient = new Ingredient(line);
            recipe.Add(ingredient, 0);
        }

        return getBestRecipe(
            recipe,
            new HashSet<string>(),
            long.MinValue,
            canMake);
    }

    private long getBestRecipe(
        IDictionary<Ingredient, long> recipe,
        ISet<string> knownRecipes,
        long bestScore,
        Func<IDictionary<Ingredient, long>, bool> canMake)
    {
        var capacity = recipe.Sum(x => x.Value);

        if (capacity == ingredient_limit)
        {
            var score = scoreRecipe(recipe, canMake);
            knownRecipes.Add(getRecipeString(recipe));
            return score;
        }

        foreach (var ingredient in recipe.Keys)
        {
            recipe[ingredient] += 1;

            var recipeString = getRecipeString(recipe);

            if (!knownRecipes.Contains(recipeString))
            {
                knownRecipes.Add(recipeString);
                bestScore = Math.Max(
                    bestScore,
                    getBestRecipe(recipe, knownRecipes, bestScore, canMake));
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
        IDictionary<Ingredient, long> recipe,
        Func<IDictionary<Ingredient, long>, bool> canMake)
    {
        if (!canMake(recipe))
        {
            return long.MinValue;
        }

        var components = recipe.Keys.First().Components.Keys;

        long score = 1;

        foreach (var component in components.Where(x => x != "calories"))
        {
            if (score == 0) break;

            var ingredients = recipe.Keys.ToArray();

            var componentScores = new long[ingredients.Length];

            for (int ii = 0; ii < ingredients.Length; ii++)
            {
                var ingredient = ingredients[ii];

                var amount = recipe[ingredient];
                var propertyValue = ingredient.Components[component];

                componentScores[ii] = amount * propertyValue;
            }

            var subScore = componentScores.Sum();

            score *= Math.Max(subScore, 0);
        }

        return score;
    }

    private bool makeEverything(IDictionary<Ingredient, long> recipe) => true;

    private bool mustBe500Calories(IDictionary<Ingredient, long> recipe)
    {
        long calorieScore = 0;

        foreach (var ingredientAndAmount in recipe)
        {
            var ingredient = ingredientAndAmount.Key;
            var amount = ingredientAndAmount.Value;

            calorieScore += amount * ingredient.Components["calories"];
        }

        return calorieScore == 500;
    }

    protected override long part1Work(string[] input) =>
        work(input, makeEverything);
    
    protected override long part2Work(string[] input) =>
        work(input, mustBe500Calories);
}
