
namespace UnoAppTemplate.Controls;

public sealed partial class TextFormGroup : UserControl
{
    public TextFormGroup()
    {
        this.InitializeComponent();
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);

        VisualStateManager.GoToState(this, "Focused", true);
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);

        VisualStateManager.GoToState(this, "Normal", true);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {

    }
}
