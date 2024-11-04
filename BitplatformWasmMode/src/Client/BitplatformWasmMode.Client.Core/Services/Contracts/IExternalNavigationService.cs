namespace BitplatformWasmMode.Client.Core.Services.Contracts;

public interface IExternalNavigationService
{
    Task NavigateToAsync(string url);
}
