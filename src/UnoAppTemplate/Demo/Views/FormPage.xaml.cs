namespace UnoAppTemplate.Demo.Views;


public sealed partial class FormPage : Page
{
    public FormViewModel ViewModel => DataContext as FormViewModel;
    
    public FormPage()
    {
        DataContext = App.GetService<FormViewModel>();

        this.InitializeComponent();
    }

    private void OnErrorStateClicked(object sender, RoutedEventArgs e)
    {
        VisualStateManager.GoToState(PART_InputOne, "Error", true);
    }

    private void OnNomralStateClicked(object sender, RoutedEventArgs e)
    {
        VisualStateManager.GoToState(PART_InputOne, "Normal", true);
    }
}
