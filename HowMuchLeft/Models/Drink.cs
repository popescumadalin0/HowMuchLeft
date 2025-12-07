namespace HowMuchLeft.Models;

public class Drink
{
    public string Recipe { get; set; }

    public override string ToString()
    {
        return
            $"{Recipe}";
    }
}