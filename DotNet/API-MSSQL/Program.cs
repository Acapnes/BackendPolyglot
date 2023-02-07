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

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Data Source=localhost;Initial Catalog=exp;UID=sa;PWD=123;TrustServerCertificate=True;"));
        builder.Services.AddScoped<IRepository<User>, UsersRepository>();
        builder.Services.AddScoped<IRepository<Entry>, EntriesRepository>();
        builder.Services.AddScoped<EntriesRepository, EntriesRepository>();
        builder.Services.AddScoped<UsersRepository, UsersRepository>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMiddleware<LoggingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}