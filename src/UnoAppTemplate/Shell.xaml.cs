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

namespace UnoAppTemplate;

public sealed partial class Shell : Page
{
    public Shell()
    {
        this.InitializeComponent();
    }

    private void OnMenuButtonClick(object sender, RoutedEventArgs e)
    {
        _splitView.IsPaneOpen = true;
    }
}
