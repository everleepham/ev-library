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

            var books = await context.Books
                .Include(b => b.Author)
                
                .ToListAsync();

            if (books == null || books.Count == 0)
                throw new ResourceNotFoundException("No books found");

            return books.Select(toDTO).ToList();
        }

        public async Task<BooksDTO?> GetByIdAsync(int id)
        {
            using var context = CreateContext();

            var book = await context.Books
                .Include(b => b.Author) 
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                throw new ResourceNotFoundException("Book not found");

            return toDTO(book);
        }


        public async Task<BooksDTO> AddAsync(Books book)
        {
            using var context = CreateContext();

            // check if author exits
            var author = await context.Authors.FirstOrDefaultAsync(a => a.Id == book.AuthorId);
            if (author == null)
            {
                throw new ResourceNotFoundException(
                    $"Author with Id {book.AuthorId} not found. Please create the author first."
                );
            }
            
            book.Author = author;

            // add book
            context.Books.Add(book);
            await context.SaveChangesAsync();

            await context.Entry(book).Reference(b => b.Author).LoadAsync();

            return toDTO(book);
        }

        private BooksDTO toDTO(Books book)
        {
            return new BooksDTO()
            {
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedYear = book.PublishedYear,
                Description = book.Description,
                Pages = book.Pages,
                CoverUrl = book.CoverUrl,
                AuthorName = book.Author.Name
            };
        }
    }
}