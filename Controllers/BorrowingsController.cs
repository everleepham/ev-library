using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.Services;
using Library.DTO;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly BorrowingsService _borrowingsService;

        public BorrowingsController(BorrowingsService borrowingsService)
        {
            _borrowingsService = borrowingsService;
        }

        // GET /api/borrowings
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var borrowings = await _borrowingsService.GetAllAsync();
            return Ok(borrowings);
        }

        // GET /api/borrowings/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var borrowing = await _borrowingsService.GetByIdAsync(id);
            if (borrowing == null)
                return NotFound($"Borrowing with id {id} not found.");
            return Ok(borrowing);
        }

        // GET /api/borrowings/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var borrowings = await _borrowingsService.GetByUserIdAsync(userId);
            return Ok(borrowings);
        }

        // POST /api/borrowings
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Borrowings borrowing)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _borrowingsService.AddAsync(borrowing);
            return Ok(created);
        }

        // PATCH /api/borrowings/{id}/return
        [HttpPatch("{id}/return")]
        public async Task<IActionResult> ReturnBook(int id, [FromBody] BorrowingUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _borrowingsService.UpdateReturnedDateAsync(id, updateDto);
            return Ok(updated);
        }
    }
}
