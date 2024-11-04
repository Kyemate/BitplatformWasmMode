using Microsoft.Extensions.Logging;
using BitplatformWasmMode.Client.Core;
using BitplatformWasmMode.Client.Maui.Services;
using Microsoft.Extensions.Options;

namespace BitplatformWasmMode.Client.Maui;

public static partial class MauiProgram
{
    public static void ConfigureServices(this MauiAppBuilder builder)
    {
        // Services being registered here can get injected in Maui (Android, iOS, macOS, Windows)

        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddClientCoreProjectServices(builder.Configuration);

        services.AddMauiBlazorWebView();

        if (AppEnvironment.IsDev())
        {
            services.AddBlazorWebViewDeveloperTools();
        }

        services.AddSessioned(sp =>
        {
            var handler = sp.GetRequiredService<HttpMessageHandler>();
            HttpClient httpClient = new(handler)
            {
                BaseAddress = new Uri(configuration.GetServerAddress(), UriKind.Absolute)
            };
            return httpClient;
        });

        builder.Logging.AddConfiguration(configuration.GetSection("Logging"));

        if (AppEnvironment.IsDev())
        {
            builder.Logging.AddDebug();
        }

        builder.Logging.AddConsole();

        if (AppPlatform.IsWindows)
        {
            builder.Logging.AddEventLog();
        }

        builder.Logging.AddEventSourceLogger();

        

        
        services.AddTransient<MainPage>();
        services.AddTransient<IStorageService, MauiStorageService>();
        services.AddTransient<IBitDeviceCoordinator, MauiDeviceCoordinator>();
        services.AddTransient<IExceptionHandler, MauiExceptionHandler>();
        services.AddTransient<IExternalNavigationService, MauiExternalNavigationService>();

        if (AppPlatform.IsWindows || AppPlatform.IsMacOS)
        {
            services.AddSessioned<ILocalHttpServer, MauiLocalHttpServer>();
        }

        services.AddOptions<SharedAppSettings>()
            .Bind(configuration)
            .ValidateOnStart();

        services.AddOptions<ClientAppSettings>()
            .Bind(configuration)
            .ValidateOnStart();

        services.AddTransient(sp => sp.GetRequiredService<IOptionsSnapshot<SharedAppSettings>>().Value);
        services.AddTransient(sp => sp.GetRequiredService<IOptionsSnapshot<ClientAppSettings>>().Value);

#if Android
        services.AddClientMauiProjectAndroidServices(builder.Configuration);
#elif iOS
        services.AddClientMauiProjectIosServices(builder.Configuration);
#elif Mac
        services.AddClientMauiProjectMacCatalystServices(builder.Configuration);
#elif Windows
        services.AddClientMauiProjectWindowsServices(builder.Configuration);
#endif
    }
}
