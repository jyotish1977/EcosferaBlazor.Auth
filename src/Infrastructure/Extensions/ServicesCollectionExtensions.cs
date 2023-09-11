using System.Runtime.Versioning;

namespace EcosferaBlazor.Auth.Infrastructure.Extensions;

[SupportedOSPlatform("windows")]
public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ExceptionHandlingMiddleware>()
            .AddScoped<IDateTime, DateTimeService>()
            .AddScoped<IExcelService, ExcelService>()
            .AddScoped<IUploadService, UploadService>()
            .AddScoped<IPDFService, PDFService>();
    }
}