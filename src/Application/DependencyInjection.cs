// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EcosferaBlazor.Auth.Application.Common.Behaviours;
using EcosferaBlazor.Auth.Application.Common.Interfaces.MultiTenant;
using EcosferaBlazor.Auth.Application.Common.PublishStrategies;
using EcosferaBlazor.Auth.Application.Common.Security;
using EcosferaBlazor.Auth.Application.Services.MultiTenant;
using Microsoft.Extensions.DependencyInjection;

namespace EcosferaBlazor.Auth.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(config=> {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.NotificationPublisher = new ParallelNoWaitPublisher();
            config.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            config.AddOpenBehavior(typeof(RequestExceptionProcessorBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(MemoryCacheBehaviour<,>));
            config.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
            config.AddOpenBehavior(typeof(CacheInvalidationBehaviour<,>));
         
            
        });
        services.AddFluxor(options => {
            options.ScanAssemblies(Assembly.GetExecutingAssembly());
            options.UseReduxDevTools();
        });
        services.AddLazyCache();
        services.AddScoped<TenantService>();
        services.AddScoped<ITenantService>(sp => {
            var service = sp.GetRequiredService<TenantService>();
            service.Initialize();
            return service;
        });
        services.AddScoped<RegisterFormModelFluentValidator>();
        return services;
    }
   
}
