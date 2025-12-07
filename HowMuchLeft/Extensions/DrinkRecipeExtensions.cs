using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        return name;
    }

    [GeneratedRegex("[^a-z]")]
    private static partial Regex LowerAlphabetRegex();
}