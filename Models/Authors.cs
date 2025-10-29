namespace Library.Models;
using Library.Attributes;
using System.ComponentModel.DataAnnotations;


public class Authors
{
    [Key] 
    public int Id { get; set; }

    [Required]
    [Name]
    public string Name { get; set; }
    
    [StringLength(200, MinimumLength = 0)]
    public string? Bio { get; set; }
    
    [Year]
    public int BirthYear { get; set; }

    [Required]
    public string? Country { get; set; }
}