using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Controls;

namespace UnoAppTemplate.Services;

public interface INavigationService
{
    Task Navigate(string route, object data = null);
    Task ShowAlert(string title, string msg, AlertType type);
    Task ShowToaster(string msg);
}
