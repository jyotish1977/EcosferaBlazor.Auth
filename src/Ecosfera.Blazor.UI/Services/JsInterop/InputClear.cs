using Microsoft.JSInterop;

namespace Ecosfera.Blazor.UI.Services.JsInterop;


public partial class InputClear
{
    private readonly IJSRuntime _jsRuntime;

    public InputClear(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    public ValueTask Clear(string targetId)
    {
        return _jsRuntime.InvokeVoidAsync(JSInteropConstants.ClearInput, targetId);
    }


}
