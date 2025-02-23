using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Animations;
using UnoAppTemplate.Controls;
using UnoAppTemplate.Views;

namespace UnoAppTemplate.Services;

public class NavigationService : INavigationService
{
    public static string CurrentRoute { get;private set; }

    public async Task Navigate(string route,object data = null)
    {
        var pageType = RouteService.GetRoutePage(route);

        if (App.ContentHost.CurrentPage != null && App.ContentHost.CurrentPage.GetType() == pageType)
            return;

        CurrentRoute = route;

        var page = Activator.CreateInstance(pageType) as Page;

        var isHostPage = route.StartsWith("//");

        if (isHostPage)
        {
            App.ContentHost.Navigate(page);
        }
        else
        {
            if (App.ContentHost.CurrentPage == null)
                App.ContentHost.Navigate(new HomePage());

            App.ContentHost.Append(page,PageAnimationType.SlideInFromRight);
        }

    }

    public async Task ShowToaster(string msg)
    {
        await App.ContentHost.ShowToaster(msg);
    }

    public async Task ShowAlert(string title,string msg,AlertType type)
    {
        await App.ContentHost.ShowAlert(title, msg, type);
    }
}
