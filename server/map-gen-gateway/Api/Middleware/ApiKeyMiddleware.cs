using Api.Services;

namespace Api.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "X-API-KEY";
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ApiKeyMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;

        // Allow requests to the root or static frontend paths without API key
        if (path is not null && (path == "/" || path.StartsWith("/index.html") || path.StartsWith("/css") || path.StartsWith("/js")))
        {
            await _next(context);
            return;
        }
        
        if (path.Equals("/api/ApiKey", StringComparison.OrdinalIgnoreCase) && context.Request.Method == HttpMethods.Post)
        {
            await _next(context);
            return;
        }
        
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key was not provided.");
            return;
        }

        using var scope = _serviceScopeFactory.CreateScope();
        var apiKeyService = scope.ServiceProvider.GetRequiredService<IApiKeyService>();

        var isValid = await apiKeyService.IsValidApiKeyAsync(extractedApiKey!);
        
        if (!isValid)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Unauthorized client.");
            return;
        }

        // Update last used timestamp
        await apiKeyService.UpdateLastUsedAsync(extractedApiKey!);

        await _next(context);
    }
}
/*
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "X-API-KEY";
    private readonly string _apiKey;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _apiKey = configuration.GetValue<string>("ApiKey") ?? throw new ArgumentNullException("API Key is not configured.");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key was not provided.");
            return;
        }

        if (!_apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Unauthorized client.");
            return;
        }

        await _next(context);
    }
}
*/