using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Library.Data;

namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Library_AppContextConnection") ?? throw new InvalidOperationException("Connection string 'Library_AppContextConnection' not found.");;

            builder.Services.AddDbContext<Library_AppContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<Library_AppContext>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            
            app.MapGet("/testdb", async (Library_AppContext db) =>
            {
                try
                {
                    bool canConnect = await db.Database.CanConnectAsync();
                    return canConnect ? Results.Ok("Connected to database!") : Results.Problem("Cannot connect to database.");
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}