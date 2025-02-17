using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Services;
using UnoAppTemplate.ViewModels;
using UnoAppTemplate.Views;

namespace UnoAppTemplate;
public class Startup
{
    public static void ConfigureServices(HostBuilderContext context,IServiceCollection services)
    {
        services.AddSingleton<IActivationHandler, ActivationHandler>();

        services.AddSingleton<INavigationService, NavigationService>();

        services.AddTransient<ShellViewModel>();
        services.AddTransient<HomeViewModel>();

        RouteService.RegisterRoute(RouteService.HOME_PAGE, typeof(HomePage));
        RouteService.RegisterRoute(RouteService.TEST_PAGE, typeof(TestPage));
        RouteService.RegisterRoute(RouteService.BUTTONS_PAGE, typeof(ButtonsPage));
    }
}
