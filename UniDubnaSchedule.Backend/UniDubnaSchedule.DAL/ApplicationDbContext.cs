using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<Schedule> Schedule { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<BellsSchedule> BellsSchedule { get; set; }
    
    // ToDo: Modify this shit
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(
            new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "UniDubnaSchedule",
                IntegratedSecurity = false,
                UserID = "sa",
                Password = ""
            }.ConnectionString
        );
}