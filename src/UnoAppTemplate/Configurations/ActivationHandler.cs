using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Services;
using UnoAppTemplate.Views;

namespace UnoAppTemplate;
public class ActivationHandler : IActivationHandler
{
    private readonly INavigationService _navService;

    public ActivationHandler(INavigationService navService)
    {
        _navService = navService;
    }

    public async Task Activate()
    {

        await _navService.Navigate(RouteService.TYPOGRAPHY_PAGE);
    }
}
