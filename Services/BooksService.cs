using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.Models;
using Library.Exception;
using Microsoft.EntityFrameworkCore;
using Library.DTO;

namespace Library.Services
{
    public class BooksService
    {
        private readonly string _connectionString;

        public BooksService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Library_AppContextConnection") 
                                ?? throw new DbConnectionException("Connection string not found");        }

        private Library_AppContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<Library_AppContext>()
                .UseSqlServer(_connectionString)
                .Options;
            return new Library_AppContext(options);
        }

        public async Task<List<BooksDTO>> GetAllAsync()
        {
            using var context = CreateContext();
            var books = await context.Books.ToListAsync();
            return (books == null || books.Count == 0)
                ? throw new ResourceNotFoundException("No books found")
                : books.Select(toDTO).ToList();        }

        public async Task<BooksDTO?> GetByIdAsync(int id)
        {
            using var context = CreateContext();
            var query = context.Books.Where(b => b.Id == id);
            Console.WriteLine(query.ToQueryString());
            var book = await query.FirstOrDefaultAsync();
            return book == null ? throw new ResourceNotFoundException("Book not found") : toDTO(book);
        }

        public async Task<BooksDTO> AddAsync(Books book)
        {
            using var context = CreateContext();

            var query = context.Books.Where(b => b.Id == book.Id);
            Console.WriteLine(query.ToQueryString());

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return toDTO(book);
        }

        private BooksDTO toDTO(Books book)
        {
            return new BooksDTO()
            {
                Name = book.Name,
                Pages = book.Pages,
            };
        }
    }
}