using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace UnoAppTemplate.Helpers;
public class EmptyStringToTrueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var convertedValue = value as string;

        if (convertedValue == null || string.IsNullOrWhiteSpace(convertedValue))
            return true;

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return true;
    }
}
