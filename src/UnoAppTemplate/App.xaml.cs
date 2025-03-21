global using System.Collections.Immutable;
global using CommunityToolkit.Mvvm.Input;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using UnoAppTemplate.Models;
global using UnoAppTemplate.ViewModels;

using Uno.Resizetizer;
using UnoAppTemplate.Demo.Views;

namespace UnoAppTemplate;
public partial class App : Application
{
    private static bool IS_DESING = false;
    public static Window? MainWindow { get; private set; }
    public static IHost? Host { get; private set; }

    public static Shell ContentHost { get; private set; }

    public App()
    {
        this.InitializeComponent();
    }

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        var builder = CreateHost(args);

        MainWindow = builder.Window;

#if DEBUG
        MainWindow.UseStudio();
#endif
        MainWindow.SetWindowIcon();

        Host = builder.Build();

        var handler = GetService<IActivationHandler>();


        if (IS_DESING)
        {
            MainWindow.Content = new DesignPage();
            MainWindow.Activate();
            return;
        }
        
        if (MainWindow.Content is not Shell)
        {
            ContentHost = new Shell();
            MainWindow.Content = ContentHost;
        }

        await handler.Activate();

        MainWindow.Activate();
    }


    public static void ReloadShell()
    {
        ContentHost = new Shell();
        MainWindow.Content = ContentHost;
    }

    public static T GetService<T>()
    {
        return Host.Services.GetService<T>();
    }

    private IApplicationBuilder CreateHost(LaunchActivatedEventArgs args)
    {
        var result = this.CreateBuilder(args)
            .Configure(host => host
#if DEBUG
                // Switch to Development environment when running in DEBUG
                .UseEnvironment(Environments.Development)
#endif
                .UseLogging(configure: (context, logBuilder) =>
                {
                    // Configure log levels for different categories of logging
                    logBuilder
                        .SetMinimumLevel(
                            context.HostingEnvironment.IsDevelopment() ?
                                LogLevel.Information :
                                LogLevel.Warning)

                        // Default filters for core Uno Platform namespaces
                        .CoreLogLevel(LogLevel.Warning);

                    // Uno Platform namespace filter groups
                    // Uncomment individual methods to see more detailed logging
                    //// Generic Xaml events
                    //logBuilder.XamlLogLevel(LogLevel.Debug);
                    //// Layout specific messages
                    //logBuilder.XamlLayoutLogLevel(LogLevel.Debug);
                    //// Storage messages
                    //logBuilder.StorageLogLevel(LogLevel.Debug);
                    //// Binding related messages
                    //logBuilder.XamlBindingLogLevel(LogLevel.Debug);
                    //// Binder memory references tracking
                    //logBuilder.BinderMemoryReferenceLogLevel(LogLevel.Debug);
                    //// DevServer and HotReload related
                    logBuilder.HotReloadCoreLogLevel(LogLevel.Information);
                    //// Debug JS interop
                    //logBuilder.WebAssemblyLogLevel(LogLevel.Debug);

                }, enableUnoLogging: true)
                .UseConfiguration(configure: configBuilder =>
                    configBuilder
                        .EmbeddedSource<App>()
                        .Section<AppConfig>()
                )
                // Enable localization (see appsettings.json for supported languages)
                .UseLocalization()
                .ConfigureServices((context, services) =>
                {
                    Startup.ConfigureServices(context, services);
                })
            );

        return result;
    }
}
