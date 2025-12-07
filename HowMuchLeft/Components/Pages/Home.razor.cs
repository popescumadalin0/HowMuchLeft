using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HowMuchLeft.Extensions;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace HowMuchLeft.Components.Pages;

public partial class Home
{
    private async Task UploadFilesAsync(IBrowserFile? file)
    {
        if (file is null)
        {
            return;
        }

        var drinks = file.OpenReadStream().LoadDrinksFromCsv().CleanNames().ToList();

        Snackbar.Add("File uploaded successfully", MudBlazor.Severity.Success);
    }
}