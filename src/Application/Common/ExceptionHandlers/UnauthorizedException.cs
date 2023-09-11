namespace EcosferaBlazor.Auth.Application.Common.ExceptionHandlers;
public class UnauthorizedException : ServerException
{
    public UnauthorizedException(string message)
       : base(message, System.Net.HttpStatusCode.Unauthorized)
    {
    }
}

