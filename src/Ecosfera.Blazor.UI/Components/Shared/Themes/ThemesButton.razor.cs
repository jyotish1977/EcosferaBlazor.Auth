using Ecosfera.Blazor.UI.Services;
using Ecosfera.Blazor.UI.Services.Layout;
using Microsoft.AspNetCore.Components.Web;

namespace Ecosfera.Blazor.UI.Components.Shared.Themes;

public partial class ThemesButton
{
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    
    [Inject] private LayoutService LayoutService { get; set; } = default!;

}