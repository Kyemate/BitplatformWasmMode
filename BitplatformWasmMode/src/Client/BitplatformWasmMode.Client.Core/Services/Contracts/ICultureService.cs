namespace BitplatformWasmMode.Client.Core.Services.Contracts;

public interface ICultureService
{
    Task ChangeCulture(string? cultureName);
}
