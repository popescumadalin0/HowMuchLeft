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