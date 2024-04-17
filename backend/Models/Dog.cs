using System.ComponentModel.DataAnnotations;

namespace canineApi.Models;

public class Dog
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public string ImageUrl { get; set; }
}