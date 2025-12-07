using CsvHelper.Configuration;

namespace HowMuchLeft.Models;

public class DrinkRecipe
{
    public string ProductName { get; set; }

    public double VolumeMilliliters { get; set; }

    public double Coffee { get; set; }

    public double Milk { get; set; }

    public double Choco { get; set; }

    public double Tea { get; set; }

    public double Water { get; set; }

    public const string BrowserStorageKey = "DrinkRecipes";

    public override string ToString()
    {
        return
            $"{ProductName} | Volume: {VolumeMilliliters} ml | Coffee: {Coffee} | Milk: {Milk} | Choco: {Choco} | Tea: {Tea} | Water: {Water}";
    }
}

public sealed class DrinkRecipeMap : ClassMap<DrinkRecipe>
{
    public DrinkRecipeMap()
    {
        Map(d => d.ProductName).Name("ProductName");
        Map(d => d.VolumeMilliliters)
            .Name("VolumeMilliliters")
            .Optional()
            .Default(0)
            .TypeConverterOption.NullValues(string.Empty);
        Map(d => d.Coffee).Name("Coffee").Optional().Default(0).TypeConverterOption.NullValues(string.Empty);
        Map(d => d.Milk).Name("Milk").Optional().Default(0).TypeConverterOption.NullValues(string.Empty);
        Map(d => d.Choco).Name("Choco").Optional().Default(0).TypeConverterOption.NullValues(string.Empty);
        Map(d => d.Tea).Name("Tea").Optional().Default(0).TypeConverterOption.NullValues(string.Empty);
        Map(d => d.Water).Name("Water").Optional().Default(0).TypeConverterOption.NullValues(string.Empty);
    }
}