using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class AuthorService
    {
        private readonly string _connectionString;

        public AuthorService(string connectionString)
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

        public async Task<List<Authors>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Authors.ToListAsync();
        }

        public async Task<Authors?> GetByIdAsync(int id)
        {
            using var context = CreateContext();
            var query = context.Authors.Where(a => a.Id == id);
            Console.WriteLine(query.ToQueryString());
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Authors> AddAsync(Authors author)
        {
            using var context = CreateContext();

            var query = context.Authors.Where(a => a.Id == author.Id);
            Console.WriteLine(query.ToQueryString());

            context.Authors.Add(author);
            await context.SaveChangesAsync();
            return author;
        }
    }
}