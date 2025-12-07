using HowMuchLeft.Extensions;
using HowMuchLeft.Models;
using MudBlazor;

namespace HowMuchLeft.Components.Layout;

public partial class MainLayout
{
    private bool _drawerOpen = false;

    private const string _drinkRecipesPath = "wwwroot/Recipes/DrinkRecipes.csv";

    private MudTheme _currentTheme = new MudTheme();

    private readonly MudTheme _defaultTheme = new() { PaletteLight = new PaletteLight() { Black = "#272c34" } };

    private readonly MudTheme _darkTheme =
        new()
        {
            PaletteDark = new PaletteDark()
            {
                Black = "#27272f",
                Background = "#32333d",
                BackgroundGray = "#27272f",
                Surface = "#373740",
                DrawerBackground = "#27272f",
                DrawerText = "rgba(255,255,255, 0.50)",
                AppbarBackground = "#27272f",
                AppbarText = "rgba(255,255,255, 0.70)",
                TextPrimary = "rgba(255,255,255, 0.70)",
                TextSecondary = "rgba(255,255,255, 0.50)",
                ActionDefault = "#adadb1",
                ActionDisabled = "rgba(255,255,255, 0.26)",
                ActionDisabledBackground = "rgba(255,255,255, 0.12)",
                DrawerIcon = "rgba(255,255,255, 0.50)"
            }
        };

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    protected override void OnInitialized()
    {
        _currentTheme = _defaultTheme;
    }

    private void DarkMode()
    {
        if (_currentTheme == _defaultTheme)
        {
            _currentTheme = _darkTheme;
        }
        else
        {
            _currentTheme = _defaultTheme;
        }
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
}