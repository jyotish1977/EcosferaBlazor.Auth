// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Reflection;
using EcosferaBlazor.Auth.Domain.Entities.Logger;

namespace EcosferaBlazor.Auth.Infrastructure.Persistence;

#nullable disable
public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser, ApplicationRole, string,
    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>, IApplicationDbContext
{


    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
        ) : base(options)
    {
  
    }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Logger> Loggers { get; set; }
    public DbSet<AuditTrail> AuditTrails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyGlobalFilters<ISoftDelete>(s => s.Deleted == null);
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }

}