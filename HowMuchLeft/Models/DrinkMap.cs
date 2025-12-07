using CsvHelper.Configuration;

namespace HowMuchLeft.Models;

public sealed class DrinkMap : ClassMap<Drink>
{
    public DrinkMap()
    {
        Map(d => d.Recipe).Name("Recipe");
    }
}