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
    private readonly ThemeManager _themeManager;
    private bool _isDarkSelected;
    
    private AppLanguage _selectedLangauge;
    public bool IsDarkSelected { get => _isDarkSelected; set => SetValue(ref _isDarkSelected, value); }

    public LanguageManager Langauge => _languageManager;

    public ThemeManager Theme => _themeManager;

    public AppLanguage SelectedLanguage { get => _selectedLangauge; set => SetValue(ref _selectedLangauge, value); }

    public ICommand ChangeLangaugeCommand { get; }

    public ICommand ChangeThemeCommand { get; }

    public SettingsViewModel(LanguageManager languageManager, INavigationService navService, ThemeManager themeManager)
    {
        _languageManager = languageManager;
        _navService = navService;
        _themeManager = themeManager;

        IsDarkSelected = _themeManager.IsDark;
        ChangeThemeCommand = new AsyncRelayCommand(ChangeTheme);
        ChangeLangaugeCommand = new AsyncRelayCommand(ChangeLanguage);
    }

    public async Task ChangeLanguage()
    {
        await _languageManager.ChangeLangauge(SelectedLanguage);

        await _navService.Navigate(RouteService.LOADING_PAGE);

        await Task.Delay(500);

        App.ReloadShell();

        await _navService.Navigate(RouteService.SETTINGS_PAGE);
    }

    public async Task ChangeTheme()
    {
        await _themeManager.SwitchTheme(IsDarkSelected);
    }

}
