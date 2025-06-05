using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class ApiKey
{
    public int Id { get; set; }
    
    [Required]
    public string KeyValue { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? LastUsed { get; set; }
}