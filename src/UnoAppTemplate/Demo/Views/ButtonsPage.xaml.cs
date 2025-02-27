namespace UnoAppTemplate.Views;

public sealed partial class ButtonsPage : Page
{
    public ButtonViewModel ViewModel => DataContext as ButtonViewModel;

    public ButtonsPage()
    {
        this.InitializeComponent();
        DataContext = App.GetService<ButtonViewModel>();
    }
}
