using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Attributes;

namespace Library.Models;

public class Books
{
    [Key] 
    public int Id { get; set; }
        
    [Required]
    [Name]
    public string Name { get; set; }

    [Required]
    [BookNum]
    public int Pages { get; set; }

    [Required]
    [Column("author_id")]
    public int AuthorId { get; set; }
}