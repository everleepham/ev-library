using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Data;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly string _connectionString;

        public BooksController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Library_AppContextConnection") 
                                ?? throw new InvalidOperationException("Connection string not found");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var options = new DbContextOptionsBuilder<Library_AppContext>()
                .UseSqlServer(_connectionString)
                .Options;

            using var context = new Library_AppContext(options);
            
            var books = await context.Books.ToListAsync();
            return Ok(books);
        }
        
        [HttpGet("{id}")] 
        public async Task<IActionResult> Get(int id)
        {
            var options = new DbContextOptionsBuilder<Library_AppContext>()
                .UseSqlServer(_connectionString)
                .Options;

            using var context = new Library_AppContext(options);
            var query = context.Books.Where(b => b.Id == id);
            Console.WriteLine(query.ToQueryString());
            
            var book = await query.FirstOrDefaultAsync();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Books book)
        {
            var options = new DbContextOptionsBuilder<Library_AppContext>()
                .UseSqlServer(_connectionString)
                .Options;

            using var context = new Library_AppContext(options);
            
            var query = context.Books.Where(b => b.Id == book.Id);
            Console.WriteLine(query.ToQueryString());

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return Ok(book);
        }
    }

    public class Books
    {
        [Key] 
        public int Id { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(0, 100)]
        public int Pages { get; set; }

        [Required]
        [Column("author_id")]
        public int AuthorId { get; set; }
    }
}