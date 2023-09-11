using Ecosfera.Blazor.UI.Models.NavigationMenu;

namespace Ecosfera.Blazor.UI.Services.Navigation;

public interface IMenuService
{
    IEnumerable<MenuSectionModel> Features { get; }
}
