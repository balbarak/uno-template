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
    public const string TEST_PAGE = "/test";
    public const string BUTTONS_PAGE = "//buttons";

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
