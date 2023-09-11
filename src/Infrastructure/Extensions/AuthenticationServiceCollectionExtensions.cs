using System.Configuration;
using System.Reflection;
using System.Text;
using EcosferaBlazor.Auth.Application.Common.Configurations;
using EcosferaBlazor.Auth.Application.Constants.ClaimTypes;
using EcosferaBlazor.Auth.Application.Constants.Permission;
using EcosferaBlazor.Auth.Application.Constants.User;
using EcosferaBlazor.Auth.Infrastructure.Services.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace EcosferaBlazor.Auth.Infrastructure.Extensions;
public static class AuthenticationServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {

        services
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        services.Configure<IdentityOptions>(options =>
        {
            var identitySettings = configuration.GetRequiredSection(IdentitySettings.Key).Get<IdentitySettings>();
            // Password settings
            options.Password.RequireDigit = identitySettings.RequireDigit;
            options.Password.RequiredLength = identitySettings.RequiredLength;
            options.Password.RequireNonAlphanumeric = identitySettings.RequireNonAlphanumeric;
            options.Password.RequireUppercase = identitySettings.RequireUpperCase;
            options.Password.RequireLowercase = identitySettings.RequireLowerCase;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identitySettings.DefaultLockoutTimeSpan);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.AllowedForNewUsers = true;

            // Default SignIn settings.
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            // User settings
            options.User.RequireUniqueEmail = true;

        });
        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationClaimsIdentityFactory>()
                .AddScoped<IIdentityService, IdentityService>()
                .AddAuthorization(options =>
                 {
                     options.AddPolicy("CanPurge", policy => policy.RequireUserName(UserName.Administrator));
                     // Here I stored necessary permissions/roles in a constant
                     foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
                     {
                         var propertyValue = prop.GetValue(null);
                         if (propertyValue is not null)
                         {
                             options.AddPolicy((string)propertyValue, policy => policy.RequireClaim(ApplicationClaimTypes.Permission, (string)propertyValue));
                         }
                     }
                 })
                 .AddAuthentication()
                 .AddJwtBearer(options =>
                 {
                     options.SaveToken = true;
                     options.RequireHttpsMetadata = false;
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateIssuerSigningKey = false,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppConfigurationSettings:Secret"]!)),
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         RoleClaimType = ClaimTypes.Role,
                         ClockSkew = TimeSpan.Zero,
                         ValidateLifetime = true
                     };

                     options.Events = new JwtBearerEvents
                     {
                         OnMessageReceived = context =>
                         {
                             var accessToken = context.Request.Headers.Authorization;
                             var path = context.HttpContext.Request.Path;
                             if (!string.IsNullOrEmpty(accessToken) &&
                                 (path.StartsWithSegments("/signalRHub")))
                             {
                                 context.Token = accessToken.ToString().Substring(7);
                             }
                             return Task.CompletedTask;
                         }
                     };
                 })
                 .AddGoogle(googleOptions =>
                 {
                     googleOptions.ClientId = "166606886033-ki1mq911b776mr4ibmhpk9c0h9opmk9e.apps.googleusercontent.com";
                     googleOptions.ClientSecret = "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";
                     googleOptions.CallbackPath = new PathString("/signin-google");
                 })
                 .AddMicrosoftAccount(micorosoftOptions =>
                 {
                     micorosoftOptions.ClientId = "6c7d1d55-1e88-41e8-b1d4-865c8f6bc30e";
                     micorosoftOptions.ClientSecret = "98301870-009a-4785-a515-b5d580dc20dc";
                     micorosoftOptions.CallbackPath = new PathString("/signin-microsoft");
                 })
                 .AddFacebook(facebookOptions =>
                 {
                     facebookOptions.AppId = "580628277601844";
                     facebookOptions.AppSecret = "9f59b1fe7bb830065f71b7982410022d";
                     facebookOptions.CallbackPath = new PathString("/signin-facebook");
                 }); 
   
        services.AddScoped<AccessTokenProvider>();
        services.AddScoped<UserDataProvider>();
        services.AddScoped<IUserDataProvider>(sp =>
        {
            var service = sp.GetRequiredService<UserDataProvider>();
            service.Initialize();
            return service;
        });
        return services;
    }

}
