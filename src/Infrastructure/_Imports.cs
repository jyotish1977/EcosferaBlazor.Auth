// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Components.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using EcosferaBlazor.Auth.Domain.Common;
global using EcosferaBlazor.Auth.Domain.Entities;
global using EcosferaBlazor.Auth.Domain.Entities.Audit;
global using EcosferaBlazor.Auth.Infrastructure.Persistence.Extensions;
global using EcosferaBlazor.Auth.Application.Common.Models;
global using EcosferaBlazor.Auth.Application.Common.Interfaces;
global using EcosferaBlazor.Auth.Application.Common.Interfaces.Identity;
global using EcosferaBlazor.Auth.Domain.Identity;
global using EcosferaBlazor.Auth.Infrastructure.Middlewares;
global using EcosferaBlazor.Auth.Infrastructure.Persistence;
global using EcosferaBlazor.Auth.Infrastructure.Services;
global using EcosferaBlazor.Auth.Infrastructure.Services.Identity;
