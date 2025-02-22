using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Services;

namespace UnoAppTemplate.ViewModels;
public class ThemeViewModel : BaseViewModel
{
    private readonly INavigationService _navService;
    public ICommand ShowToasterCommand { get; }

    public ThemeViewModel(INavigationService navService)
    {
        ShowToasterCommand = new AsyncRelayCommand(OnShowToasterCommand,AsyncRelayCommandOptions.AllowConcurrentExecutions);
        _navService = navService;
    }

    private async Task OnShowToasterCommand()
    {
        await _navService.ShowToaster("Hello, this is a toaster for display quick message");
    }
}
