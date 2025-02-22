namespace UnoAppTemplate.Demo.Views;

public sealed partial class DesignPage : Page
{
    public DesignPage()
    {
        this.InitializeComponent();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        await PART_Toaster.ShowToaster("Welcome aboard dude Hello !");
    }

}
