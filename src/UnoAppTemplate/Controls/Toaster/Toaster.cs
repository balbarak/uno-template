using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate.Controls;
public partial class Toaster  : Control
{
    public static readonly DependencyProperty ShowProperty =
       DependencyProperty.Register(nameof(Show), typeof(bool), typeof(Toaster), new PropertyMetadata(null, OnShowPropertyChanged));
    public bool Show { get => (bool)GetValue(ShowProperty); set => SetValue(ShowProperty, value); }


    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(Toaster), new PropertyMetadata(null));
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

    private void ShowViewInternal()
    {
        VisualStateManager.GoToState(this, "ShowState", true);
    }

    private void HideViewInternal(bool useTransition = true)
    {
        VisualStateManager.GoToState(this, "HideState", useTransition);
        Show = false;
    }

    private static async void OnShowPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        var control = dependencyObject as Toaster;

        if (control == null)
            return;

        if ((bool)args.NewValue)
        {
            control.ShowViewInternal();

            await Task.Delay(3000);

            control.HideViewInternal();
        }
        else
        {
            control.HideViewInternal();
        }
    }
}
