
namespace UnoAppTemplate.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel => DataContext as SettingsViewModel;
    public SettingsPage()
    {
        this.InitializeComponent();

        DataContext = App.GetService<SettingsViewModel>();

        SetCurrentLangauge();
    }

    private void OnSelectLangaugeChanged(object sender, RoutedEventArgs e)
    {
        var control = sender as RadioButton;

        if (control == null)
            return;

        var selectedLanguage = Enum.Parse<AppLanguage>(control.Name);

        ViewModel.SelectedLanguage = selectedLanguage;
    }

    private void SetCurrentLangauge()
    {
        var current = ViewModel.Langauge.CurrentLanguage;

        switch (current)
        {
            case AppLanguage.English:

                English.IsChecked = true;
                Arabic.IsChecked = false;

                break;
            case AppLanguage.Arabic:
                
                Arabic.IsChecked = true;
                English.IsChecked = false;

                break;
            default:
                break;
        }
    }

    private void OnLanguageFlyoutOpening(object sender, object e)
    {
        SetCurrentLangauge();
    }
}
