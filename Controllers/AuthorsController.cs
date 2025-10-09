using Microsoft.AspNetCore.Mvc;
using Library.Services;
using Library.Models;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _service;

        public AuthorsController(AuthorService authorsService)
        {
            _service = authorsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _service.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _service.GetByIdAsync(id);
            if (author == null)
                return NotFound($"Author with ID {id} not found.");
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Authors author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.AddAsync(author);
            return Ok(created);
        }
    }
}