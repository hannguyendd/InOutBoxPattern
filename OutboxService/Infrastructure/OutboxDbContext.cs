using Microsoft.EntityFrameworkCore;
using OutboxService.Models;

namespace OutboxService.Infrastructure;

public class OutboxDbContext(DbContextOptions<OutboxDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients => Set<Patient>();
}