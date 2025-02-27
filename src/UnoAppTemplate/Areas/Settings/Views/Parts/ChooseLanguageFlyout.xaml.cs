
using UnoAppTemplate.Resx;

namespace UnoAppTemplate.Views;


public sealed partial class ChooseLanguageFlyout : Flyout
{
    private LanguageManager _langManager;

    public static readonly DependencyProperty ChangeLanguageCommandProperty =
        DependencyProperty.Register(nameof(ChangeLanguageCommand), typeof(ICommand), typeof(ChooseLanguageFlyout), new PropertyMetadata(null));
    
    public ICommand ChangeLanguageCommand { get => (ICommand)GetValue(ChangeLanguageCommandProperty); set => SetValue(ChangeLanguageCommandProperty, value); }


    public static readonly DependencyProperty SelectedLanguageProperty =
        DependencyProperty.Register(nameof(SelectedLanguage), typeof(AppLanguage), typeof(ChooseLanguageFlyout), new PropertyMetadata(null));
    public AppLanguage SelectedLanguage { get => (AppLanguage)GetValue(SelectedLanguageProperty); set => SetValue(SelectedLanguageProperty, value); }


    public static readonly DependencyProperty ApplyChangeTextProperty =
        DependencyProperty.Register(nameof(ApplyChangeText), typeof(string), typeof(ChooseLanguageFlyout), new PropertyMetadata(null));
    public string ApplyChangeText { get => (string)GetValue(ApplyChangeTextProperty); set => SetValue(ApplyChangeTextProperty, value); }


    public ChooseLanguageFlyout()
    {
        _langManager = App.GetService<LanguageManager>();

        ApplyChangeText = CommonText.ApplyChanges;
        this.InitializeComponent();
        DataContext = this;
    }

    private void OnArabicSelected(object sender, RoutedEventArgs e)
    {
        SelectedLanguage = AppLanguage.Arabic;
    }

    private void OnEnglishChecked(object sender, RoutedEventArgs e)
    {
        SelectedLanguage = AppLanguage.English;
    }

    private void OnFlyoutOpening(object sender, object e)
    {
        if (_langManager.CurrentLanguage == AppLanguage.Arabic)
        {
            Arabic.IsChecked = true;
            English.IsChecked = false;
        }
        else
        {
            English.IsChecked = true;
            Arabic.IsChecked = false;
        }
    }
}
