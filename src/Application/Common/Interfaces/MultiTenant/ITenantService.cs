using EcosferaBlazor.Auth.Application.Features.Tenants.DTOs;

namespace EcosferaBlazor.Auth.Application.Common.Interfaces.MultiTenant;
public interface ITenantService
{
    List<TenantDto> DataSource { get; }
    event Action? OnChange;
    Task InitializeAsync();
    void Initialize();
    Task Refresh();
}
