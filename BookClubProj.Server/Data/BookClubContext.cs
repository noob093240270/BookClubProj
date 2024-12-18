using BookClubProj.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BookClubProj.Server.Data
{
    public class BookClubContext : DbContext
    {

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<ReadBook> ReadBooks { get; set; } = null!;
        public BookClubContext(DbContextOptions<BookClubContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
