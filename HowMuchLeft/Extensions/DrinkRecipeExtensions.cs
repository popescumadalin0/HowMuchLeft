using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper;
using HowMuchLeft.Models;

namespace HowMuchLeft.Extensions;

public static partial class DrinkRecipeExtensions
{
    public static List<DrinkRecipe> LoadRecipesFromCsv(this string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"CSV file not found: {path}");

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<DrinkRecipeMap>();

        var records = csv.GetRecords<DrinkRecipe>();

        return records.ToList();
    }

    public static List<DrinkRecipe> CleanNames(this List<DrinkRecipe> drinkRecipes)
    {
        var result = new List<DrinkRecipe>();
        foreach (var drinkRecipe in drinkRecipes)
        {
            drinkRecipe.ProductName = drinkRecipe.ProductName.NormalizeProductNames();
            result.Add(drinkRecipe);
        }

        return result;
    }

    public static string NormalizeProductNames(this string productName)
    {
        var name = productName.ToLowerInvariant();

        name = LowerAlphabetRegex().Replace(name, "");
        name = RemoveConsecutiveDuplicates(name);
        return name;
    }

    private static string RemoveConsecutiveDuplicates(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var sb = new StringBuilder();
        var last = '\0';

        foreach (var c in input)
        {
            if (c != last)
                sb.Append(c);

            last = c;
        }

        return sb.ToString();
    }

    [GeneratedRegex("[^a-z]")]
    private static partial Regex LowerAlphabetRegex();
}