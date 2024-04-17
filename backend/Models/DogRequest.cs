using System.ComponentModel.DataAnnotations;

namespace canineApi.Models;

public class DogRequest
{
    public required string Name { get; set; }
    
    public IFormFile? Image { get; set; }
}