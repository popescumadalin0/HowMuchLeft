using HowMuchLeft.Extensions;
using HowMuchLeft.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace HowMuchLeft.Components.Pages;

public partial class Home
{
    [Inject]
    public ProtectedLocalStorage ProtectedLocalStorage { get; set; }

    private List<DrinkRecipe> DrinkRecipes { get; set; } = new();

    private double TotalCoffee { get; set; }

    private double TotalMilk { get; set; }

    private double TotalChoco { get; set; }

    private double TotalTea { get; set; }

    private double TotalWater { get; set; }

    private string FileUploaded { get; set; } = "File not uploaded";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var drinkRecipes = await ProtectedLocalStorage.GetAsync<List<DrinkRecipe>>(DrinkRecipe.BrowserStorageKey);
            if (drinkRecipes.Success)
            {
                DrinkRecipes = drinkRecipes.Value!;
            }
            else
            {
                Snackbar.Add("No drink recipes found", MudBlazor.Severity.Error);
                throw new Exception("No drink recipes found");
            }
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task UploadFilesAsync(IBrowserFile? file)
    {
        if (file is null)
        {
            return;
        }

        TotalCoffee = 0;
        TotalMilk = 0;
        TotalChoco = 0;
        TotalTea = 0;
        TotalWater = 0;

        using var memoryStream = new MemoryStream();

        await file.OpenReadStream()
            .CopyToAsync(memoryStream);
        memoryStream.Seek(
            0,
            SeekOrigin.Begin);

        FileUploaded = "File uploaded: " + file.Name;
        Snackbar.Add("File uploaded successfully", MudBlazor.Severity.Success);

        var drinks = memoryStream.LoadDrinksFromCsv().CleanNames().ToList();

        foreach (var drink in drinks)
        {
            var drinkRecipe = DrinkRecipes.FirstOrDefault(d => drink.Recipe.Contains(d.ProductName));
            if (drinkRecipe is null)
            {
                Snackbar.Add($"No recipe found for {drink.Recipe}", MudBlazor.Severity.Error);
                continue;
            }

            TotalCoffee += drinkRecipe.Coffee;
            TotalMilk += drinkRecipe.Milk;
            TotalChoco += drinkRecipe.Choco;
            TotalTea += drinkRecipe.Tea;
            TotalWater += drinkRecipe.Water;
        }

        Snackbar.Add("Drink recipes loaded successfully", MudBlazor.Severity.Success);
    }
}