using EcosferaBlazor.Auth.Infrastructure.Constants.Database;

namespace EcosferaBlazor.Auth.Infrastructure.Extensions;

internal static class DbContextOptionsBuilderExtensions
{
    internal static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string dbProvider,
        string connectionString)
    {
        switch (dbProvider.ToLowerInvariant())
        {
            case DbProviderKeys.Npgsql:
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                return builder.UseNpgsql(connectionString,
                    e => e.MigrationsAssembly("EcosferaBlazor.Auth.Migrators.PostgreSQL"));
            case DbProviderKeys.SqlServer:
                return builder.UseSqlServer(connectionString,
                    e => e.MigrationsAssembly("EcosferaBlazor.Auth.Migrators.MSSQL"));
            case DbProviderKeys.SqLite:
                return builder.UseSqlite(connectionString,
                    e => e.MigrationsAssembly("EcosferaBlazor.Auth.Migrators.SqLite"));
            default:
                throw new InvalidOperationException($"DB Provider {dbProvider} is not supported.");
        }
    }
}