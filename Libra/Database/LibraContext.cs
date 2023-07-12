using Libra.Models;
using Libra.Models.BookModels;
using Libra.Models.RegistrationModels;
using Libra.Models.RentModels;
using Libra.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace Libra.Database;

public class LibraContext : DbContext
{
    public LibraContext(DbContextOptions<LibraContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    // public DbSet<Chat> Chats { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookPicture> Pictures { get; set; }
    public DbSet<Registration> Registrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(e => e.Books)
            .WithOne()
            .HasForeignKey(k => k.OwnerId);
        
        modelBuilder.Entity<User>()
            .HasMany(e => e.RentedBooks)
            .WithOne()
            .HasForeignKey(k => k.BorrowerId);

        modelBuilder.Entity<Book>()
            .HasMany(e => e.Rents)
            .WithOne()
            .HasForeignKey(k => k.BookId);
        
        modelBuilder.Entity<Book>()
            .HasMany(e => e.Pictures)
            .WithOne()
            .HasForeignKey(k => k.BookId);
    }
}
