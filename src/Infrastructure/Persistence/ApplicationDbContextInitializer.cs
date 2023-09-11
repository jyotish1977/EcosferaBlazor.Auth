﻿using System.Reflection;
using EcosferaBlazor.Auth.Application.Constants.ClaimTypes;
using EcosferaBlazor.Auth.Application.Constants.Permission;
using EcosferaBlazor.Auth.Application.Constants.Role;
using EcosferaBlazor.Auth.Application.Constants.User;
using EcosferaBlazor.Auth.Domain.Enums;

namespace EcosferaBlazor.Auth.Infrastructure.Persistence;
public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer() || _context.Database.IsNpgsql() || _context.Database.IsSqlite())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }
    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            _context.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }
    private static IEnumerable<string> GetAllPermissions()
    {
        var allPermissions = new List<string>();
        var modules = typeof(Permissions).GetNestedTypes();

        foreach (var module in modules)
        {
            var moduleName = string.Empty;
            var moduleDescription = string.Empty;

            var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            foreach (var fi in fields)
            {
                var propertyValue = fi.GetValue(null);

                if (propertyValue is not null)
                    allPermissions.Add((string)propertyValue);
            }
        }

        return allPermissions;
    }

    private async Task TrySeedAsync()
    {
        // Default tenants
        if (!_context.Tenants.Any())
        {
            _context.Tenants.Add(new Tenant { Name = "Master", Description = "Master Site" });
            _context.Tenants.Add(new Tenant { Name = "Slave", Description = "Slave Site" });
            await _context.SaveChangesAsync();

        }

        // Default roles
        var administratorRole = new ApplicationRole(RoleName.Admin) { Description = "Admin Group" };
        var userRole = new ApplicationRole(RoleName.Basic) { Description = "Basic Group" };
        var permissions = GetAllPermissions();
        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
           
            foreach (var permission in permissions)
            {
                await _roleManager.AddClaimAsync(administratorRole, new Claim(ApplicationClaimTypes.Permission, permission));
            }
        }
        if (_roleManager.Roles.All(r => r.Name != userRole.Name))
        {
            await _roleManager.CreateAsync(userRole);
            foreach (var permission in permissions)
            {
                if (permission.StartsWith("Permissions.Products"))
                    await _roleManager.AddClaimAsync(userRole, new Claim(ApplicationClaimTypes.Permission, permission));
            }
        }
        // Default users
        var administrator = new ApplicationUser { UserName = UserName.Administrator, Provider = "Local", IsActive = true, TenantId = _context.Tenants.First().Id, TenantName = _context.Tenants.First().Name, DisplayName = UserName.Administrator, Email = "new163@163.com", EmailConfirmed = true, ProfilePictureDataUrl = "https://s.gravatar.com/avatar/78be68221020124c23c665ac54e07074?s=80" };
        var demo = new ApplicationUser { UserName = UserName.Demo, IsActive = true, Provider = "Local", TenantId = _context.Tenants.First().Id, TenantName = _context.Tenants.First().Name, DisplayName = UserName.Demo, Email = "neozhu@126.com", EmailConfirmed = true, ProfilePictureDataUrl = "https://s.gravatar.com/avatar/ea753b0b0f357a41491408307ade445e?s=80" };


        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, UserName.DefaultPassword);
            await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name! });
        }
        if (_userManager.Users.All(u => u.UserName != demo.UserName))
        {
            await _userManager.CreateAsync(demo, UserName.DefaultPassword);
            await _userManager.AddToRolesAsync(demo, new[] { userRole.Name! });
        }

        // Default data
        // Seed, if necessary
    }
}
