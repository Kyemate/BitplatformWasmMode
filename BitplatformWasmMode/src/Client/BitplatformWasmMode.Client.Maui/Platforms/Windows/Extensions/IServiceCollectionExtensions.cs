﻿
namespace Microsoft.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddClientMauiProjectWindowsServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Services being registered here can get injected in Maui/windows.


        return services;
    }
}
