using BudgetIntelligence2024.Application;
using FastEndpoints;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BudgetIntelligence2024.API;

class ExceptionHandler;

/// <summary>
/// Extensions for global exception handling
/// Based on: https://fast-endpoints.com/docs/exception-handler
/// </summary>
public static class UnhandledExceptionsExtensions
{
    /// <summary>
    /// registers the default global exception handler which will log the exceptions on the server and return a user-friendly json response to the client
    /// when unhandled exceptions occur.
    /// TIP: when using this exception handler, you may want to turn off the asp.net core exception middleware logging to avoid duplication like so:
    /// <code>
    /// "Logging": { "LogLevel": { "Default": "Warning", "Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware": "None" } }
    /// </code>
    /// </summary>
    /// <param name="logger">an optional logger instance</param>
    /// <param name="logStructuredException">set to true if you'd like to log the error in a structured manner</param>
    /// <param name="useGenericReason">set to true if you don't want to expose the actual exception reason in the json response sent to the client</param>
    public static IApplicationBuilder UseCustomExceptionHandler(
        this IApplicationBuilder app,
        IWebHostEnvironment env,
        ILogger? logger = null,
        bool enableLogging = true)
    {
        app.UseExceptionHandler(
            errApp =>
            {
                errApp.Run(
                    async ctx =>
                    {
                        var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();

                        if (exHandlerFeature is not null)
                        {
                            logger ??= ctx.Resolve<ILogger<ExceptionHandler>>();

                            var route = exHandlerFeature.Endpoint?.DisplayName?.Split(" => ")[0];
                            var exceptionType = exHandlerFeature.Error.GetType().Name;
                            var reason = exHandlerFeature.Error.Message;

                            if(enableLogging)
                            {
                                logger.LogError(
                                    $"""
                                     =================================
                                     {route}
                                     TYPE: {exceptionType}
                                     REASON: {reason}
                                     ---------------------------------
                                     {exHandlerFeature.Error.StackTrace}
                                     """);
                            }

                            var statusCode = GetStatusCode(exceptionType);

                            ctx.Response.StatusCode = statusCode;
                            ctx.Response.ContentType = "application/problem+json";

                            if (env.IsDevelopment())
                            {
                                await ctx.Response.WriteAsJsonAsync(new ErrorResponse
                                {
                                    Exception = exHandlerFeature.Error.Message,
                                    StackTrace = exHandlerFeature.Error.StackTrace,
                                    StatusCode = statusCode
                                });
                            }
                        }
                    });
            });

        return app; 
    }

    private static int GetStatusCode(string exceptionType)
    {
        return exceptionType switch
        {
            nameof(DbUpdateException) => (int)HttpStatusCode.BadRequest,
            nameof(DbUpdateConcurrencyException) => (int)HttpStatusCode.BadRequest,
            nameof(CSVParseException) => (int)HttpStatusCode.BadRequest,

            _ => (int)HttpStatusCode.InternalServerError
        };
    }

    public class ErrorResponse
    {
        public string? Exception { get; set; }
        public string? StackTrace { get; set; }
        public int StatusCode { get; set; }
    }
}
