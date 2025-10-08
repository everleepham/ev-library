using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BooksService
    {
        private readonly string _connectionString;

        public BooksService(string connectionString)
        {
            _connectionString = connectionString ?? throw new InvalidOperationException("Connection string not found");
        }

        private Library_AppContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<Library_AppContext>()
                .UseSqlServer(_connectionString)
                .Options;
            return new Library_AppContext(options);
        }

        public async Task<List<Books>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Books.ToListAsync();
        }

        public async Task<Books?> GetByIdAsync(int id)
        {
            using var context = CreateContext();
            var query = context.Books.Where(b => b.Id == id);
            Console.WriteLine(query.ToQueryString());
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Books> AddAsync(Books book)
        {
            using var context = CreateContext();

            var query = context.Books.Where(b => b.Id == book.Id);
            Console.WriteLine(query.ToQueryString());

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return book;
        }
    }
}