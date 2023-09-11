using EcosferaBlazor.Auth.Application.Common.Interfaces.Serialization;
using EcosferaBlazor.Auth.Infrastructure.Services.Serialization;

namespace EcosferaBlazor.Auth.Infrastructure.Extensions;
public static class SerializationServiceCollectionExtensions
{
    public static IServiceCollection AddSerialization(this IServiceCollection services)
        => services.AddSingleton<ISerializer, SystemTextJsonSerializer>();
}
