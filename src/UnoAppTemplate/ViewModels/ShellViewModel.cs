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
    private readonly LanguageManager _language;

    public LanguageManager Language => _language;

    public ICommand GoToPage { get; }

    public ShellViewModel(INavigationService navService, LanguageManager language)
    {
        _navService = navService;

        GoToPage = new AsyncRelayCommand<string>(OnGotoPage);
        _language = language;
    }

    private async Task OnGotoPage(string route)
    {
        App.ContentHost.HideMenu();

        await _navService.Navigate(route);
    }
}
