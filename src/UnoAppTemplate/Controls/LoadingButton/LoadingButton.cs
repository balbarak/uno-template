using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate.Controls;
public partial class LoadingButton : Button
{
    public static readonly DependencyProperty IsLoadingProperty =
       DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(LoadingButton), new PropertyMetadata(null, OnIsLoadingPropertyChanged));
    public bool IsLoading { get => (bool)GetValue(IsLoadingProperty); set => SetValue(IsLoadingProperty, value); }


    public LoadingButton()
    {
        DefaultStyleKey = typeof(LoadingButton);
    }

    private static void OnIsLoadingPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        var control = dependencyObject as LoadingButton;

        if (control == null)
            return;

        if ((bool)args.NewValue)
        {
            control.IsEnabled = false;
        }
        else
        {
            control.IsEnabled = true;
        }
    }
}
