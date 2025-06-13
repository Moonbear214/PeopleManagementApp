using CoFloPeopleManagement.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoFloPeopleManagement.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> People { get; set; }
}