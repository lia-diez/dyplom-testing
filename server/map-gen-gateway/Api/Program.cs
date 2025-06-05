using Api;
using Api.Data;
using Api.Middleware;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? builder.Configuration.GetValue<string>("DATABASE_URL");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(Setup.Swagger);
builder.Services.AddControllers();

builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddHttpClient<LlmService>();
builder.Services.AddHttpClient<MapGenService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    var maxRetries = 10;
    var delay = TimeSpan.FromSeconds(5);
    
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            logger.LogInformation("Attempting to connect to database (attempt {Attempt}/{MaxRetries})", i + 1, maxRetries);
            context.Database.Migrate();
            logger.LogInformation("Database migration completed successfully");
            break;
        }
        catch (Exception ex) when (i < maxRetries - 1)
        {
            logger.LogWarning("Database connection failed (attempt {Attempt}/{MaxRetries}): {Error}", i + 1, maxRetries, ex.Message);
            await Task.Delay(delay);
        }
    }
}

app.MapControllers();
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MapGen Gateway API v1");
        c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
        c.ConfigObject.AdditionalItems.Add("tryItOutEnabled", true);
    });// }

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();
app.UseDefaultFiles();  // serves index.html by default
app.UseStaticFiles();   // serves files from wwwroot/

app.Run();