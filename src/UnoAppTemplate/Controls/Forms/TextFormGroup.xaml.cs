using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace UnoAppTemplate.Controls;

public sealed partial class TextFormGroup : UserControl
{
    private object _dataItem;
    private string _propertyName;
    private ValidationContext _validationContext;
    private bool _isFocused;
    private bool _isFirstTimeFocused = true;
    private string _preservedHelpText;

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(TextFormGroup), new PropertyMetadata(null));
    public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }


    public static readonly DependencyProperty ForProperty =
        DependencyProperty.Register(nameof(For), typeof(string), typeof(TextFormGroup), new PropertyMetadata(null));
    public string For { get => (string)GetValue(ForProperty); set => SetValue(ForProperty, value); }


    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(TextFormGroup), new PropertyMetadata(null));
    public string LabelText { get => (string)GetValue(LabelTextProperty); set => SetValue(LabelTextProperty, value); }


    public static readonly DependencyProperty HelpTextProperty =
        DependencyProperty.Register(nameof(HelpText), typeof(string), typeof(TextFormGroup), new PropertyMetadata(null));
    public string HelpText { get => (string)GetValue(HelpTextProperty); set => SetValue(HelpTextProperty, value); }


    public TextFormGroup()
    {
        this.DataContextChanged += OnDataContextChanged;
        this.InitializeComponent();
    }

    private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        SetAttributeValues();
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);

        _isFocused = true;

        UpdateVisualState();

        _isFirstTimeFocused = false;
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);

        _isFocused = false;

        UpdateVisualState();

    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateVisualState();
    }

    public bool Validate()
    {
        var result = true;

        if (_dataItem == null || string.IsNullOrWhiteSpace(_propertyName))
            return result;

        var errors = new List<ValidationResult>();

        Validator.TryValidateObject(_dataItem, _validationContext, errors, true);

        var error = errors
            .Where(a => a.MemberNames.Contains(_propertyName))
            .FirstOrDefault();

        if (error != null)
        {
            if (!_isFirstTimeFocused)
            {
                HelpText = error.ErrorMessage;
            }

            result = false;
        }
        else
        {
            HelpText = _preservedHelpText;
            result = true;
        }

        return result;
    }

    private void UpdateVisualState()
    {
        var isValid = Validate();

        if (_isFocused)
        {
            if (_isFirstTimeFocused)
            {
                VisualStateManager.GoToState(this, "Focused", true);
                return;
            }

            if (isValid)
            {
                VisualStateManager.GoToState(this, "Focused", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "ErrorFocused", true);
            }
        }
        else
        {
            if (isValid)
            {
                VisualStateManager.GoToState(this, "Normal", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Error", true);
            }
        }
    }

    private void SetAttributeValues()
    {
        var expression = GetBindingExpression(ForProperty);
        _dataItem = expression?.DataItem;
        _propertyName = expression?.ParentBinding?.Path?.Path;

        if (_dataItem == null || _propertyName == null)
            return;

        _validationContext = new ValidationContext(_dataItem);

        var property = _dataItem.GetType().GetProperty(_propertyName);

        var displayName = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

        var isRequired = (property.GetCustomAttribute(typeof(RequiredAttribute)) as RequiredAttribute) != null;

        if (displayName != null)
            LabelText = displayName.GetName() ?? _propertyName;



        if (!isRequired && string.IsNullOrWhiteSpace(HelpText))
            HelpText = _preservedHelpText = "(Optional)";
        else
            _preservedHelpText = HelpText;
    }
}
