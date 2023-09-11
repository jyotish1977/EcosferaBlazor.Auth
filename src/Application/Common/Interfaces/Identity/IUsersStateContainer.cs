using System.Collections.Concurrent;

namespace EcosferaBlazor.Auth.Application.Common.Interfaces.Identity;
public interface IUsersStateContainer
{
    ConcurrentDictionary<string, string> UsersByConnectionId { get; }
    event Action? OnChange;
    void AddOrUpdate(string connectionId, string? name);
    void Remove(string connectionId);
}
