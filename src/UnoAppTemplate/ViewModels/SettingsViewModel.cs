using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Services;

namespace UnoAppTemplate.ViewModels;
public class SettingsViewModel : BaseViewModel
{
    private readonly LanguageManager _languageManager;
    private readonly INavigationService _navService;
    private AppLanguage _selectedLangauge;

    public LanguageManager Langauge => _languageManager;

    public AppLanguage SelectedLanguage { get => _selectedLangauge; set => SetValue(ref _selectedLangauge, value); }

    public ICommand ChangeLangaugeCommand { get; }

    public SettingsViewModel(LanguageManager languageManager, INavigationService navService)
    {
        _languageManager = languageManager;
        _navService = navService;

        ChangeLangaugeCommand = new AsyncRelayCommand(ChangeLanguage);
    }

    private async Task ChangeLanguage()
    {
        await _languageManager.ChangeLangauge(SelectedLanguage);

        await _navService.Navigate(RouteService.LOADING_PAGE);

        await Task.Delay(500);

        App.ReloadShell();

        await _navService.Navigate(RouteService.SETTINGS_PAGE);

    }
}
