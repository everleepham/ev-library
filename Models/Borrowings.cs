using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class Borrowings
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
        
    [Required]
    [Column("book_id")]
    public int BookId { get; set; }
        
    [Required]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required]
    [Column("borrowed_date")]
    public DateTime BorrowedDate { get; set; }
    
    [Required]
    [Column("due_date")]
    public DateTime DueDate { get; set; }
    
    [Column("returned_date")]
    public DateTime? ReturnedDate { get; set; }
}