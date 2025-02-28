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
    private readonly ThemeManager _themeManager;
    private readonly LanguageManager _langManager;

    public ActivationHandler(INavigationService navService, ThemeManager themeManager, LanguageManager langManager)
    {
        _navService = navService;
        _themeManager = themeManager;
        _langManager = langManager;
    }

    public async Task Activate()
    {
        await _themeManager.SwitchTheme(true);

        await _langManager.ChangeLangauge(AppLanguage.Arabic);

        await _navService.Navigate(RouteService.FORM_PAGE);
    }
}
