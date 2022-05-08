using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL;

/// <summary>
/// Database context class.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Schedule> Schedule { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<BellsSchedule> BellsSchedule { get; set; }
    public DbSet<User> Users { get; set; }
}