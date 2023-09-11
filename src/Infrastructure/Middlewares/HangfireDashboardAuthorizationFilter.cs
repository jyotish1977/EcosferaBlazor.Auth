using Hangfire.Dashboard;

namespace EcosferaBlazor.Auth.Infrastructure.Middlewares;
public class HangfireDashboardAsyncAuthorizationFilter : IDashboardAsyncAuthorizationFilter
{

    public Task<bool> AuthorizeAsync(DashboardContext context)
    {
        return Task.FromResult(true);
    }
}

public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {

        return true;
    }
}