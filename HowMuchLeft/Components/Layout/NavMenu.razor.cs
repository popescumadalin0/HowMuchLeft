using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace HowMuchLeft.Components.Layout;

public partial class NavMenu
{
    private string? _currentUrl;

    protected override async Task OnInitializedAsync()
    {
        _currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var drinkRecipes = DrinkRecipe.LoadFromCsv("wwwroot/DrinkRecipes.csv");
            await ProtectedLocalStorage.SetAsync("DrinkRecipes", drinkRecipes);
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