using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Demo.Views;
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
        services.AddTransient<ThemeViewModel>();
        
        services.AddPage<HomeViewModel, HomePage>(RouteService.HOME_PAGE);
        services.AddPage<TypographyViewModel, TypographyPage>(RouteService.TYPOGRAPHY_PAGE);
        services.AddPage<ButtonViewModel, ButtonsPage>(RouteService.BUTTONS_PAGE);
        services.AddPage<ThemeViewModel, ThemePage>(RouteService.THEME_PAGE);
        services.AddPage<SettingsViewModel, SettingsPage>(RouteService.SETTINGS_PAGE);
    }
}
