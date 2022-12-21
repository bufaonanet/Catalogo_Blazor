using Microsoft.JSInterop;

namespace Catalogo_Blazor.Client.Utils;

public static class IJSRunTimeExtensions
{
    public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
    {
        return js.InvokeAsync<object>("localStorage.setItem",key, content);
    }

    public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
    {
        return js.InvokeAsync<string>("localStorage.getItem", key);
    }

    public static ValueTask<object> RemoveFromLocalStorage(this IJSRuntime js, string key)
    {
        return js.InvokeAsync<object>("localStorage.removeItem", key);
    }
}
