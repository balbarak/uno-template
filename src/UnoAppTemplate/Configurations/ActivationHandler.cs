using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Views;

namespace UnoAppTemplate;
public class ActivationHandler : IActivationHandler
{

    public async Task Activate()
    {
        App.MainWindow.Content = new ButtonsPage();
    }
}
