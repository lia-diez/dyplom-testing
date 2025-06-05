using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiKeyController : ControllerBase
{
    private readonly IApiKeyService _apiKeyService;

    public ApiKeyController(IApiKeyService apiKeyService)
    {
        _apiKeyService = apiKeyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApiKey>>> GetApiKeys()
    {
        var apiKeys = await _apiKeyService.GetAllApiKeysAsync();
        return Ok(apiKeys);
    }

    [HttpPost]
    public async Task<ActionResult<ApiKey>> CreateApiKey([FromBody] CreateApiKeyRequest request)
    {
        var apiKey = await _apiKeyService.CreateApiKeyAsync(request.Name);
        return CreatedAtAction(nameof(GetApiKey), new { keyValue = apiKey.KeyValue }, apiKey);
    }

    [HttpGet("{keyValue}")]
    public async Task<ActionResult<ApiKey>> GetApiKey(string keyValue)
    {
        var apiKey = await _apiKeyService.GetApiKeyAsync(keyValue);
        if (apiKey == null)
        {
            return NotFound();
        }
        return Ok(apiKey);
    }

    [HttpDelete("{keyValue}")]
    public async Task<ActionResult> DeactivateApiKey(string keyValue)
    {
        var result = await _apiKeyService.DeactivateApiKeyAsync(keyValue);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}

public class CreateApiKeyRequest
{
    public required string Name { get; set; }
}