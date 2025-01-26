using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate;
public class Startup
{
    public static void ConfigureServices(HostBuilderContext context,IServiceCollection services)
    {
        services.AddSingleton<IActivationHandler, ActivationHandler>();
    }
}
