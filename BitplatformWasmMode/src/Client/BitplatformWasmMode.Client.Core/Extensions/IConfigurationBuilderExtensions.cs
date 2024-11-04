using System.Reflection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Microsoft.Extensions.Configuration;

public static partial class IConfigurationBuilderExtensions
{
    /// <summary>
    /// Configuration priority (Lowest to highest) =>
    /// Shared/appsettings.json
    /// Shared/appsettings.Production.json
    /// Client/Core/appsettings.json
    /// Client/Core/appsettings.Production.json
    ///     Server.Web and Server.Api only =>
    ///         Server/appsettings.json
    ///         Server/appsettings.Production.json
    ///         https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration#default-application-configuration-sources
    ///     Blazor WebAssembly only =>
    ///         Client/Web/appsettings.json (If present)
    ///         Client/Web/appsettings.Production.json (If present)
    /// </summary>
    public static void AddClientConfigurations(this IConfigurationBuilder builder)
    {
        IConfigurationBuilder configBuilder = AppPlatform.IsBrowser ? new WebAssemblyHostConfiguration() : new ConfigurationBuilder();

        var sharedAssembly = Assembly.Load("BitplatformWasmMode.Shared");

        configBuilder.AddJsonStream(sharedAssembly.GetManifestResourceStream("BitplatformWasmMode.Shared.appsettings.json")!);

        var envSharedAppSettings = sharedAssembly.GetManifestResourceStream($"BitplatformWasmMode.Shared.appsettings.{AppEnvironment.Current}.json");
        if (envSharedAppSettings != null)
        {
            configBuilder.AddJsonStream(envSharedAppSettings);
        }

        var clientCoreAssembly = Assembly.Load("BitplatformWasmMode.Client.Core");

        configBuilder.AddJsonStream(clientCoreAssembly.GetManifestResourceStream("BitplatformWasmMode.Client.Core.appsettings.json")!);

        var envClientCoreAppSettings = clientCoreAssembly.GetManifestResourceStream($"BitplatformWasmMode.Client.Core.appsettings.{AppEnvironment.Current}.json");
        if (envClientCoreAppSettings != null)
        {
            configBuilder.AddJsonStream(envClientCoreAppSettings);
        }

        if (AppPlatform.IsBrowser)
        {
            var providersField = builder.GetType().GetField("_providers", BindingFlags.NonPublic | BindingFlags.Instance)!;
            providersField.SetValue(builder, (((IConfigurationRoot)configBuilder).Providers).Union(((IConfigurationRoot)builder).Providers).ToList());
        }
        else if (AppPlatform.IsBlazorHybrid)
        {
            foreach (var source in configBuilder.Sources)
            {
                builder.Sources.Add(source);
            }
        }
        else
        {
            var originalSources = builder.Sources.ToList();
            builder.Sources.Clear();
            foreach (var source in configBuilder.Sources.Union(originalSources))
            {
                builder.Sources.Add(source);
            }
        }
    }
}
