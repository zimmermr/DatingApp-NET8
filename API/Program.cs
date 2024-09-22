using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => 
{
    options.AddPolicy(name:"angular", policy =>
    {
        policy.WithOrigins("https://localhost:4200","http://localhost:4200");
    });
});


// Configure Kestrel to use the PFX certificate
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5001, listenOptions =>
    {
        listenOptions.UseHttps("localhost.pfx", "12345");
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("angular");

app.MapControllers();

app.Run();
