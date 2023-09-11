// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EcosferaBlazor.Auth.Application.Common.Configurations;
using EcosferaBlazor.Auth.Application.Common.Interfaces.MultiTenant;
using EcosferaBlazor.Auth.Infrastructure.Extensions;
using EcosferaBlazor.Auth.Infrastructure.Persistence.Interceptors;
using EcosferaBlazor.Auth.Infrastructure.Services.JWT;
using EcosferaBlazor.Auth.Infrastructure.Services.MultiTenant;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace EcosferaBlazor.Auth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DashboardSettings>(configuration.GetSection(DashboardSettings.Key));
        services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.Key));
        services.Configure<AppConfigurationSettings>(configuration.GetSection(AppConfigurationSettings.Key));
        services.Configure<IdentitySettings>(configuration.GetSection(IdentitySettings.Key));
        services.AddSingleton(s => s.GetRequiredService<IOptions<DashboardSettings>>().Value);
        services.AddSingleton(s => s.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        services.AddSingleton(s => s.GetRequiredService<IOptions<AppConfigurationSettings>>().Value);
        services.AddSingleton(s => s.GetRequiredService<IOptions<IdentitySettings>>().Value);
        services.AddScoped<AuthenticationStateProvider, BlazorAuthStateProvider>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ITenantProvider, TenantProvider>();
        services.AddScoped<ISaveChangesInterceptor,AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("BlazorDashboardDb");
                options.EnableSensitiveDataLogging();
            });
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>((p, m) =>
             {
                 var databaseSettings = p.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                 m.AddInterceptors(p.GetServices<ISaveChangesInterceptor>());
                 m.UseDatabase(databaseSettings.DBProvider, databaseSettings.ConnectionString);
             });
        }

       
        services.AddScoped<IDbContextFactory<ApplicationDbContext>, BlazorContextFactory<ApplicationDbContext>>();
        services.AddTransient<IApplicationDbContext>(provider =>
            provider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());
        services.AddScoped<ApplicationDbContextInitializer>();


        services.AddLocalizationServices();
        services.AddServices()
            .AddHangfireService()
            .AddSerialization()
            .AddMessageServices(configuration)
            .AddSignalRServices();
        services.AddAuthenticationService(configuration);
        services.AddHttpClientService();
        services.AddControllers();
        return services;
    }
}