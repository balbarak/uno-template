using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate.Services;

public interface INavigationService
{
    Task Navigate(string route, object data = null);
}
