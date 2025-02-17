using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Services;

namespace UnoAppTemplate.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private readonly INavigationService _navService;
    public ICommand GoToSecondPage { get; }

    public HomeViewModel(INavigationService navService)
    {
        GoToSecondPage = new AsyncRelayCommand(OnGoToSecondPage);
        _navService = navService;
    }

    private async Task OnGoToSecondPage()
    {
        await _navService.Navigate(RouteService.TEST_PAGE);
    }
}
