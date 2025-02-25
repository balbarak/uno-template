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


    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(MenuItem), new PropertyMetadata(null));
    public ICommand Command { get => (ICommand)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }


    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(MenuItem), new PropertyMetadata(null));
    public object CommandParameter { get => (object)GetValue(CommandParameterProperty); set => SetValue(CommandParameterProperty, value); }



    public static readonly DependencyProperty GlyphProperty =
        DependencyProperty.Register(nameof(Glyph), typeof(string), typeof(MenuItem), new PropertyMetadata(null));
    public string Glyph { get => (string)GetValue(GlyphProperty); set => SetValue(GlyphProperty, value); }


    public MenuItem()
    {
        this.DefaultStyleKey = typeof(MenuItem);
    }
}
