using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate;
public static class ServiceCollectionExtensions
{
    public static void AddPage<TViewModel,TPage>(this IServiceCollection service,string route)
    {
        RouteService.RegisterRoute(route, typeof(TPage));

        service.AddTransient(typeof(TViewModel));
    }
}
