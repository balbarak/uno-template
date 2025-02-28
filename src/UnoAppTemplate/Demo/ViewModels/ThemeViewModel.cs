using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Controls;
using UnoAppTemplate.Resx;
using UnoAppTemplate.Services;

namespace UnoAppTemplate.ViewModels;
public class ThemeViewModel : BaseViewModel
{
    private readonly INavigationService _navService;
    public ICommand ShowToasterCommand { get; }

    public ICommand ShowSuccessAlertCommand { get; }
    public ICommand ShowErrorAlertCommand { get; }

    public ICommand GoToFormsCommand { get; }


    public ThemeViewModel(INavigationService navService)
    {
        Title = CommonText.Theme;
        ShowToasterCommand = new AsyncRelayCommand(OnShowToasterCommand,AsyncRelayCommandOptions.AllowConcurrentExecutions);
        ShowSuccessAlertCommand = new AsyncRelayCommand(ShowSuccessAlert);
        ShowErrorAlertCommand = new AsyncRelayCommand(ShowErrorAlert);

        GoToFormsCommand = new AsyncRelayCommand(GoToForms);
        _navService = navService;
    }


    private async Task OnShowToasterCommand()
    {
        await _navService.ShowToaster("Hello, this is a toaster for display quick message");
    }

    private async Task ShowSuccessAlert()
    {
        await _navService.ShowAlert("Success", "This is a success alert example", AlertType.Success);
    }

    private async Task ShowErrorAlert()
    {
        await _navService.ShowAlert("Error", "This is a error alert example", AlertType.Error);
    }

    private async Task GoToForms()
    {
        await _navService.Navigate(RouteService.FORM_PAGE);
    }

}
