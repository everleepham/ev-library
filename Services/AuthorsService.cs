using Library.Controllers; // chứa class Authors
using Library.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public class AuthorsService
    {
        private readonly Library_AppContext _context;

        public AuthorsService(Library_AppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Authors>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Authors?> GetByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Authors> CreateAsync(Authors author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

    }
}