using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace UnoAppTemplate.Views;

public sealed partial class LoadingPage : Page
{
    public LoadingViewModel ViewModel => DataContext as LoadingViewModel;

    public LoadingPage()
    {
        this.InitializeComponent();
        
        DataContext = App.GetService<LoadingViewModel>();
    }
}
