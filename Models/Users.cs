using System.ComponentModel.DataAnnotations.Schema;
using Library.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class Users
{
    [Key] 
    public int Id { get; set; }
        
    [Required]
    [Name]
    public string FName { get; set; }
        
    [Required]
    [Name]
    public string LName { get; set; }
    
    [Required]
    [Email]
    public string Email { get; set; }

    [Required]
    [Age]
    public int Age { get; set; }
}