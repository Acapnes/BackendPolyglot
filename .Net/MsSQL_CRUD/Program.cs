using API_MSSQL.Data;
using API_MSSQL.Models;
using API_MSSQL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Data Source=localhost;Initial Catalog=DBExperimental;UID=sa;PWD=123;TrustServerCertificate=True;"));
        builder.Services.AddScoped<IRepository<User>, UsersRepository>();
        builder.Services.AddScoped<IRepository<Entry>, EntriesRepository>();
        builder.Services.AddScoped<EntriesRepository, EntriesRepository>();
        builder.Services.AddScoped<UsersRepository, UsersRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMiddleware<LoggingMiddleware>();
            
            // Func validation on specified Endpoints
            /* app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/general"), appBuilder =>
                {
                    appBuilder.UseMiddleware<MyMiddlewareOne>();
                });
            */
            
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
