using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

// Configure Kestrel to use the PFX certificate
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5001, listenOptions =>
    {
        listenOptions.UseHttps("localhost.pfx", "12345");
    });
});

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("angular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
