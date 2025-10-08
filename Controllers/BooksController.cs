using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.Services;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;

        public BooksController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Library_AppContextConnection") 
                                   ?? throw new InvalidOperationException("Connection string not found");
            _booksService = new BooksService(connectionString);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _booksService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _booksService.GetByIdAsync(id);
            if (book == null)
                return NotFound($"Book with id {id} not found.");
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Books book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _booksService.AddAsync(book);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
    }
}