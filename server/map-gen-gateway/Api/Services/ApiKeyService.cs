using System.Security.Cryptography;
using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public interface IApiKeyService
{
    Task<bool> IsValidApiKeyAsync(string apiKey);
    Task UpdateLastUsedAsync(string apiKey);
    Task<ApiKey?> GetApiKeyAsync(string keyValue);
    Task<IEnumerable<ApiKey>> GetAllApiKeysAsync();
    Task<ApiKey> CreateApiKeyAsync(string name);
    Task<bool> DeactivateApiKeyAsync(string keyValue);
}

public class ApiKeyService : IApiKeyService
{
    private readonly ApplicationDbContext _context;

    public ApiKeyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsValidApiKeyAsync(string apiKey)
    {
        var key = await _context.ApiKeys
            .FirstOrDefaultAsync(k => k.KeyValue == apiKey && k.IsActive);
        
        return key != null;
    }

    public async Task UpdateLastUsedAsync(string apiKey)
    {
        var key = await _context.ApiKeys
            .FirstOrDefaultAsync(k => k.KeyValue == apiKey);
        
        if (key != null)
        {
            key.LastUsed = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ApiKey?> GetApiKeyAsync(string keyValue)
    {
        return await _context.ApiKeys
            .FirstOrDefaultAsync(k => k.KeyValue == keyValue);
    }

    public async Task<IEnumerable<ApiKey>> GetAllApiKeysAsync()
    {
        return await _context.ApiKeys
            .Where(k => k.IsActive)
            .OrderBy(k => k.Name)
            .ToListAsync();
    }

    public async Task<ApiKey> CreateApiKeyAsync(string name)
    {
        var apiKey = new ApiKey
        {
            KeyValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)),
            Name = name,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.ApiKeys.Add(apiKey);
        await _context.SaveChangesAsync();

        return apiKey;

    }

    public async Task<bool> DeactivateApiKeyAsync(string keyValue)
    {
        var key = await _context.ApiKeys
            .FirstOrDefaultAsync(k => k.KeyValue == keyValue);
        
        if (key != null)
        {
            key.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
        
        return false;
    }
}