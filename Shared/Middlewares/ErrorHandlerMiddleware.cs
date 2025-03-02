using System.Net;
using Microsoft.AspNetCore.Http;
using Shared.Contracts;

namespace Shared.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ApiException exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, ApiException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exception.Status;

        await context.Response.WriteAsJsonAsync(new ApiErrorResponse
            (exception.Status,
            exception.Message,
            DateTime.UtcNow,
            ToDescription(exception.Status),
            Guid.NewGuid()));
    }

    private static string ToDescription(HttpStatusCode statusCode)
    {
        var name = Enum.GetName(statusCode) ?? string.Empty;
        var description = string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x.ToString() : x.ToString()));
        return description;
    }

}