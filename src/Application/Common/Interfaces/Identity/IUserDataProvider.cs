

using EcosferaBlazor.Auth.Application.Features.Identity.Dto;

namespace EcosferaBlazor.Auth.Application.Common.Interfaces.Identity;
public interface IUserDataProvider
{
    List<ApplicationUserDto> DataSource { get; }
    event Action? OnChange;
    Task InitializeAsync();
    void Initialize();
    Task Refresh();
}
