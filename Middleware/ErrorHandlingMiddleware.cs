using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Library.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");

                context.Response.StatusCode = 500;
                
                await context.Response.WriteAsync(
                    $"Internal server error. {ex.Message}" +
                    (ex.InnerException != null ? $" Inner: {ex.InnerException.Message}" : "")
                );
            }
        }
    }
}