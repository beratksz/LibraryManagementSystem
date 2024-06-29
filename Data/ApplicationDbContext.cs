using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LibraryManagementSystem.Models;


public class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BorrowRecord> BorrowRecords { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}