using InboxService.Models;
using Microsoft.EntityFrameworkCore;

namespace InboxService.Infrastructure;

public class InboxDbContext(DbContextOptions<InboxDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}