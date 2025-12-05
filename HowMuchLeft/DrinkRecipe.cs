using System.Globalization;

namespace HowMuchLeft;

public class DrinkRecipe
{
    public string ProductName { get; set; }
    public double VolumeMilliliters { get; set; }
    public double Coffee { get; set; }
    public double Milk { get; set; }
    public double Choco { get; set; }
    public double Tea { get; set; }
    public double Water { get; set; }

    public override string ToString()
    {
        return
            $"{ProductName} | Volume: {VolumeMilliliters} ml | Coffee: {Coffee} | Milk: {Milk} | Choco: {Choco} | Tea: {Tea} | Water: {Water}";
    }

    public static List<DrinkRecipe> LoadFromCsv(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"CSV file not found: {path}");

        var lines = File.ReadAllLines(path);
        var result = new List<DrinkRecipe>();

        if (lines.Length < 2) return result;

        // Parse header
        var header = lines[0].Split(',');
        if (header.Length < 7)
            throw new InvalidDataException(
                "Expected header: ProductName,VolumeMilliliters,Coffee,Milk,Choco,Tea,Water");

        for (int i = 1; i < lines.Length; i++)
        {
            var row = lines[i].Split(',');
            if (row.Length < 7) continue; // skip malformed rows

            string name = row[0].Trim();
            if (string.IsNullOrEmpty(name)) continue;

            var recipe = new DrinkRecipe
            {
                ProductName = name,
                VolumeMilliliters = ParseDouble(row[1]),
                Coffee = ParseDouble(row[2]),
                Milk = ParseDouble(row[3]),
                Choco = ParseDouble(row[4]),
                Tea = ParseDouble(row[5]),
                Water = ParseDouble(row[6])
            };

            result.Add(recipe);
        }

        return result;
    }

    private static double ParseDouble(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return 0;
        if (double.TryParse(input.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double val))
            return val;
        return 0;
    }
}