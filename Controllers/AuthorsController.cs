using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Library.Data;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly string _connectionString;

        public AuthorsController(IConfiguration configuration)
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
            
            var authors = await context.Authors.ToListAsync();
            return Ok(authors);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var options = new DbContextOptionsBuilder<Library_AppContext>()
                .UseSqlServer(_connectionString)
                .Options;

            using var context = new Library_AppContext(options);
            var query = context.Authors.Where(a => a.Id == id);
            Console.WriteLine(query.ToQueryString());
            
            var author = await query.FirstOrDefaultAsync();
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Authors author)
        {
            var options = new DbContextOptionsBuilder<Library_AppContext>()
                .UseSqlServer(_connectionString)
                .Options;

            using var context = new Library_AppContext(options);
            
            var query = context.Authors.Where(a => a.Id == author.Id);
            Console.WriteLine(query.ToQueryString());

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            return Ok(author);
        }
    }

    public class Authors
    {
        [Key] 
        public int Id { get; set; }
        
        [Required]
        [StringLength(90, MinimumLength = 5)]
        public string? Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string? FName { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string? LName { get; set; }

        [Required]
        [Range(0, 100)]
        public int? Age { get; set; }

        [Required]
        [StringLength(90, MinimumLength = 3)]
        public string? Address { get; set; }
    }
}