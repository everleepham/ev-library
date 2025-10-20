using Microsoft.AspNetCore.Identity;
using Library.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data;

public class Library_AppContext : IdentityDbContext<IdentityUser>
{
    public Library_AppContext(DbContextOptions<Library_AppContext> options)
        : base(options)
    {
    }

    public DbSet<Authors> Authors { get; set; }
    public DbSet<Books> Books { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Borrowings> Borrowings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}