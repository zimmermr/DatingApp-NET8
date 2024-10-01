using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        // Add services to the container.
        services.AddCors(options =>
        {
            options.AddPolicy(name: "angular", policy =>
            {
                policy.WithOrigins("https://localhost:4200", "http://localhost:4200");
            });
        });

        services.AddScoped<ITokenService, TokenService>();

        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}
