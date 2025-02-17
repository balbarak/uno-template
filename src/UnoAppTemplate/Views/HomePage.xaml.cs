namespace UnoAppTemplate.Views;


public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel => DataContext as HomeViewModel;

    public HomePage()
    {
        this.DataContext = App.GetService<HomeViewModel>();

        this.InitializeComponent();
    }
}
