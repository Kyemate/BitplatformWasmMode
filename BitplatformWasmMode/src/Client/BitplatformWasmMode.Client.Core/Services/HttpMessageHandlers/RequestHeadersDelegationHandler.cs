﻿using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace BitplatformWasmMode.Client.Core.Services.HttpMessageHandlers;

public partial class RequestHeadersDelegationHandler(HttpMessageHandler handler)
    : DelegatingHandler(handler)
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Omit);
        request.SetBrowserResponseStreamingEnabled(true);

        if (CultureInfoManager.MultilingualEnabled && string.IsNullOrEmpty(CultureInfo.CurrentUICulture.Name) is false)
        {
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));
        }

        return await base.SendAsync(request, cancellationToken);
    }
}