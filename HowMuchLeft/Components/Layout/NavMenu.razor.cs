using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HowMuchLeft.Extensions;
using HowMuchLeft.Models;
using Microsoft.AspNetCore.Components.Routing;

namespace HowMuchLeft.Components.Layout;

public partial class NavMenu
{
    private string? _currentUrl;

    private const string _drinkRecipesPath = "wwwroot/Recipes/DrinkRecipes.csv";

    protected override async Task OnInitializedAsync()
    {
        _currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var existingDrinkRecipes =
                await ProtectedLocalStorage.GetAsync<IEnumerable<DrinkRecipe>>(DrinkRecipe.BrowserStorageKey);
            if (existingDrinkRecipes.Success)
            {
                await base.OnAfterRenderAsync(firstRender);
                return;
            }

            var drinkRecipes = _drinkRecipesPath.LoadRecipesFromCsv().CleanNames().ToList();
            await ProtectedLocalStorage.SetAsync(DrinkRecipe.BrowserStorageKey, drinkRecipes);
            await base.OnAfterRenderAsync(firstRender);
        }
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        _currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
        StateHasChanged();
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}