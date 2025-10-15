using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace Library.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;


        // constructor
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // run each times there's a request
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

            await _next(context); // move to nex middleware

            Console.WriteLine($"Response: {context.Response.StatusCode}");
        }
    }
}