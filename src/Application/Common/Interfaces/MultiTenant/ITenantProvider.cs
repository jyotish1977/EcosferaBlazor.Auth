namespace EcosferaBlazor.Auth.Application.Common.Interfaces.MultiTenant;

public interface ITenantProvider
{
    string? TenantId { get; set; }
    string? TenantName { get; set; }
    void Update();
    Guid Register(Action callback);
    void Clear();
    void Unregister(Guid id);
}