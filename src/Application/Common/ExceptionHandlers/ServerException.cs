using System.Net;

namespace EcosferaBlazor.Auth.Application.Common.ExceptionHandlers;
public class ServerException : Exception
{
    public IEnumerable<string> ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }

    public ServerException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = new string[] { message };
        StatusCode = statusCode;
    }
}