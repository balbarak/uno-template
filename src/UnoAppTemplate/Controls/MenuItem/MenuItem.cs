using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate.Controls;
public partial class MenuItem : NavigationViewItem
{

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(MenuItem), new PropertyMetadata(null));
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }


    public MenuItem()
    {
        
        this.DefaultStyleKey = typeof(MenuItem);
    }
}
