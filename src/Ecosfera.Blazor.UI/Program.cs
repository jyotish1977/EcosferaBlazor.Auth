using Ecosfera.Blazor.UI;
using Ecosfera.Blazor.UI.Services.Notifications;
using EcosferaBlazor.Auth.Application;
using EcosferaBlazor.Auth.Infrastructure;
using EcosferaBlazor.Auth.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.Connections;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterSerilog();
builder.AddBlazorUiServices();
builder.Services.AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

var app = builder.Build();

app.MapHealthChecks("/health");
app.UseExceptionHandler("/Error");
app.MapFallbackToPage("/_Host");
app.UseInfrastructure(builder.Configuration);
app.UseWebSockets();
app.MapBlazorHub(options => options.Transports = HttpTransportType.WebSockets);

if (app.Environment.IsDevelopment())
{
    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initializer.InitialiseAsync();
        await initializer.SeedAsync();
        var notificationService = scope.ServiceProvider.GetService<INotificationService>();
        if (notificationService is InMemoryNotificationService inMemoryNotificationService)
        {
            inMemoryNotificationService.Preload();
        }
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

await app.RunAsync();