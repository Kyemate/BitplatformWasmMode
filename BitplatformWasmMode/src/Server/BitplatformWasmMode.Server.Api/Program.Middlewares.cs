﻿
using Microsoft.AspNetCore.Localization.Routing;

namespace BitplatformWasmMode.Server.Api;

public static partial class Program
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0#middleware-order
    /// </summary>
    private static void ConfigureMiddlewares(this WebApplication app)
    {
        var configuration = app.Configuration;
        var env = app.Environment;

        var forwardedHeadersOptions = configuration.Get<ServerApiAppSettings>()!.ForwardedHeaders;

        if (forwardedHeadersOptions is not null 
            && (app.Environment.IsDevelopment() || forwardedHeadersOptions.AllowedHosts.Any()))
        {
            // If the list is empty then all hosts are allowed. Failing to restrict this these values may allow an attacker to spoof links generated for reset password etc.
            app.UseForwardedHeaders(forwardedHeadersOptions);
        }

        if (CultureInfoManager.MultilingualEnabled)
        {
            var supportedCultures = CultureInfoManager.SupportedCultures.Select(sc => sc.Culture).ToArray();
            var options = new RequestLocalizationOptions
            {
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                ApplyCurrentCultureToResponseHeaders = true
            };
            options.SetDefaultCulture(CultureInfoManager.DefaultCulture.Name);
            options.RequestCultureProviders.Insert(1, new RouteDataRequestCultureProvider() { Options = options });
            app.UseRequestLocalization(options);
        }

        app.UseExceptionHandler("/", createScopeForErrors: true);

        if (env.IsDevelopment() is false)
        {
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseResponseCaching();

        if (env.IsDevelopment())
        {
            app.UseDirectoryBrowser();
        }

        app.UseStaticFiles();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAntiforgery();

        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.InjectJavascript($"/scripts/swagger-utils.js?v={Environment.TickCount64}");
        });

        app.MapGet("/api/minimal-api-sample/{routeParameter}", (string routeParameter, [FromQuery] string queryStringParameter) => new
        {
            RouteParameter = routeParameter,
            QueryStringParameter = queryStringParameter
        }).WithTags("Test");

        app.MapHub<Hubs.AppHub>("/app-hub");

        app.MapControllers().RequireAuthorization();
    }
}