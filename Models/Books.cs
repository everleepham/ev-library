using System.ComponentModel.DataAnnotations;
using Library.Attributes;

namespace Library.Models;

public class Books
{
    [Key] 
    public int Id { get; set; }
        
    [Required]
    [Name]
    public string Title { get; set; }
    
    [Required]
    public string ISBN { get; set; }
    
    [Required]
    [Year]
    public int PublishedYear { get; set; }
    
    [StringLength(200, MinimumLength = 0)]
    public string? Description { get; set; }

    [Required]
    [BookNum]
    public int Pages { get; set; }

    [Required]
    public int AuthorId { get; set; }
    
    [Required]
    public string CoverUrl { get; set; }
    
    public Authors? Author { get; set; }

}