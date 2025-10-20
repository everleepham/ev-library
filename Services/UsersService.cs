
using Library.Data;
using Library.Models;
using Library.DTO;
using Library.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Library.Services
{
    public class UsersService
    {
        private readonly string _connectionString;
        
        public UsersService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Library_AppContextConnection") 
                                ?? throw new DbConnectionException("Connection string not found");
        }
        
        private Library_AppContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<Library_AppContext>()
                .UseSqlServer(_connectionString)
                .Options;
            return new Library_AppContext(options);
        }

        public async Task<List<UsersDTO>> GetAllAsync()
        {
            using var context = CreateContext();
            var users = await context.Users.ToListAsync();
            return (users == null || users.Count == 0)
                ? throw new ResourceNotFoundException("No users found")
                : users.Select(toDTO).ToList();
        }

        public async Task<UsersDTO?> GetByIdAsync(int id)
        {
            using var context = CreateContext();
            var query = context.Users.Where(u => u.Id == id);
            Console.WriteLine(query.ToQueryString());
            var user = await query.FirstOrDefaultAsync();
            return user == null ? throw new ResourceNotFoundException("User not found")
                                : toDTO(user);
        }

        public async Task<UsersDTO> AddAsync(Users user)
        {
            using var context = CreateContext();

            if (user == null)
            {
                throw new InvalidAuthorInputException("User not found");
            }

            var existing = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existing != null)
                throw new InvalidAuthorInputException("User already exists");

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return toDTO(user);
        }

        private UsersDTO toDTO(Users user)
        {
            return new UsersDTO
            {
                Email = user.Email,
                FName = user.FName,
                LName = user.LName,
                Age = user.Age,
                
            };
        }
    }
}
