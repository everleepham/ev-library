namespace Library.DTO;

public class BooksDTO
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int PublishedYear { get; set; }
    public string? Description { get; set; }
    public int Pages { get; set; }
    public string CoverUrl { get; set; }
    public string AuthorName { get; set; }

}