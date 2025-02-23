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
using Windows.System.Preview;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnoAppTemplate.Demo.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class DesignPage : Page
{
    public DesignPage()
    {
        this.InitializeComponent();
    }
   

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        await PART_Modal.ShowModal("ff", "helo");
    }

    private async void CloseModal(object sender, RoutedEventArgs e)
    {
        await PART_Modal.ShowModal("Error Modal", "This is error moadal",Controls.AlertType.Error);
    }
}
