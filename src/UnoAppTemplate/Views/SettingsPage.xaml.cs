
namespace UnoAppTemplate.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel => DataContext as SettingsViewModel;
    public SettingsPage()
    {
        this.InitializeComponent();

        DataContext = App.GetService<SettingsViewModel>();
    }
}
