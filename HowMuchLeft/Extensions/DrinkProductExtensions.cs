using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using HowMuchLeft.Models;

namespace HowMuchLeft.Extensions;

public static class DrinkProductExtensions
{
    public static IEnumerable<Drink> LoadDrinksFromCsv(this Stream stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<DrinkMap>();
        var records = csv.GetRecords<Drink>();

        return records;
    }

    public static IEnumerable<Drink> CleanNames(this IEnumerable<Drink> drinks)
    {
        var result = new List<Drink>();

        foreach (var drink in drinks)
        {
            drink.Recipe.NormalizeProductNames();
            result.Add(drink);
        }

        return result;
    }
}