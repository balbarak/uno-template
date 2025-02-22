namespace UnoAppTemplate.Demo.Views;

public sealed partial class ThemePage : Page
{
    public ThemeViewModel ViewModel => DataContext as ThemeViewModel;
    public ThemePage()
    {
        this.InitializeComponent();

        DataContext = App.GetService<ThemeViewModel>();
    }
}
