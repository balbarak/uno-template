namespace UnoAppTemplate.Demo.Views;


public sealed partial class FormPage : Page
{
    public FormViewModel ViewModel => DataContext as FormViewModel;
    
    public FormPage()
    {
        DataContext = App.GetService<FormViewModel>();

        this.InitializeComponent();
    }

}
