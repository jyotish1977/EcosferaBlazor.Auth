using EcosferaBlazor.Auth.Infrastructure.Constants.Localization;
using Microsoft.AspNetCore.Builder;

namespace EcosferaBlazor.Auth.Infrastructure.Extensions;

public static class LocalizationServiceCollectionExtensions
{
    public static IServiceCollection AddLocalizationServices(this IServiceCollection services)
    {
        return services.AddScoped<LocalizationCookiesMiddleware>()
            .Configure<RequestLocalizationOptions>(options =>
            {
                options.AddSupportedUICultures(LocalizationConstants.SupportedLanguages.Select(x => x.Code).ToArray());
                options.AddSupportedCultures(LocalizationConstants.SupportedLanguages.Select(x => x.Code).ToArray());
                options.FallBackToParentUICultures = true;
            })
            .AddLocalization(options => options.ResourcesPath = LocalizationConstants.ResourcesPath);
    }
}