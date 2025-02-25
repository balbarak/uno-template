using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Markup;

namespace UnoAppTemplate.Controls;

[MarkupExtensionReturnType(ReturnType = typeof(string))]
public class LocaleExtension : MarkupExtension
{
    protected override object ProvideValue()
    {
        return "Welcome";

        return base.ProvideValue();
    }
    
    protected override object ProvideValue(IXamlServiceProvider serviceProvider)
    {
        return "FFF";

        return base.ProvideValue(serviceProvider);
    }
}
