using System;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Library.DTO;
using Library.Exception;


namespace Library.Services
{
    public class AuthorService
    {
        private readonly string _connectionString;

        public AuthorService(IConfiguration configuration)
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

        public async Task<List<AuthorsDTO>> GetAllAsync()
        {
            using var context = CreateContext();
            var authors = await context.Authors.ToListAsync();
            return (authors == null || authors.Count == 0)
                ? throw new ResourceNotFoundException("No authors found")
                : authors.Select(toDTO).ToList();
        }

        public async Task<AuthorsDTO?> GetByIdAsync(int id)
        {
            using var context = CreateContext();
            var query = context.Authors.Where(a => a.Id == id);
            Console.WriteLine(query.ToQueryString());
            var author = await query.FirstOrDefaultAsync();
            return author == null ? throw new ResourceNotFoundException("Author not found") 
                : toDTO(author);
        }

        public async Task<AuthorsDTO> AddAsync(Authors author)
        {
            using var context = CreateContext();

            var query = context.Authors.Where(a => a.Id == author.Id);
            Console.WriteLine(query.ToQueryString());

            context.Authors.Add(author);
            await context.SaveChangesAsync();
            return toDTO(author);
        }

        private AuthorsDTO toDTO(Authors author)
        {
            return new AuthorsDTO
            {
                Email = author.Email,
                FName = author.FName,
                LName = author.LName,
            };
        }
    }
}