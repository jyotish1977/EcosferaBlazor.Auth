using EcosferaBlazor.Auth.Infrastructure.Hubs;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace EcosferaBlazor.Auth.Infrastructure.Extensions;

public static class SignalRServiceCollectionExtensions
{
    public static void AddSignalRServices(this IServiceCollection services)
    {
        services.AddSingleton<IUsersStateContainer, UsersStateContainer>()
            .AddScoped<HubClient>()
            .AddSignalR();
    }
}