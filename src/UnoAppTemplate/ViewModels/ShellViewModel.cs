using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Services;

namespace UnoAppTemplate.ViewModels;

public class ShellViewModel : BaseViewModel
{
    private readonly INavigationService _navService;

    public ICommand GoToPage { get; }

    public ShellViewModel(INavigationService navService)
    {
        _navService = navService;

        GoToPage = new AsyncRelayCommand<string>(OnGotoPage);
    }

    private async Task OnGotoPage(string route)
    {
        App.ContentHost.HideMenu();

        await _navService.Navigate(route);
    }
}
