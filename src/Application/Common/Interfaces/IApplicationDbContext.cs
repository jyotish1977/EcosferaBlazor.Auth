// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using EcosferaBlazor.Auth.Domain.Entities.Logger;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EcosferaBlazor.Auth.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Logger> Loggers { get; set; }
    DbSet<AuditTrail> AuditTrails { get; set; }
    DbSet<Tenant> Tenants { get; set; }
    ChangeTracker ChangeTracker { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
