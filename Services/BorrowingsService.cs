using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.Models;
using Library.DTO;
using Library.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Library.Services
{
    public class BorrowingsService
    {
        private readonly string _connectionString;

        public BorrowingsService(IConfiguration configuration)
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

        // get all borrowings
        public async Task<List<BorrowingReadDto>> GetAllAsync()
        {
            using var context = CreateContext();
            var borrowings = await context.Borrowings.ToListAsync();
            return (borrowings == null || borrowings.Count == 0)
                ? throw new ResourceNotFoundException("No borrowings found")
                : borrowings.Select(b => toReadDto(b)).ToList();
        }

        public async Task<BorrowingReadDto?> GetByIdAsync(int id)
        {
            using var context = CreateContext();
            var borrowing = await context.Borrowings.FirstOrDefaultAsync(b => b.Id == id);
            return borrowing == null
                ? throw new ResourceNotFoundException("Borrowing not found")
                : toReadDto(borrowing);
        }

        // get borrowing by user
        public async Task<List<BorrowingReadDto>> GetByUserIdAsync(int userId)
        {
            using var context = CreateContext();
            var borrowings = await context.Borrowings
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return (borrowings == null || borrowings.Count == 0)
                ? throw new ResourceNotFoundException($"No borrowings found for user {userId}")
                : borrowings.Select(b => toReadDto(b)).ToList();
        }

        public async Task<BorrowingReadDto> AddAsync(Borrowings borrowing)
        {
            using var context = CreateContext();

            if (borrowing == null)
                throw new InvalidAuthorInputException("Borrowing input is invalid");

            var existing = await context.Borrowings.FirstOrDefaultAsync(b => b.Id == borrowing.Id);
            if (existing != null)
                throw new InvalidAuthorInputException("Borrowing already exists");

            context.Borrowings.Add(borrowing);
            await context.SaveChangesAsync();

            return toReadDto(borrowing);
        }

        public async Task<BorrowingReadDto> UpdateReturnedDateAsync(int id, BorrowingUpdateDto updateDto)
        {
            using var context = CreateContext();
            var borrowing = await context.Borrowings.FirstOrDefaultAsync(b => b.Id == id);

            if (borrowing == null)
                throw new ResourceNotFoundException("Borrowing not found");

            borrowing.ReturnedDate = updateDto.ReturnedDate;
            await context.SaveChangesAsync();

            return toReadDto(borrowing);
        }

        private BorrowingReadDto toReadDto(Borrowings borrowing)
        {
            return new BorrowingReadDto
            {
                Id = borrowing.Id,
                BookId = borrowing.BookId,
                UserId = borrowing.UserId,
                BorrowedDate = borrowing.BorrowedDate,
                DueDate = borrowing.DueDate,
                ReturnedDate = borrowing.ReturnedDate
            };
        }
    }

}
