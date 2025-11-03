using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Middleware;
using Library.Services;

namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connection string
            var connectionString = builder.Configuration
                .GetConnectionString("Library_AppContextConnection")
                ?? throw new InvalidOperationException("Connection string 'Library_AppContextConnection' not found.");

            // DbContext
            builder.Services.AddDbContext<Library_AppContext>(options =>
                options.UseSqlServer(connectionString));

            // Identity
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Library_AppContext>();

            // DI services
            builder.Services.AddScoped<BooksService>();
            builder.Services.AddScoped<AuthorService>();
            builder.Services.AddScoped<UsersService>();
            builder.Services.AddScoped<BorrowingsService>();

            // Controllers
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi(); // Swagger / OpenAPI

            // ⚡ CORS (must be before builder.Build())
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Swagger/OpenAPI
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // Middleware pipeline
            app.UseCors("AllowAll"); // CORS must be before UseAuthorization
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();

            // Map controllers
            app.MapControllers();

            // Run app
            app.Run();
        }
    }
}
