using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using UnoAppTemplate.Animations;

namespace UnoAppTemplate.Controls;

public sealed partial class PageHeader : UserControl
{

    public static readonly DependencyProperty BackCommandProperty =
        DependencyProperty.Register(nameof(BackCommand), typeof(ICommand), typeof(PageHeader), new PropertyMetadata(null));
    public ICommand BackCommand { get => (ICommand)GetValue(BackCommandProperty); set => SetValue(BackCommandProperty, value); }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(object), typeof(PageHeader), new PropertyMetadata(null));
    public object Header { get => (object)GetValue(HeaderProperty); set => SetValue(HeaderProperty, value); }

    public PageHeader()
    {
        this.InitializeComponent();

        BackCommand = new AsyncRelayCommand(OnGoBack);
    }

    private async Task OnGoBack()
    {
        await App.ContentHost.GoBack(PageAnimationType.SlideOutToRight);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (App.ContentHost.CanGoBack)
        {
            PART_MenuButton.Visibility = Visibility.Collapsed;
            PART_BackButton.Visibility = Visibility.Visible;
        }
        else
        {
            PART_MenuButton.Visibility = Visibility.Visible;
            PART_BackButton.Visibility = Visibility.Collapsed;
        }
    }

    private void OnMenuButtonClick(object sender, RoutedEventArgs e)
    {
        App.ContentHost?.ShowMenu();
    }

}
