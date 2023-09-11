using Ecosfera.Blazor.UI.Models.NavigationMenu;
using EcosferaBlazor.Auth.Application.Constants.Role;

namespace Ecosfera.Blazor.UI.Services.Navigation;

public class MenuService : IMenuService
{
    private readonly List<MenuSectionModel> _features = new()
    {
        new MenuSectionModel
        {
            Title = "Application",
            SectionItems = new List<MenuSectionItemModel>
            {
                new() { Title = "Home", Icon = Icons.Material.Filled.Home, Href = "/" }
            }
        }
        //new MenuSectionModel
        //{
        //    Title = "MANAGEMENT",
        //    Roles = new[] { RoleName.Admin },
        //    SectionItems = new List<MenuSectionItemModel>
        //    {
        //        new()
        //        {
        //            IsParent = true,
        //            Title = "Authorization",
        //            Icon = Icons.Material.Filled.ManageAccounts,
        //            MenuItems = new List<MenuSectionSubItemModel>
        //            {
        //                new()
        //                {
        //                    Title = "Multi-Tenant",
        //                    Href = "/system/tenants",
        //                    PageStatus = PageStatus.Completed
        //                },
        //                new()
        //                {
        //                    Title = "Users",
        //                    Href = "/identity/users",
        //                    PageStatus = PageStatus.Completed
        //                },
        //                new()
        //                {
        //                    Title = "Roles",
        //                    Href = "/identity/roles",
        //                    PageStatus = PageStatus.Completed
        //                },
        //                new()
        //                {
        //                    Title = "Profile",
        //                    Href = "/user/profile",
        //                    PageStatus = PageStatus.Completed
        //                }
        //            }
        //        }
        //    }
        //}
    };

    public IEnumerable<MenuSectionModel> Features => _features;
}