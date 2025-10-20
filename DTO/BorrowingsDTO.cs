namespace Library.DTO;

public class BorrowingUpdateDto
{
    public DateTime ReturnedDate { get; set; }
}

public class BorrowingReadDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}