
namespace Microsoft.JSInterop;

public static partial class IJSRuntimeExtensions
{
    public static ValueTask<string> GetBrowserPlatform(this IJSRuntime jsRuntime)
    {
        return jsRuntime.InvokeAsync<string>("App.getPlatform");
    }

    public static ValueTask ApplyBodyElementClasses(this IJSRuntime jsRuntime, List<string> cssClasses, Dictionary<string, string> cssVariables)
    {
        return jsRuntime.InvokeVoidAsync("App.applyBodyElementClasses", cssClasses, cssVariables);
    }



    /// <summary>
    /// The return value would be false during pre-rendering
    /// </summary>
    public static bool IsInitialized(this IJSRuntime jsRuntime)
    {
        var type = jsRuntime.GetType();

        return type.Name switch
        {
            "UnsupportedJavaScriptRuntime" => false, // pre-rendering
            "RemoteJSRuntime" /* blazor server */ => (bool)type.GetProperty("IsInitialized")!.GetValue(jsRuntime)!,
            _ => true // blazor wasm / hybrid
        };
    }
}
