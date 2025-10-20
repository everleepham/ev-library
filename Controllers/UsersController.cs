using Microsoft.AspNetCore.Mvc;
using Library.Services;
using Library.Models;
using Library.DTO;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;

        public UsersController(UsersService usersService)
        {
            _service = usersService;
        }

        // GET /api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        // GET /api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");
            return Ok(user);
        }

        // POST /api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Users user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.AddAsync(user);
            return Ok(created);
        }
    }
}