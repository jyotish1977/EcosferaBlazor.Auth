﻿using Ecosfera.Blazor.UI.Pages.Identity.Users;
using Microsoft.JSInterop;

namespace Ecosfera.Blazor.UI.Services.JsInterop;


public partial class OrgChart
{
    private readonly IJSRuntime _jsRuntime;

    public OrgChart(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    public ValueTask Create(List<OrgItem> data)
    {
        return _jsRuntime.InvokeVoidAsync(JSInteropConstants.CreateOrgChart, data);
    }


}
