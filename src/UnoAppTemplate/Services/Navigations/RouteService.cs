using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate;

public class RouteService
{
    private static Dictionary<string, Type> _routes;

    public const string HOME_PAGE = "//homepage";
    public const string BUTTONS_PAGE = "//buttons";
    public const string TYPOGRAPHY_PAGE = "//typography";
    public const string THEME_PAGE = "//theme";
    public const string SETTINGS_PAGE = "//settings";

    static RouteService()
    {
        _routes = new Dictionary<string, Type>();
    }

    public static void RegisterRoute(string route,Type pageType)
    {
        _routes.Add(route, pageType);
    }

    public static Type GetRoutePage(string route)
    {
        return _routes.GetValueOrDefault(route);
    }
}
