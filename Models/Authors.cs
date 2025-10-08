namespace Library.Models;
using Library.Attributes;
using System.ComponentModel.DataAnnotations;


public class Authors
{
    [Key] 
    public int Id { get; set; }
        
    [Required]
    [Email]
    public string Email { get; set; }

    [Required]
    [Name]
    public string FName { get; set; }
        
    [Required]
    [Name]
    public string LName { get; set; }

    [Required]
    [Age]
    public int Age { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string? Address { get; set; }
}