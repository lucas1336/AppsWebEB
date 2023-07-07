using System.Net;
using System.Text.Json;
using EB.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace EB.Domain.ExceptionHandling;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ResourceNotFoundException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var errorCode = "500";
        var message = "Internal Server Error";

        if (ex is ResourceNotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            errorCode = "404";
            message = ex.Message;
        }
        else if (ex is ValidationException)
        {
            statusCode = HttpStatusCode.BadRequest;
            errorCode = "400";
            message = ex.Message;
        }

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            ErrorCode = errorCode,
            Message = message
        };

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var errorJson = JsonSerializer.Serialize(errorResponse, options);

        await context.Response.WriteAsync(errorJson);
    }
}