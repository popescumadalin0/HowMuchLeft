using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using HowMuchLeft.Models;

namespace HowMuchLeft.Extensions;

public static class DrinkProductExtensions
{
    public static List<Drink> LoadDrinksFromCsv(this Stream stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<DrinkMap>();
        var records = csv.GetRecords<Drink>();

        return records.ToList();
    }

    public static List<Drink> CleanNames(this List<Drink> drinks)
    {
        var result = new List<Drink>();

        foreach (var drink in drinks)
        {
            drink.Recipe = drink.Recipe.NormalizeProductNames();
            result.Add(drink);
        }

        return result;
    }
}